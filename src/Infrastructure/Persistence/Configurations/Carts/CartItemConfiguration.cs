using MarketplaceApi.src.Domain.Entities.Carts.Entities;
using MarketplaceApi.src.Domain.Entities.Carts.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Carts
{
    public sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasConversion(id => id.Value, value => new CartItemId(value));

            builder.Property(i => i.ProductId)
                .HasConversion(id => id.Value, value => new ProductId(value))
                .IsRequired();

            builder.Property(i => i.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(i => i.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.Quantity).IsRequired();

            // Shadow FK for Cart (configured in CartConfiguration)
            builder.Property<Guid>("CartId");
        }
    }
}
