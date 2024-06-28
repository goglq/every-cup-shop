using EveryCupShop.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveryCupShop.Infrastructure.EntityConfigurations;

public class CupEntityConfig : IEntityTypeConfiguration<Cup>
{
    public void Configure(EntityTypeBuilder<Cup> builder)
    {
        builder.HasKey(cup => cup.Id);

        builder.Property(cup => cup.Name)
            .IsRequired();

        builder.Property(cup => cup.Price)
            .IsRequired();
    }
}