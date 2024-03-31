// Services/PedidoService.cs
using ServicioProfiles.Models;
using ServicioProfiles.Repositories;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServicioProfiles.Services
{
    public class ProfileService : IProfileService
    {
        
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<IEnumerable<Profile>> GetProfiles()   
        {
            return await _profileRepository.GetProfilesAsync();
        }

        public async Task<Profile?>GetProfileByID(int id)
        {
            return await _profileRepository.GetProfileByIDAsync(id);
        }
    }
}
