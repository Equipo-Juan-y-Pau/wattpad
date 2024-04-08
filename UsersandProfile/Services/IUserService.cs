using UsersandProfile.Models;

namespace UsersandProfile.Services
{
    public interface IUserService 
    {
        Task<ServiceResponse<string>> login(UserDto userDto);

        Task<ServiceResponse<string>> register(UserDto userDto);

    }

}
