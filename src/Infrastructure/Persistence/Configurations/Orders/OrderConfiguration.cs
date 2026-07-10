using MarketplaceApi.src.Domain.Entities.Orders.Entities;
using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Orders
{
    public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasConversion(id => id.Value, value => new OrderId(value));

            builder.Property(o => o.CustomerId).IsRequired();

            // Order → AppUser (customer)
            builder.HasOne<MarketplaceApi.src.Domain.Entities.Users.Entities.AppUser>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            // ShippingAddress owned type
            builder.OwnsOne(o => o.ShippingAddress, sa =>
            {
                sa.Property(a => a.Line1).HasColumnName("ShippingLine1").IsRequired().HasMaxLength(200);
                sa.Property(a => a.Line2).HasColumnName("ShippingLine2").HasMaxLength(200);
                sa.Property(a => a.City).HasColumnName("ShippingCity").IsRequired().HasMaxLength(100);
                sa.Property(a => a.PostalCode).HasColumnName("ShippingPostalCode").IsRequired().HasMaxLength(20);
                sa.Property(a => a.Country).HasColumnName("ShippingCountry").IsRequired().HasMaxLength(100);
            });

            // Order → OrderLines
            builder.HasMany(o => o.Lines)
                .WithOne()
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
