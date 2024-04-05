using UsersandProfile.Models;

namespace UsersandProfile.Repositories
{
    public interface IProfileRepository
    {
        IEnumerable<Profile> GetAll();
        Profile GetById(int id);
        Profile Add(Profile profile);
        Profile Update(Profile profile);
        void Delete(int id);
    }
}