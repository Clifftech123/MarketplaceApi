using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Hubs
{
    /// <summary>
    /// Strongly-typed contract for methods the server invokes on connected clients.
    /// </summary>
    public interface INotificationHub
    {
        Task ReceiveNotification(NotificationResponse notification);
    }
}
