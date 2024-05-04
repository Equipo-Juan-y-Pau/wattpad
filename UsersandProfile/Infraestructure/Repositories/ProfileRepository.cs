using Microsoft.EntityFrameworkCore;
using UsersandProfile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace UsersandProfile.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProfileRepository> _logger;

        public ProfileRepository(ApplicationDbContext context, ILogger<ProfileRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Profile>> GetProfilesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all profiles");
                return await _context.Perfiles.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving profiles");
                throw;
            }
        }

        public async Task<Profile?> GetProfileByIDAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving profile by ID: {id}");
                var profile = await _context.Perfiles.FindAsync(id);
                if (profile == null)
                {
                    _logger.LogWarning($"Profile with ID: {id} not found");
                }
                return profile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving profile with ID: {id}");
                throw;
            }
        }

        public async Task<Profile> CreateProfileAsync(Profile profile)
        {
            try
            {
                _logger.LogInformation("Creating a new profile");
                await _context.Perfiles.AddAsync(profile);
                await _context.SaveChangesAsync();
                return profile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating profile");
                throw;
            }
        }

        public async Task<bool> DeleteProfileAsync(int id)
        {
            try
            {
                var profile = await GetProfileByIDAsync(id);
                if (profile == null)
                {
                    _logger.LogWarning($"Attempt to delete profile failed, profile with ID: {id} not found");
                    return false;
                }

                _logger.LogInformation($"Deleting profile with ID: {id}");
                _context.Perfiles.Remove(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting profile with ID: {id}");
                throw;
            }
        }

        public async Task<Profile?> UpdateProfileAsync(Profile profileToUpdate)
        {
            try
            {
                _logger.LogInformation($"Updating profile with ID: {profileToUpdate.Id}");
                var existingProfile = await GetProfileByIDAsync(profileToUpdate.Id);
                if (existingProfile == null)
                {
                    _logger.LogWarning($"Attempt to update failed, profile with ID: {profileToUpdate.Id} not found");
                    return null;
                }

                existingProfile.NombreCompleto = profileToUpdate.NombreCompleto;
                existingProfile.AvatarURL = profileToUpdate.AvatarURL;
                existingProfile.FotoPerfilURL = profileToUpdate.FotoPerfilURL;
                _context.Perfiles.Update(existingProfile);
                await _context.SaveChangesAsync();
                return existingProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating profile with ID: {profileToUpdate.Id}");
                throw;
            }
        }
    }
}
