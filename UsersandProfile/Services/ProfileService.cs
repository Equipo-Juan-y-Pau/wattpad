// ProfileService.cs
using UsersandProfile.Models;
using UsersandProfile.Repositories;
using System;
using System.Collections.Generic;

namespace UsersandProfile.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<IEnumerable<Profile>> GetAll()
        {
            return await _profileRepository.GetAll();
        }

        public async Task<Profile> GetById(int id)
        {
            return await _profileRepository.GetById(id);
        }

        public async Task<Profile> Add(Profile profile)
        {
            return await _profileRepository.Add(profile);
        }

        public async Task<Profile> Update(int id, Profile profile)
        {
            return await _profileRepository.Update(profile);
        }

        public async Task<bool> Delete(int id)
        {
            return await _profileRepository.Delete(id);
        }
    }
}
