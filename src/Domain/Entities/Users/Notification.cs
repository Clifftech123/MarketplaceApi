using MarketplaceApi.src.Domain.Common.Entities;
using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Domain.Entities.Users
{
    public sealed class Notification : ISoftDeletable
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
        public Guid UserId { get; private init; }
        public AppUser? User { get; init; }

        public string Title { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public NotificationType Type { get; private init; }
        public Guid? ReferenceId { get; private init; }

        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }
        public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;

        public bool IsDeleted { get; private set; }
        public DateTime? DeletedOnUtc { get; private set; }
        public string? DeletedBy { get; private set; }

        private Notification() { }

        public static Notification Create(Guid userId, string title, string message, NotificationType type, Guid? referenceId = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Notification title is required.", nameof(title));

            return new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = type,
                ReferenceId = referenceId
            };
        }

        public void MarkAsRead()
        {
            if (IsRead) return;

            IsRead = true;
            ReadAt = DateTime.UtcNow;
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
                throw new InvalidOperationException("Notification is not deleted.");

            IsDeleted = false;
            DeletedOnUtc = null;
            DeletedBy = null;
        }
    }
}
