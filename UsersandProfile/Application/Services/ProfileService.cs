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
        private readonly ILogger<ProfileService> _logger; 

        public ProfileService(IProfileRepository profileRepository, ILogger<ProfileService> logger)
        {
            _profileRepository = profileRepository;
            _logger = logger; 
        }

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los perfiles de la base de datos.");
                var profiles = await _profileRepository.GetProfilesAsync();
                return profiles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los perfiles.");
                throw; 
            }
        }

        public async Task<Profile?> GetProfileByID(int id)
        {
            try
            {
                _logger.LogInformation($"Obteniendo el perfil con ID: {id}");
                return await _profileRepository.GetProfileByIDAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el perfil con ID: {id}");
                throw;
            }
        }

        public async Task<Profile> CreateProfile(Profile profile)
        {
            try
            {
                _logger.LogInformation("Creando un nuevo perfil.");
                return await _profileRepository.CreateProfileAsync(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el perfil.");
                throw;
            }
        }

        public async Task<bool> DeleteProfile(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando el perfil con ID: {id}");
                return await _profileRepository.DeleteProfileAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el perfil con ID: {id}");
                throw;
            }
        }

        public async Task<Profile?> UpdateProfile(Profile profileToUpdate)
        {
            try
            {
                _logger.LogInformation($"Actualizando el perfil con ID: {profileToUpdate.Id}");
                return await _profileRepository.UpdateProfileAsync(profileToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el perfil con ID: {profileToUpdate.Id}");
                throw;
            }
        }
    }
}
