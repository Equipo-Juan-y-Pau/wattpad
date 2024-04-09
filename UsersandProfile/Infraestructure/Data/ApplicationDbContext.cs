using Microsoft.EntityFrameworkCore;
using UsersandProfile.Models;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) {}

    public DbSet<Profile> Perfiles {get;set;}

    public DbSet<User> Usuarios {get;set;}
    
}