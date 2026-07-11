using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record CreateNotificationRequest
    {
        public required Guid UserId { get; init; }

        public required string Title { get; init; }

        public required string Message { get; init; }

        public required NotificationType Type { get; init; }

        public Guid? ReferenceId { get; init; }
    }
}
