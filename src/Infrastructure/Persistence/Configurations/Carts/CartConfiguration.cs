using MarketplaceApi.src.Domain.Entities.Carts.Entities;
using MarketplaceApi.src.Domain.Entities.Carts.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Carts
{
    public sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasConversion(id => id.Value, value => new CartId(value));

            builder.Property(c => c.CustomerId).IsRequired();

            // Cart → AppUser (customer) — one active cart per user
            builder.HasOne<MarketplaceApi.src.Domain.Entities.Users.Entities.AppUser>()
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.CustomerId).IsUnique();

            // Cart → CartItems
            builder.HasMany(c => c.Items)
                .WithOne()
                .HasForeignKey("CartId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
