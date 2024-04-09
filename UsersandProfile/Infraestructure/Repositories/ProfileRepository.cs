using Microsoft.EntityFrameworkCore;
using UsersandProfile.Models;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace UsersandProfile.Repositories
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

        public async Task<Profile> CreateProfileAsync(Profile profile)
        {
            await _context.Perfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<bool> DeleteProfileAsync(int id)
        {
            var profile = await _context.Perfiles.FindAsync(id);
            if (profile == null)
            {
                return false;
            }

            _context.Perfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Profile?> UpdateProfileAsync(Profile profileToUpdate)
        {
            var existingProfile = await _context.Perfiles.FindAsync(profileToUpdate.Id);
            if (existingProfile == null)
            {
                return null;
            }

            existingProfile.NombreCompleto = profileToUpdate.NombreCompleto;
            existingProfile.AvatarURL = profileToUpdate.AvatarURL;
            existingProfile.FotoPerfilURL = profileToUpdate.FotoPerfilURL;

            _context.Perfiles.Update(existingProfile);
            await _context.SaveChangesAsync();
            return existingProfile;
        }
    }
}
