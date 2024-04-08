using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicioProfiles.Models;
using ServicioProfiles.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsersandProfile.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        // GET: Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            var profiles = await _profileService.GetProfiles(); // Aquí también ajusta el nombre del método
            return Ok(profiles);
        }

        // GET: Profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfileByID(int id)
        {
            var profile = await _profileService.GetProfileByID(id);

            if (profile == null)
            {
                return NotFound($"No se encontró un perfil con el ID {id}.");
            }

            return Ok(profile);
        }

        // POST: Profiles
        [HttpPost]
        public async Task<ActionResult<Profile>> CreateProfile([FromBody] Profile profile)
        {
            if (profile == null)
            {
                return BadRequest("El perfil proporcionado es nulo.");
            }
            try
            {
                var createdProfile = await _profileService.CreateProfile(profile);
                if (createdProfile == null)
                {
                    return NotFound("No se pudo crear el perfil.");
                }

                return CreatedAtAction(nameof(GetProfileByID), new { id = createdProfile.Id }, createdProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }   

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var wasDeleted = await _profileService.DeleteProfile(id);

            if (!wasDeleted)
            {
                return NotFound($"No se encontró un perfil con el ID {id}.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, Profile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest("El ID del perfil no coincide con el ID Proporcionado.");
            }

            var updatedProfile = await _profileService.UpdateProfile(profile);

            if (updatedProfile == null)
            {
                return NotFound($"No se encontró un perfil con el ID {id}.");
            }

            return Ok(updatedProfile);
        }

    }
}
