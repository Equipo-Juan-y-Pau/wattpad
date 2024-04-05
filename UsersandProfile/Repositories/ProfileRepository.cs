using UsersandProfile.Models;
using UsersandProfile.Datos;
using System;
using System.Collections.Generic;

namespace UsersandProfile.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        //private readonly List<Profile> _profiles;
        private int _nextId = 3;
        /*
        public ProfileRepository()
        {
            // Inicializa la lista con algunos datos de ejemplo.
            _profiles = new List<Profile>
            {
                new Profile { Id = _nextId++, Nombre = "Paula", Avatar = "avatar1.png" },
                new Profile { Id = _nextId++, Nombre = "Juan", Avatar = "avatar2.png" },
                new Profile { Id = _nextId++, Nombre = "Mimosa", Avatar = "avatar3.png" }
            };
        }
        */

        public IEnumerable<Profile> GetAll()
        {
            //return _profiles;
            return (IEnumerable<Profile>)ProfileStore.profileList;
        }

        public Profile GetById(int id)
        {
            //return _profiles.Find(p => p.Id == id);
            return ProfileStore.profileList.Find(p => p.Id == id);
        }

        public Profile Add(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            profile.Id = _nextId++;
            ProfileStore.profileList.Add(profile);
            return profile;
        }

        public Profile Update(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            var existingProfile = ProfileStore.profileList.Find(p => p.Id == profile.Id);
            if (existingProfile == null)
            {
                throw new KeyNotFoundException("El perfil no existe");
            }

            existingProfile.Nombre = profile.Nombre;
            existingProfile.Avatar = profile.Avatar;

            return existingProfile;
        }

        public void Delete(int id)
        {
            var profile = ProfileStore.profileList.Find(p => p.Id == id);
            if (profile == null)
            {
                throw new KeyNotFoundException("El perfil no existe");
            }


            ProfileStore.profileList.Remove(profile);
        }
    }
}
