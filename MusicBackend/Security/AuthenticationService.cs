using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicBackend.Data;
using MusicBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicBackend.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private IConfiguration _config;
        private readonly MusicDbContext _dbContext;
        private readonly IPasswordAuthentication _passwordAuth;

        public AuthenticationService(MusicDbContext dbContext, IConfiguration configuration, IPasswordAuthentication passwordAuth)
        {
            _dbContext = dbContext;
            _config = configuration;
            _passwordAuth = passwordAuth;
        }

        public async Task<(UserRequest, string)> Authentication(string username, string password)
        {
            var userToAuth = await _dbContext.User.Where(x => x.Username == username).FirstOrDefaultAsync();

            if(userToAuth == null || !_passwordAuth.verifyPassword(userToAuth.Password, password))
            {
                (UserRequest, string) value = (null, "");
                return value;
            }
            
            //Create a security key with the JWT string key from config
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //The claims for the token IE. the parameters for it what is used to identify and the username for the user
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userToAuth.Id.ToString()),
                new Claim(ClaimTypes.Name, userToAuth.Username)
            };

            //Create JWT security token, here we get the issuer and audiance (which is the same) from the config file as well.
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
                );

            //Create/Write the new token, and return it along with the user object
            var writtenToken = new JwtSecurityTokenHandler().WriteToken(token);
            return (userToAuth.ConvertUsertblToUserRequest(), writtenToken);
        }

        public string GetNameFromToken(ClaimsPrincipal claims)
        {
            return claims.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        }
    }
}
