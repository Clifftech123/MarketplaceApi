using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record CreateEmailLogRequest
    {
        public required string ToAddress { get; init; }

        public required string Subject { get; init; }

        public required EmailType Type { get; init; }

        public Guid? OrderId { get; init; }
    }
}
