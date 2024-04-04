using Microsoft.AspNetCore.Mvc;
using ServicioUsers.Models;
using Microsoft.AspNetCore.Authorization;
using ServicioUsers.DTOs;
using ServicioUsers.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace UsersandUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            // verificamos si usuario existe
            var result = await _userService.RegisterUser(registerDto);

            if (result == "Usuario creado con exito")
            {
                return StatusCode(201);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _userService.LoginUser(loginDto);
            if (token == null)
            {
                return Unauthorized("Correo, Usuario o Contraseña inválidos");
            }

            return Ok(new { Token = token});
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(LoginDto loginDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0")
            var token = await _userService.LoginUser(loginDto);
            if (token == null)
            {
                return Unauthorized("Correo, Usuario o Contraseña inválidos");
            }

            return Ok(new { Token = token});
        }
    }
}
