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
        
    }
}
