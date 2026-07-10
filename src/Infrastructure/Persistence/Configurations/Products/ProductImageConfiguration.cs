using MarketplaceApi.src.Domain.Entities.Products.Entities;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Products
{
    public sealed class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasConversion(id => id.Value, value => new ImageId(value));

            builder.Property(i => i.Url)
                .IsRequired()
                .HasMaxLength(2048);

            builder.Property(i => i.AltText)
                .HasMaxLength(300);

            builder.Property(i => i.ProductId)
                .HasConversion(id => id.Value, value => new ProductId(value))
                .IsRequired();
        }
    }
}
