using EveryCupShop.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveryCupShop.Infrastructure.EntityConfigurations;

public class OrderItemEntityConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(orderFinalCup => orderFinalCup.Id);

        builder
            .HasOne(item => item.Order)
            .WithMany(order => order.OrderItems)
            .HasForeignKey(item => item.OrderId);

        builder
            .HasOne(item => item.CupAttachment)
            .WithMany(attachment => attachment.OrderItems)
            .HasForeignKey(item => item.CupAttachmentId);

        builder
            .HasOne(item => item.CupShape)
            .WithMany(shape => shape.OrderItems)
            .HasForeignKey(item => item.CupShapeId);
    }
}