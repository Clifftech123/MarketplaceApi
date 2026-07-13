using MarketplaceApi.src.Application.DTOs.Users;
using MarketplaceApi.src.Domain.Entities.Orders.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Users.Entities;

namespace MarketplaceApi.src.Application.Mappings.Users
{
    public static class EmailLogMappings
    {
        public static EmailLogResponse ToResponse(this EmailLog log) => new()
        {
            Id = log.Id.Value,
            OrderId = log.OrderId?.Value,
            Type = log.Type,
            ToAddress = log.ToAddress,
            Subject = log.Subject,
            WasSent = log.WasSent,
            ErrorMessage = log.ErrorMessage,
            SentAt = log.SentAt
        };

        public static EmailLog ToEntity(this CreateEmailLogRequest request)
            => EmailLog.Create(
                request.ToAddress,
                request.Subject,
                request.Type,
                request.OrderId.HasValue ? new OrderId(request.OrderId.Value) : null);
    }
}
