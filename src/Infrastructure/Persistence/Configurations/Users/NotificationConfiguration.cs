using MarketplaceApi.src.Domain.Entities.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Users
{
    public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.UserId).IsRequired();

            // Notification → AppUser
            builder.HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(n => n.Title).IsRequired().HasMaxLength(200);
            builder.Property(n => n.Message).IsRequired().HasMaxLength(1000);

            builder.Property(n => n.Type)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(n => n.ReferenceId);
            builder.Property(n => n.IsRead).HasDefaultValue(false);
            builder.Property(n => n.ReadAt);
            builder.Property(n => n.CreatedAt).IsRequired();

            builder.Property(n => n.IsDeleted).HasDefaultValue(false);
            builder.Property(n => n.DeletedOnUtc);
            builder.Property(n => n.DeletedBy).HasMaxLength(256);

            builder.HasQueryFilter(n => !n.IsDeleted);
        }
    }
}
