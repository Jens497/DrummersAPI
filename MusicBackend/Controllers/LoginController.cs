using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicBackend.Data;
using MusicBackend.Dtos;
using MusicBackend.Models;
using MusicBackend.Security;
using MusicBackend.Services;

namespace MusicBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly IAuthenticationService _authService;
        private readonly IPasswordAuthentication _passService;
        private readonly IUserService _userService;
        public LoginController(IConfiguration configuration, IAuthenticationService authService, IPasswordAuthentication passService, IUserService userService)
        {
            _configuration = configuration;
            _authService = authService;
            _passService = passService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            (UserRequest userReturning, string userToken) = await _authService.Authentication(userLoginDto.Username, userLoginDto.Password);

            if(userToken == null)
            {
                //return status code 401 since user doesn't exist.
                return Unauthorized("User does not exists!");
            }

            var user = userReturning.CovertToDtoUser();
            var jsonObj = new { userToken, user };

            return Ok(jsonObj);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUpdateUserDto registeredUserDto)
        {
            Console.WriteLine("dhsja");
            var checkIfUsernameTaken = await _userService.checkIfUsernameTaken(registeredUserDto.Username);
            if (checkIfUsernameTaken)
                return Unauthorized("Please select another username, this one is taken.");

            //Create user corresponding object that corresponds to the one in the database
            tblUser userForRegistration = new()
            {
                Username = registeredUserDto.Username,
                Password = _passService.generateNewPassword(registeredUserDto.Password),
                Email = registeredUserDto.Email,
                Firstname = registeredUserDto.Firstname,
                Lastname = registeredUserDto.Lastname,
            };

            //Here we register the new user
            await _userService.RegisterUser(userForRegistration);

            //Next we need to create a token for our new user.
            (_, string userToken) = await _authService.Authentication(registeredUserDto.Username, registeredUserDto.Password);
            var userRegistered = userForRegistration.CovertToDtoUser();
            var jsonObj = new { userToken, userRegistered };

            return CreatedAtAction(nameof(Login), new { id= userRegistered.Id }, jsonObj);
        }
    }
}
