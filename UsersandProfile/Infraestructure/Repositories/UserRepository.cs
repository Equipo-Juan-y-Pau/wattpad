using Microsoft.EntityFrameworkCore;
using ServicioUsers.Models;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ServicioUsers.Repositories
{   
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string usernameOrEmail)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == usernameOrEmail || u.Username == usernameOrEmail);
        }

        public async Task<User?> ChangePasswordAsync(string usernameOrEmail)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == usernameOrEmail || u.Username == usernameOrEmail);
        }
    }
}
