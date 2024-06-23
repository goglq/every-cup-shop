using EveryCupShop.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveryCupShop.Infrastructure.EntityConfigurations;

public class CupAttachmentEntityConfig : IEntityTypeConfiguration<CupAttachment>
{
    public void Configure(EntityTypeBuilder<CupAttachment> builder)
    {
        builder.HasKey(cupAttachment => cupAttachment.Id);
    }
}