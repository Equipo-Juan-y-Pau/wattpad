using UsersandProfile.Models;

namespace UsersandProfile.Services
{
    public interface IProfileService
    {
        Task<IEnumerable<Profile>> GetAll();
        Task<Profile> GetById(int id);
        Task<Profile> Add(Profile profile);
        Task<Profile> Update(int id, Profile profile);
        Task<bool> Delete(int id);
    }
}
