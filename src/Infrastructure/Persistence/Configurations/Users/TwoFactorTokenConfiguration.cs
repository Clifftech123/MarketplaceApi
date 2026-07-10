using MarketplaceApi.src.Domain.Entities.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Users
{
    public sealed class TwoFactorTokenConfiguration : IEntityTypeConfiguration<TwoFactorToken>
    {
        public void Configure(EntityTypeBuilder<TwoFactorToken> builder)
        {
            builder.ToTable("TwoFactorTokens");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasMaxLength(36);

            builder.Property(t => t.UserIdentifier).IsRequired().HasMaxLength(256);
            builder.HasIndex(t => t.UserIdentifier);

            builder.Property(t => t.Secret).IsRequired().HasMaxLength(512);
            builder.Property(t => t.Token).IsRequired().HasMaxLength(10);
            builder.Property(t => t.IssuedAt).IsRequired();
            builder.Property(t => t.ValidTo).IsRequired();
            builder.Property(t => t.Purpose).HasMaxLength(50);
            builder.Property(t => t.IsUsed).HasDefaultValue(false);
            builder.Property(t => t.UsedAt);
            builder.Property(t => t.IpAddress).HasMaxLength(45);
        }
    }
}
