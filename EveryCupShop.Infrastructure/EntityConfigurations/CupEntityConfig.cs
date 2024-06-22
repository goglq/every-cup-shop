using EveryCupShop.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveryCupShop.Infrastructure.EntityConfigurations;

public class CupEntityConfig : IEntityTypeConfiguration<Cup>
{
    public void Configure(EntityTypeBuilder<Cup> builder)
    {
        builder.HasKey(cup => cup.Id);

        builder
            .HasOne(cup => cup.CupShape)
            .WithMany(shape => shape.Cups)
            .HasForeignKey(cup => cup.CupShapeId);

        builder
            .HasOne(cup => cup.CupAttachment)
            .WithMany(attachment => attachment.Cups)
            .HasForeignKey(cup => cup.CupAttachmentId);
    }
}