using Microsoft.EntityFrameworkCore;
using ServicioProfiles.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicioProfiles.Repositories
{   
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public async Task<IEnumerable<Profile>> GetProfilesAsync()
        {
            return await _context.Perfiles.ToListAsync();
        }

        public async Task<Profile?> GetProfileByIDAsync(int id)
        {
            return await _context.Perfiles.FindAsync(id);
        }
    }
}
