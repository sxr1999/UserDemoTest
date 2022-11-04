using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserMgr.Domain;

namespace UserMgr.infrastracture;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserLoginHistory> UserLoginHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}