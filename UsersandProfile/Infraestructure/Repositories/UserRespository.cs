using Microsoft.EntityFrameworkCore;
using UsersandProfile.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace UsersandProfile.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly ApplicationDbContext _context;

        public UserRepository(ILogger<UserRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> Register(User user)
        {
            _logger.LogInformation("Attempting to register a new user.");
            try
            {
                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"User registered successfully with username: {user.Username}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user.");
                throw;  // Rethrow the exception after logging it
            }
        }

        public async Task<bool> Login(UserDto userDto) 
        {
            _logger.LogInformation($"Attempting to log in user: {userDto.Username}");
            try
            {
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == userDto.Username);
                if (user == null)
                {
                    _logger.LogWarning("Login attempt failed: User not found.");
                    return false;
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password);
                _logger.LogInformation(isPasswordValid ? "User logged in successfully." : "Login attempt failed: Incorrect password.");
                return isPasswordValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login attempt.");
                throw;
            }
        }

        public async Task<User?> UserExists(UserDto userDto)
        {
            try
            {
                _logger.LogInformation($"Checking existence for user: {userDto.Username} or email: {userDto.Email}");
                var userByUsername = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == userDto.Username);
                var userByEmail = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == userDto.Email);

                if (userByUsername != null)
                {
                    _logger.LogInformation($"User found by username: {userDto.Username}");
                    return userByUsername;
                }
                if (userByEmail != null)
                {
                    _logger.LogInformation($"User found by email: {userDto.Email}");
                    return userByEmail;
                }
                _logger.LogInformation("No user found by username or email.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user existence.");
                throw;
            }
        }

        public async Task Update(User userToUpdate)
        {
            _logger.LogInformation($"Attempting to update user: {userToUpdate.Username}");
            try
            {
                _context.Usuarios.Update(userToUpdate);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user: {userToUpdate.Username}");
                throw;
            }
        }

        public async Task<User?> GetUserById(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving user by ID: {id}");
                var user = await _context.Usuarios.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning($"User with ID: {id} not found.");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user with ID: {id}");
                throw;
            }
        }
    }
}
