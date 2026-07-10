using MarketplaceApi.src.Domain.Entities.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Users
{
    public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.UserId).IsRequired();

            // RefreshToken → AppUser
            builder.HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(r => r.TokenHash).IsRequired().HasMaxLength(128);
            builder.HasIndex(r => r.TokenHash).IsUnique();

            builder.Property(r => r.ExpiresAt).IsRequired();
            builder.Property(r => r.CreatedAt).IsRequired();
            builder.Property(r => r.IsRevoked).HasDefaultValue(false);
            builder.Property(r => r.RevokedAt);
        }
    }
}
