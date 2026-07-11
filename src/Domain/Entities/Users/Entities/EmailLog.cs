using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Domain.Entities.Users.Entities
{
    public sealed class EmailLog : ISoftDeletable, IAuditableEntity
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
        public OrderId? OrderId { get; private init; }
        public EmailType Type { get; private init; }
        public string ToAddress { get; private init; } = string.Empty;
        public string Subject { get; private init; } = string.Empty;
        public bool WasSent { get; private set; }
        public string? ErrorMessage { get; private set; }
        public DateTime SentAt { get; private init; } = DateTime.UtcNow;

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletedOnUtc { get; private set; }
        public string? DeletedBy { get; private set; }

        private EmailLog() { }

        public static EmailLog Create(string toAddress, string subject, EmailType type, OrderId? orderId = null)
        {
            if (string.IsNullOrWhiteSpace(toAddress))
                throw new ArgumentException("Recipient address is required.", nameof(toAddress));

            return new EmailLog
            {
                ToAddress = toAddress,
                Subject = subject,
                Type = type,
                OrderId = orderId
            };
        }

        public void MarkSent()
        {
            WasSent = true;
            ErrorMessage = null;
        }

        public void MarkFailed(string errorMessage)
        {
            WasSent = false;
            ErrorMessage = errorMessage;
        }

        public void Delete(string? deletedBy = null)
        {
            if (IsDeleted) return;

            IsDeleted = true;
            DeletedOnUtc = DateTime.UtcNow;
            DeletedBy = deletedBy;
        }

        public void Restore()
        {
            if (!IsDeleted)
                throw new InvalidOperationException("EmailLog is not deleted.");

            IsDeleted = false;
            DeletedOnUtc = null;
            DeletedBy = null;
        }
    }
}
