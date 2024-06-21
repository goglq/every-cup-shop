using EveryCupShop.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EveryCupShop.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

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
    }
}