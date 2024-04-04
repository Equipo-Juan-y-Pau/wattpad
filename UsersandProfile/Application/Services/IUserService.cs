using ServicioUsers.Models;
using ServicioUsers.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicioUsers.Services
{
    public interface IUserService
    {
        Task<string> RegisterUser(RegisterDto registerDto);
        Task<string?> LoginUser(LoginDto loginDto);
    }

    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string htmlContent);
    }
}
