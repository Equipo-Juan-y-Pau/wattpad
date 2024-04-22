using Microsoft.EntityFrameworkCore;
using UsersandProfile.Models;

namespace UsersandProfile.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly ApplicationDbContext _user;

        public UserRepository(ILogger<UserRepository> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _user = db;
        }

        public async Task<bool> register(User user)
        {
            _logger.LogInformation("Entro al repositorio");
            _user.Set<User>().Add(user);
            await _user.SaveChangesAsync();
            return true;
        }

        public async Task<bool> login(UserDto userDto) 
        {
            var user = await _user.Usuarios.FirstOrDefaultAsync(u => u.Username == userDto.Username);
            
            if (user == null)
            {
                return false; 
            }

            return BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password);
        }

        public async Task<User?> userExist(UserDto userDto)
        {
            var userByUsername = await _user.Usuarios.FirstOrDefaultAsync(u => u.Username == userDto.Username);
            var userByEmail = await _user.Usuarios.FirstOrDefaultAsync(u => u.Email == userDto.Email);
    
            if (userByUsername != null)
                return userByUsername;
    
            if (userByEmail != null)
                return userByEmail;
    
            return null;
        }

        public async Task Update(User userToUpdate)
        {
            _user.Usuarios.Update(userToUpdate);
            await _user.SaveChangesAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _user.Usuarios.FindAsync(id);
        }

    }
}