using MarketplaceApi.src.Domain.Entities.Categories.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Products.Entities;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Stores.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Products
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, value => new ProductId(value));

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(2000);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.StockQuantity).IsRequired();

            builder.Property(p => p.CategoryId)
                .HasConversion(id => id.Value, value => new CategoryId(value))
                .IsRequired();

            builder.Property(p => p.StoreId)
                .HasConversion(id => id.Value, value => new StoreId(value))
                .IsRequired();

            // Product → Category
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product → Store
            builder.HasOne(p => p.Store)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product → ProductImages
            builder.HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product → ProductTags
            builder.HasMany(p => p.Tags)
                .WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.DeletedOnUtc);
            builder.Property(p => p.DeletedBy).HasMaxLength(256);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
