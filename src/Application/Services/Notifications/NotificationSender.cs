using MarketplaceApi.src.Application.Abstractions.Notifications;
using MarketplaceApi.src.Application.DTOs.Users;
using MarketplaceApi.src.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace MarketplaceApi.src.Application.Services.Notifications
{
    public class NotificationSender(IHubContext<NotificationHub, INotificationHub> hubContext) : INotificationSender
    {
        public Task SendToUserAsync(Guid userId, NotificationResponse notification, CancellationToken cancellationToken = default)
            => hubContext.Clients.Group(NotificationHub.GroupName(userId.ToString()))
                .ReceiveNotification(notification);
    }
}
