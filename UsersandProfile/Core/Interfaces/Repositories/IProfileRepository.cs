using System.Collections.Generic;
using System.Threading.Tasks; 
using UsersandProfile.Models; 
public interface IProfileRepository
{
    Task<IEnumerable<Profile>> GetProfilesAsync();
    Task<Profile?> GetProfileByIDAsync(int id);
    Task<Profile> CreateProfileAsync(Profile profile);
    Task<bool> DeleteProfileAsync(int id);
    Task<Profile?> UpdateProfileAsync(Profile profileToUpdate);
}
