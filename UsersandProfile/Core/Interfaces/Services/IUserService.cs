using UsersandProfile.Models;

namespace UsersandProfile.Services
{
    public interface IUserService 
    {
        Task<ServiceResponse<string>> Login(LoginRequestDto loginRequest);

        Task<ServiceResponse<string>> Register(UserDto userDto);

        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);

    }

}
