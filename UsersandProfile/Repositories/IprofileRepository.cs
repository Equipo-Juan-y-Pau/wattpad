using UsersandProfile.Models;

namespace UsersandProfile.Repositories
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> GetAll();
        Task<Profile> GetById(int id);
        Task<Profile> Add(Profile profile);
        Task<Profile> Update(Profile profile);
        Task<bool> Delete(int id);
    }
}