// Services/PedidoService.cs
using UsersandProfile.Models;

namespace UsersandProfile.Services
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

        public async Task<Profile>CreateProfile(Profile profile)
        {
            return await _profileRepository.CreateProfileAsync(profile);
        }
        public async Task<bool>DeleteProfile(int id)
        {
            return await _profileRepository.DeleteProfileAsync(id);
        }
        public async Task<Profile?>UpdateProfile(Profile profileToUpdate)
        {
            return await _profileRepository.UpdateProfileAsync(profileToUpdate);
        }

    }
}
