using MarketplaceApi.src.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record CreateNotificationRequest
    {
        [Required]
        public required Guid UserId { get; init; }

        [Required, StringLength(150)]
        public required string Title { get; init; }

        [Required, StringLength(1000)]
        public required string Message { get; init; }

        [Required]
        public required NotificationType Type { get; init; }

        public Guid? ReferenceId { get; init; }
    }
}
