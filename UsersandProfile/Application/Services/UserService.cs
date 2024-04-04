// Services/PedidoService.cs
using ServicioUsers.Models;
using ServicioUsers.Repositories;
using ServicioUsers.DTOs;
using System.Text.Json;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ServicioUsers.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> RegisterUser(RegisterDto registerDto)
        {
        {
            if(await _userRepository.UserExists(registerDto.Email))
            {
                return "El usuario ya existe.";
            }

            string passworHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passworHash,
                Role = "User",
                RegistrationDate = DateTime.UtcNow,
                EmailConfirmation = false
            };

            await _userRepository.AddUser(user);

            return "Usuario creado con exito";
        }
    }

    public async Task<string?> LoginUser(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByEmail(loginDto.UsernameOrEmail);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return null;
        }

        var token = GenerateJwtToken(user);
        return token;
    }
    private string GenerateJwtToken(User user)
    {
        var jwtKey = _configuration["Jwt:Key"];
        if (jwtKey == null)
        {
            throw new InvalidOperationException("La clave JWT no está configurada.");
        }
        
        // Asegúrate de que la clave tiene una longitud mínima de 32 bytes (256 bits)
        if (jwtKey.Length < 32)
        {
            throw new InvalidOperationException("La clave JWT debe tener al menos 32 caracteres de longitud.");
        }
    
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            }),
            Expires = DateTime.UtcNow.AddDays(1), 
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    }
}
