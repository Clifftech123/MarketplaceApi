using MarketplaceApi.src.Domain.Entities.Orders.Entities;
using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Payments.Entities;
using MarketplaceApi.src.Domain.Entities.Payments.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceApi.src.Infrastructure.Persistence.Configurations.Payments
{
    public sealed class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.ToTable("PaymentTransactions");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, value => new PaymentTransactionId(value));

            builder.Property(p => p.OrderId)
                .HasConversion(id => id.Value, value => new OrderId(value))
                .IsRequired();

            // PaymentTransaction → Order
            builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.StripeTransactionId).IsRequired().HasMaxLength(255);
            builder.Property(p => p.StripePaymentIntentId).HasMaxLength(255);
            builder.Property(p => p.StripeChargeId).HasMaxLength(255);
            builder.Property(p => p.StripeInvoiceId).HasMaxLength(255);
            builder.Property(p => p.StripeRefundId).HasMaxLength(255);

            builder.Property(p => p.AmountPence).IsRequired();
            builder.Property(p => p.Currency).IsRequired().HasMaxLength(10);

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Method)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.TransactionDate).IsRequired();
            builder.Property(p => p.StripeResponse).HasColumnType("nvarchar(max)");
            builder.Property(p => p.FailureReason).HasMaxLength(1000);
            builder.Property(p => p.CreatedBy).HasMaxLength(256);
            builder.Property(p => p.UpdatedBy).HasMaxLength(256);

            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.DeletedOnUtc);
            builder.Property(p => p.DeletedBy).HasMaxLength(256);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
