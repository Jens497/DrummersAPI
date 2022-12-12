using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Data;
using MusicBackend.Models;
using MusicBackend.Security;
using MusicBackend.Services;

namespace MusicBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MusicDbContext _dbContext;
        private readonly IAuthenticationService _authService;
        private readonly IPasswordAuthentication _passService;
        private readonly IUserService _userService;


        public UserController(MusicDbContext dbContext, IAuthenticationService authService, IPasswordAuthentication passwordAuthentication, 
                              IUserService userService)
        {
            _dbContext = dbContext;
            _authService = authService;
            _passService = passwordAuthentication;
            _userService = userService;
        }

        [HttpPut("UpdateUser")] 
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserModel request)
        {
            var username = _authService.GetNameFromToken(User);

            if(username == null)
                return NotFound("The user was not found!");

            try
            {
                var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Username == username);
                user.Email = request.Email;
                user.Firstname = request.Firstname;
                user.Lastname = request.Lastname;
                user.Password = _passService.generateNewPassword(request.Password);

                await _userService.UpdateSelectUser(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }


        //Test data
        /*private List<UserRequest> GetUsers()
        {
            return new List<UserRequest>{
                new UserRequest { UserName = "First", Firstname = "Bj" },
                new UserRequest { UserName = "First", Firstname = "Bj" },
                new UserRequest { UserName = "First", Firstname = "Bj" },
            };
        }*/
    }
}
