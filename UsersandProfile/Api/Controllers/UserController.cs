using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UsersandProfile.Models;
using UsersandProfile.Services;

namespace UsersandProfile.Controllers
{
    /// <summary>
    /// Controlador para la autenticación y gestión de usuarios.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;



        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UserController"/>.
        /// </summary>
        /// <param name="userService">Servicio para gestionar los datos de usuarios.</param>
        /// <param name="logger">Logger para la información de diagnóstico.</param>
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Inicia sesión con las credenciales de un usuario.
        /// </summary>
        /// <param name="userDto">Datos del usuario para iniciar sesión.</param>
        /// <returns>Resultado de la operación de inicio de sesión.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                _logger.LogWarning("Intento de inicio de sesión fallido por datos nulos.");
                return BadRequest("Los datos del usuario son requeridos.");
            }

            try
            {
                var resultado = await _userService.Login(userDto);
                _logger.LogInformation("Inicio de sesión exitoso.");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el inicio de sesión.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error durante el inicio de sesión.");
            }
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="userDto">Datos del usuario para el registro.</param>
        /// <returns>Resultado de la operación de registro.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                _logger.LogWarning("Intento de registro fallido por datos nulos.");
                return BadRequest("Los datos del usuario son requeridos.");
            }

            try
            {
                var resultado = await _userService.Register(userDto);
                _logger.LogInformation("Registro de usuario exitoso.");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el registro.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error durante el registro.");
            }
        }

        /// <summary>
        /// Cambia la contraseña de un usuario.
        /// </summary>
        /// <param name="changePasswordDto">Datos para el cambio de contraseña.</param>
        /// <returns>Resultado de la operación de cambio de contraseña.</returns>
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto == null)
            {
                _logger.LogWarning("Intento de cambio de contraseña fallido por datos nulos.");
                return BadRequest("Los datos para cambiar la contraseña son requeridos.");
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            try
            {
                if (await _userService.ChangePasswordAsync(userId, changePasswordDto))
                {
                    _logger.LogInformation($"Cambio de contraseña exitoso para el usuario ID {userId}.");
                    return Ok("Contraseña actualizada con éxito.");
                }
                else
                {
                    _logger.LogWarning($"Fallo al cambiar la contraseña para el usuario ID {userId}.");
                    return BadRequest("Error al actualizar la contraseña.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cambiar la contraseña para el usuario ID {userId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al cambiar la contraseña.");
            }
        }
    }
}
