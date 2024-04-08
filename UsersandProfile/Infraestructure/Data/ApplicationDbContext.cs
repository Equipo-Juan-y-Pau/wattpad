using Microsoft.EntityFrameworkCore;
using ServicioProfiles.Models;
using ServicioUsers.Models;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) {}

    public DbSet<Profile> Perfiles {get;set;}
    public DbSet<User> Users {get;set;}
    
}