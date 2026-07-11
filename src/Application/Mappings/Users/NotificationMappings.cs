using MarketplaceApi.src.Application.DTOs.Users;
using MarketplaceApi.src.Domain.Entities.Users.Entities;

namespace MarketplaceApi.src.Application.Mappings.Users
{
    public static class NotificationMappings
    {
        public static NotificationResponse ToResponse(this Notification notification) => new()
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Title = notification.Title,
            Message = notification.Message,
            Type = notification.Type,
            ReferenceId = notification.ReferenceId,
            IsRead = notification.IsRead,
            ReadAt = notification.ReadAt,
            CreatedAt = notification.CreatedAt
        };

        public static Notification ToEntity(this CreateNotificationRequest request)
            => Notification.Create(request.UserId, request.Title, request.Message, request.Type, request.ReferenceId);
    }
}
