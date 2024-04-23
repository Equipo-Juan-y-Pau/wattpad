using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
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

        public async Task<ServiceResponse<string>> Register(UserDto userDto)
        {
            var response = new ServiceResponse<string>();
            _logger.LogInformation("Attempting to register a new user.");

            try 
            {
                if (await _userRepository.UserExists(userDto)!= null)
                {
                    _logger.LogWarning("Registration failed: User or email already exists.");
                    response.Success = false;
                    response.Message = "Usuario o email ya existen";
                    return response;
                }

                var user = new User
                {  
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    Role = "user",
                    Date = DateTime.UtcNow,
                    Valid = false
                };

                await _userRepository.Register(user);
                _logger.LogInformation($"User {user.Username} registered successfully.");
                response.Success = true;
                response.Message = "Usuario registrado.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user.");
                response.Success = false;
                response.Message = $"Error during registration: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> Login(UserDto userDto)
        {
            var response = new ServiceResponse<string>();
            _logger.LogInformation("Attempting user login.");

            try
            {
                var user = await _userRepository.UserExists(userDto);
                if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
                {
                    _logger.LogWarning("Login failed: Incorrect username or password.");
                    response.Success = false;
                    response.Message = "Contraseña incorrecta o usuario no encontrado.";
                    return response;
                }

                var token = GenerateJWTToken(user);
                response.Success = true;
                response.Data = token;
                _logger.LogInformation("User logged in successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login.");
                response.Success = false;
                response.Message = $"Login error: {ex.Message}";
            }

            return response;
        }

        private string GenerateJWTToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                _logger.LogError("JWT key configuration is missing.");
                throw new InvalidOperationException("La clave JWT no está configurada correctamente.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? "Desconocido"),
                new Claim(ClaimTypes.Email, user.Email?? "CorreoDesconocido@email.com")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            _logger.LogInformation($"Attempting to change password for user ID {userId}.");
            var user = await _userRepository.GetUserById(userId);
            if (user == null || !BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, user.Password))
            {
                _logger.LogWarning("Change password failed: User not found or current password is incorrect.");
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            await _userRepository.Update(user);
            _logger.LogInformation("Password changed successfully.");
            return true;
        }
    }
}
