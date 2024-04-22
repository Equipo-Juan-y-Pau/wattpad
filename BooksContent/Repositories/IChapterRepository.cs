using System.Collections.Generic;
using System.Threading.Tasks; 
using UsersandProfile.Models; 
public interface IProfileRepository
{
    Task<IEnumerable<Profile>> GetChaptersAsync();
    Task<Profile?> GetChapterByIDAsync(int id);
    Task<Profile> CreateProfileAsync(Profile profile);
    Task<bool> DeleteChapterAsync(int id);
    Task<Profile?> UpdateChapterAsync(Profile profileToUpdate);
}
