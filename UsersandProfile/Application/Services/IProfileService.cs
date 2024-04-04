using ServicioProfiles.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicioProfiles.Services
{
    public interface IProfileService
    {
        Task<IEnumerable<Profile>> GetProfiles();
        Task<Profile?> GetProfileByID(int id);
        Task<Profile> CreateProfile(Profile profile);
        Task<bool>DeleteProfile(int id);
        Task<Profile?>UpdateProfile(Profile profileToUpdate);
    }
}
