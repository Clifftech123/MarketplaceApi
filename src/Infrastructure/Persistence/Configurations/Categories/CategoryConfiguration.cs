using MarketplaceApi.src.Domain.Entities.Categories.Entities;
using MarketplaceApi.src.Domain.Entities.Categories.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Categories
{
    public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasConversion(id => id.Value, value => new CategoryId(value));

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.DeletedOnUtc);
            builder.Property(c => c.DeletedBy).HasMaxLength(256);

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
