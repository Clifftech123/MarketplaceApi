using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Abstractions.Notifications
{
    public interface INotificationSender
    {
        Task SendToUserAsync(Guid userId, NotificationResponse notification, CancellationToken cancellationToken = default);
    }
}
