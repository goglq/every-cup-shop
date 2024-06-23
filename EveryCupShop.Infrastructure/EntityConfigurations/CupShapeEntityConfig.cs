using EveryCupShop.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveryCupShop.Infrastructure.EntityConfigurations;

public class CupShapeEntityConfig : IEntityTypeConfiguration<CupShape>
{
    public void Configure(EntityTypeBuilder<CupShape> builder)
    {
        builder.HasKey(cupShape => cupShape.Id);
    }
}