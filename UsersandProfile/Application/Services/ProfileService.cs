// Services/ProfileService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UsersandProfile.Models;
using UsersandProfile.Repositories;

namespace UsersandProfile.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<ProfileService> _logger; // Añade un logger

        public ProfileService(IProfileRepository profileRepository, ILogger<ProfileService> logger)
        {
            _profileRepository = profileRepository;
            _logger = logger; // Inicializa el logger
        }

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            try
            {
                _logger.LogInformation("Fetching all profiles from the database.");
                var profiles = await _profileRepository.GetProfilesAsync();
                return profiles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch profiles.");
                throw; // Re-lanzar la excepción después de loguear
            }
        }

        public async Task<Profile?> GetProfileByID(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching profile with ID: {id}");
                return await _profileRepository.GetProfileByIDAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch profile with ID: {id}");
                throw;
            }
        }

        public async Task<Profile> CreateProfile(Profile profile)
        {
            try
            {
                _logger.LogInformation("Creating a new profile.");
                return await _profileRepository.CreateProfileAsync(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create a profile.");
                throw;
            }
        }

        public async Task<bool> DeleteProfile(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting profile with ID: {id}");
                return await _profileRepository.DeleteProfileAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete profile with ID: {id}");
                throw;
            }
        }

        public async Task<Profile?> UpdateProfile(Profile profileToUpdate)
        {
            try
            {
                _logger.LogInformation($"Updating profile with ID: {profileToUpdate.Id}");
                return await _profileRepository.UpdateProfileAsync(profileToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update profile with ID: {profileToUpdate.Id}");
                throw;
            }
        }
    }
}
