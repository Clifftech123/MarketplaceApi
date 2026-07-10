using MarketplaceApi.src.Domain.Entities.Orders.Entities;
using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Orders
{
    public sealed class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.ToTable("OrderLines");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                .HasConversion(id => id.Value, value => new OrderLineId(value));

            builder.Property(l => l.ProductId)
                .HasConversion(id => id.Value, value => new ProductId(value))
                .IsRequired();

            builder.Property(l => l.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(l => l.Quantity).IsRequired();

            // Shadow FK for Order (configured in OrderConfiguration)
            builder.Property<Guid>("OrderId");
        }
    }
}
