using EveryCupShop.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveryCupShop.Infrastructure.EntityConfigurations;

public class RoleEntityConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Name)
            .IsUnique();

        builder.Property(e => e.Name)
            .IsRequired();

        builder
            .HasMany(e => e.Users)
            .WithMany(e => e.Roles);
    }
}