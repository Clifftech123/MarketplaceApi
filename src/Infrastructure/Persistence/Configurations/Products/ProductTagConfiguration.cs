using MarketplaceApi.src.Domain.Entities.Products.Entities;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Products
{
    public sealed class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.ToTable("ProductTags");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasConversion(id => id.Value, value => new TagId(value));

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .HasMaxLength(500);

            builder.Property(t => t.ProductId)
                .HasConversion(id => id.Value, value => new ProductId(value))
                .IsRequired();
        }
    }
}
