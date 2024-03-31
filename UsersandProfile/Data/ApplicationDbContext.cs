using Microsoft.EntityFrameworkCore;
using ServicioProfiles.Models;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) {}

    public DbSet<Profile> Perfiles {get;set;}
    
}