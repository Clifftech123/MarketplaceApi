using MarketplaceApi.src.Domain.Entities.Stores.Entities;
using MarketplaceApi.src.Domain.Entities.Stores.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Stores
{
    public sealed class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Stores");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasConversion(id => id.Value, value => new StoreId(value));

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.Description)
                .HasMaxLength(1000);

            builder.Property(s => s.OwnerId).IsRequired();

            // Store → AppUser (owner)
            builder.HasOne(s => s.Owner)
                .WithMany()
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.DeletedOnUtc);
            builder.Property(s => s.DeletedBy).HasMaxLength(256);

            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}
