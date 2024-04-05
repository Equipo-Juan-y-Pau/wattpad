using UsersandProfile.Models;

namespace UsersandProfile.Services
{
    public interface IProfileService
    {
        IEnumerable<Profile> GetAll();
        Profile GetById(int id);
        Profile Add(Profile profile);
        Profile Update(int id, Profile profile);
        void Delete(int id);
    }
}
