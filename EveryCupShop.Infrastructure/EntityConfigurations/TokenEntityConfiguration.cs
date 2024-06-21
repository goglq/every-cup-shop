using EveryCupShop.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveryCupShop.Infrastructure.EntityConfigurations;

public class TokenEntityConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.HasKey(token => token.Id);

        builder
            .Property(token => token.UserId)
            .IsRequired();

        builder
            .HasOne(token => token.User)
            .WithOne(user => user.Token)
            .HasForeignKey<Token>(token => token.UserId);
        
        builder
            .HasIndex(token => token.RefreshToken)
            .IsUnique();

        builder
            .Property(token => token.RefreshToken)
            .IsRequired();
    }
}