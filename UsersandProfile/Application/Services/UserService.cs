using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UsersandProfile.Models;
using UsersandProfile.Repositories;

namespace UsersandProfile.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _userRepository = userRepository;
        }


        public async Task<ServiceResponse<string>> register(UserDto userDto)
        {
            var response = new ServiceResponse<string>();

            try 
            {
                _logger.LogInformation("Entro al servicio");
                var exist = await _userRepository.userExist(userDto);
                if (exist != null)
                {
                    _logger.LogInformation("Entro al servicio1");
                    response.Success = false;
                    response.Message = $"Usuario o email ya existen";
                    return response;
                }

                
                
                _logger.LogInformation("Entro al servicio2");
                var user = new User
                {  
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    Role = "user",
                    Date = DateTime.UtcNow, 
                    Valid = false
                };
                await _userRepository.register(user);
                response.Success = true;
                response.Message = "Usuario registrado.";
                
            
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Hubo un error: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> login(UserDto userDto)
        {
            var response = new ServiceResponse<string>();

            try
            {
                bool isPasswordValid = await _userRepository.login(userDto);

                if (isPasswordValid)
                {
                    var user = await _userRepository.userExist(userDto);
                    var token = GenerateJWTToken(user);

                    response.Success = true;
                    response.Data = token;
                }

            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Hubo un error: {ex.Message}";
            }
            return response;
        }

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username!),
                new Claim(ClaimTypes.Email, user.Email!)

            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:issuer"],
                audience: _configuration["Jwt:audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool>ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user ==null || !BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, user.Password))
            {
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            await _userRepository.Update(user);
            return true;
        }
    }
}
