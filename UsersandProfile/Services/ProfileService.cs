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

        public IEnumerable<Profile> GetAll()
        {
            return _profileRepository.GetAll();
        }

        public Profile GetById(int id)
        {
            return _profileRepository.GetById(id);
        }

        public Profile Add(Profile profile)
        {

            return _profileRepository.Add(profile);
        }

        public Profile Update(int id, Profile profile)
        {

            return _profileRepository.Update(profile);
        }

        public void Delete(int id)
        {
            _profileRepository.Delete(id);
        }
    }
}
