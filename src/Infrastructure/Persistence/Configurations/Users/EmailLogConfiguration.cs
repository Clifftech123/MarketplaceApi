using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Users
{
    public sealed class EmailLogConfiguration : IEntityTypeConfiguration<EmailLog>
    {
        public void Configure(EntityTypeBuilder<EmailLog> builder)
        {
            builder.ToTable("EmailLogs");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.OrderId)
                .HasConversion(
                    id => id == null ? (Guid?)null : id.Value,
                    value => value == null ? null : new OrderId(value.Value));

            builder.Property(e => e.Type)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.ToAddress).IsRequired().HasMaxLength(256);
            builder.Property(e => e.Subject).IsRequired().HasMaxLength(500);
            builder.Property(e => e.WasSent);
            builder.Property(e => e.ErrorMessage).HasMaxLength(2000);
            builder.Property(e => e.SentAt).IsRequired();

            builder.Property(e => e.IsDeleted).HasDefaultValue(false);
            builder.Property(e => e.DeletedOnUtc);
            builder.Property(e => e.DeletedBy).HasMaxLength(256);

            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
