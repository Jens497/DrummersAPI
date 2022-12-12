using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicBackend.Data;
using MusicBackend.Dtos;
using MusicBackend.Models;
using MusicBackend.Security;
using MusicBackend.Services;

namespace MusicBackend.Controllers
{
    [Authorize]
    [Route("api/excercise")]
    [ApiController]
    public class ExcerciseController : ControllerBase
    {

        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly IExcerciseService _excerciseService;
        

        public ExcerciseController(IAuthenticationService authService, IExcerciseService excerciseService, IUserService userService)
        {
            _authService = authService;
            _excerciseService = excerciseService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<ExcerciseDto>> GetExcercises()
        {            
            var username = _authService.GetNameFromToken(User);

            var excercises = (await _excerciseService.GetExcercisesFromDb(username)).Select(x => x.ConvertToExcerciseDto());
            return excercises;
        }

        [HttpPost]
        public async Task<ActionResult> CreateExcercise([FromBody] ExcerciseDto excerciseDto)
        {
            var username = _authService.GetNameFromToken(User);
            tblExcercise newExcercise = new()
            {
                Username = username,
                Name = excerciseDto.Name,
                Filename = excerciseDto.Filename,
            };

            await _excerciseService.CreateExcercise(newExcercise);

            return Ok("The excercise was created");
        }

        [HttpDelete("{excerciseId}")]
        public async Task<ActionResult> DeleteExcercise(int excerciseId)
        {
            var excercise = await _excerciseService.GetSingleExcercise(excerciseId);

            if (excercise is null)
                return BadRequest("No excercise found.");

            await _excerciseService.DeleteExcercise(excerciseId);
            return Ok();
        }
    }
}
