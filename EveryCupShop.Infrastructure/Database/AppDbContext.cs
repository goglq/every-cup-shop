using EveryCupShop.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EveryCupShop.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Token> Tokens => Set<Token>();
    public DbSet<Cup> Cups => Set<Cup>();
    public DbSet<CupShape> CupShapes => Set<CupShape>();
    public DbSet<CupAttachment> CupAttachments => Set<CupAttachment>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var configurationTypes = typeof(AppDbContext).Assembly
            .GetTypes()
            .Where(type => type.GetInterfaces().Any(interfaceType =>
                interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

        foreach (var configurationType in configurationTypes)
        {
            dynamic configurationInstance = Activator.CreateInstance(configurationType) ?? throw new InvalidOperationException();
            builder.ApplyConfiguration(configurationInstance);
        }
        
        SeedRoles(builder);
    }

    private void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<Role>()
            .HasData(
                new Role { Id = Guid.Parse("fb314355-326b-4f6f-9a7b-a2cde7a0351b"), Name = "Admin" }, 
                new Role { Id = Guid.Parse("db2f19c9-5c05-4e64-95e0-626679e8410b"), Name = "User" });
    }
}