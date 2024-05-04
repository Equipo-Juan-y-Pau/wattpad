using UsersandProfile.Models;

namespace UsersandProfile.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Login(UserDto userDto);

        Task<bool> Register(User user);

        Task<User?> UserExists(UserDto userDto);

        Task<User?> GetUserById(int id);

        Task Update(User userToUpdate);
        
        Task<User?> GetUserByUsername(string username);
    Task<User?> GetUserByEmail(string email);
    }
}