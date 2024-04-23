using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersandProfile.Models;
using UsersandProfile.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog; // Importar Serilog
using System;

namespace UsersandProfile.Controllers
{
    /// <summary>
    /// Controlador para la gestión de perfiles de usuario.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<ProfileController> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ProfileController"/>.
        /// </summary>
        /// <param name="profileService">Servicio para gestionar los datos de perfiles.</param>
        /// <param name="logger">Logger para la información de diagnóstico.</param>
        public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los perfiles de usuarios.
        /// </summary>
        /// <returns>Una lista de perfiles de usuarios.</returns>
        /// <response code="200">Si se recuperan correctamente los perfiles.</response>
        /// <response code="500">Si ocurre un error interno en el servidor.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los perfiles");
                var profiles = await _profileService.GetProfiles();
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los perfiles");
                return StatusCode(500, "Error interno del servidor al obtener los perfiles");
            }
        }

        /// <summary>
        /// Obtiene un perfil específico por ID.
        /// </summary>
        /// <param name="id">El ID del perfil a obtener.</param>
        /// <returns>El perfil solicitado.</returns>
        /// <response code="200">Si se encuentra el perfil.</response>
        /// <response code="404">Si no se encuentra el perfil.</response>
        /// <response code="500">Si ocurre un error interno en el servidor.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfileByID(int id)
        {
            try
            {
                _logger.LogInformation($"Obteniendo el perfil con ID: {id}");
                var profile = await _profileService.GetProfileByID(id);
                if (profile == null)
                {
                    _logger.LogWarning($"Perfil con ID: {id} no encontrado");
                    return NotFound($"Perfil con ID: {id} no encontrado.");
                }
                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el perfil con ID: {id}");
                return StatusCode(500, $"Error interno del servidor al obtener el perfil con ID: {id}");
            }
        }

        /// <summary>
        /// Crea un nuevo perfil de usuario.
        /// </summary>
        /// <param name="profile">Los datos del perfil para crear.</param>
        /// <returns>El perfil creado.</returns>
        /// <response code="201">Si se crea correctamente el perfil.</response>
        /// <response code="400">Si los datos del perfil son nulos.</response>
        /// <response code="500">Si ocurre un error interno en el servidor.</response>
        [HttpPost]
        public async Task<ActionResult<Profile>> CreateProfile([FromBody] Profile profile)
        {
            if (profile == null)
            {
                _logger.LogWarning("El intento de crear un perfil falló debido a datos de entrada nulos");
                return BadRequest("Los datos del perfil son nulos.");
            }

            try
            {
                _logger.LogInformation("Intentando crear un nuevo perfil");
                var createdProfile = await _profileService.CreateProfile(profile);
                if (createdProfile == null)
                {
                    _logger.LogWarning("Falló la creación del perfil");
                    return NotFound("No se pudo crear el perfil.");
                }

                _logger.LogInformation($"Perfil creado con éxito con ID: {createdProfile.Id}");
                return CreatedAtAction(nameof(GetProfileByID), new { id = createdProfile.Id }, createdProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el perfil");
                return StatusCode(500, "Error interno del servidor al crear el perfil");
            }
        }

        /// <summary>
        /// Elimina un perfil de usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del perfil a eliminar.</param>
        /// <returns>Estado indicando el resultado de la operación.</returns>
        /// <response code="204">Si se elimina correctamente el perfil.</response>
        /// <response code="404">Si no se encuentra el perfil.</response>
        /// <response code="500">Si ocurre un error interno en el servidor.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            try
            {
                _logger.LogInformation($"Intentando eliminar el perfil con ID: {id}");
                var wasDeleted = await _profileService.DeleteProfile(id);
                if (!wasDeleted)
                {
                    _logger.LogWarning($"No se pudo eliminar el perfil con ID: {id}");
                    return NotFound($"Perfil con ID: {id} no encontrado.");
                }

                _logger.LogInformation($"Perfil con ID: {id} eliminado con éxito");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el perfil con ID: {id}");
                return StatusCode(500, $"Error interno del servidor al eliminar el perfil con ID: {id}");
            }
        }

        /// <summary>
        /// Actualiza los datos de un perfil de usuario.
        /// </summary>
        /// <param name="id">El ID del perfil a actualizar.</param>
        /// <param name="profile">Los nuevos datos para el perfil.</param>
        /// <returns>El perfil actualizado.</returns>
        /// <response code="200">Si se actualiza correctamente el perfil.</response>
        /// <response code="400">Si los datos proporcionados son nulos o los IDs no coinciden.</response>
        /// <response code="404">Si no se encuentra el perfil a actualizar.</response>
        /// <response code="500">Si ocurre un error interno en el servidor.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, [FromBody] Profile profile)
        {
            if (profile == null)
            {
                _logger.LogWarning("El intento de actualizar un perfil falló debido a datos de entrada nulos");
                return BadRequest("Los datos del perfil son nulos.");
            }

            if (id != profile.Id)
            {
                _logger.LogWarning("Discrepancia entre el ID del perfil proporcionado y el ID del perfil en la ruta durante la actualización");
                return BadRequest("El ID del perfil no coincide con el proporcionado.");
            }

            try
            {
                _logger.LogInformation($"Intentando actualizar el perfil con ID: {id}");
                var updatedProfile = await _profileService.UpdateProfile(profile);
                if (updatedProfile == null)
                {
                    _logger.LogWarning($"Perfil con ID: {id} no encontrado durante la actualización");
                    return NotFound($"Perfil con ID: {id} no encontrado.");
                }

                _logger.LogInformation($"Perfil con ID: {id} actualizado con éxito");
                return Ok(updatedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el perfil con ID: {id}");
                return StatusCode(500, $"Error interno del servidor al actualizar el perfil con ID: {id}");
            }
        }
    }
}
