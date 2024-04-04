using System.Collections.Generic;
using System.Threading.Tasks;
using ServicioUsers.Models; 
public interface IUserRepository
{
    Task AddUser(User user);
    Task<bool> UserExists(string email);
    Task<User?> GetUserByEmail(string email);

}
