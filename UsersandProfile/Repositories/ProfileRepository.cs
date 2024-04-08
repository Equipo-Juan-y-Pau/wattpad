using UsersandProfile.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace UsersandProfile.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _profile;

        public ProfileRepository(ApplicationDbContext bd)
        {
            _profile = bd;
        }
        

        public async Task<IEnumerable<Profile>> GetAll()
        {
            return await _profile.Perfiles.ToListAsync();
        }

        public async Task<Profile> GetById(int id)
        {
            var perfil = await _profile.Perfiles.FindAsync(id);
            if (perfil != null)
            {
                return await _profile.Perfiles.FindAsync(id);
            }
            throw new KeyNotFoundException($"No se encontró un perfil con el ID: {id}");
        }

        public async Task<Profile> Add(Profile profile)
        {

            _profile.Set<Profile>().Add(profile);
            await _profile.SaveChangesAsync();
            return profile;

        }

        public async Task<Profile> Update(Profile profile)
        {
            _profile.Entry(profile).State = EntityState.Modified;
            await _profile.SaveChangesAsync();
            return  profile;
        }

        public async Task<bool> Delete(int id)
        {
            var perfil = await _profile.Perfiles.FindAsync(id);
            if (perfil != null)
            {
                _profile.Perfiles.Remove(perfil);
                await _profile.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
