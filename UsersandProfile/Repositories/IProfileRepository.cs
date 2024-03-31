using System.Collections.Generic;
using System.Threading.Tasks;
using ServicioProfiles.Models; 
public interface IProfileRepository
{
    Task<IEnumerable<Profile>> GetProfilesAsync();
    Task<Profile?> GetProfileByIDAsync(int id);
}
