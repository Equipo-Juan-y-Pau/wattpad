using UsersandProfile.Models;

namespace UsersandProfile.Repositories
{
    public interface IUserRepository
    {
        Task<bool> login(UserDto userDto);

        Task<bool> register(User user);

        Task<User?> userExist(UserDto userDto);

        Task<User?> GetUserById(int id);

        Task Update(User userToUpdate);
    }
}