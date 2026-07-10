using MarketplaceApi.src.Infrastructure.Persistence.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Audit
{
    public sealed class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail>
    {
        public void Configure(EntityTypeBuilder<AuditTrail> builder)
        {
            builder.ToTable("AuditTrails");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.UserId);

            // AuditTrail → AppUser (optional)
            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(a => a.TrailType)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(a => a.DateUtc).IsRequired();
            builder.Property(a => a.EntityName).IsRequired().HasMaxLength(200);
            builder.Property(a => a.PrimaryKey).HasMaxLength(100);

            builder.Property(a => a.OldValues)
                .HasConversion(
                    d => JsonSerializer.Serialize(d, (JsonSerializerOptions?)null),
                    s => JsonSerializer.Deserialize<Dictionary<string, object?>>(s, (JsonSerializerOptions?)null)!)
                .HasColumnType("nvarchar(max)");

            builder.Property(a => a.NewValues)
                .HasConversion(
                    d => JsonSerializer.Serialize(d, (JsonSerializerOptions?)null),
                    s => JsonSerializer.Deserialize<Dictionary<string, object?>>(s, (JsonSerializerOptions?)null)!)
                .HasColumnType("nvarchar(max)");

            builder.Property(a => a.ChangedColumns)
                .HasConversion(
                    l => JsonSerializer.Serialize(l, (JsonSerializerOptions?)null),
                    s => JsonSerializer.Deserialize<List<string>>(s, (JsonSerializerOptions?)null)!)
                .HasColumnType("nvarchar(max)");
        }
    }
}
