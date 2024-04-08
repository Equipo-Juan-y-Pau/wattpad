using Microsoft.AspNetCore.Mvc;
using UsersandProfile.Models;
using UsersandProfile.Services;

namespace UsersandProfile.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class UserController :ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto) 
        {
            try 
            {
                var resultado = await _userService.login(userDto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                var resultado = await _userService.register(userDto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

