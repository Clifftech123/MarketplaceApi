using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record EmailLogResponse
    {
        public required Guid Id { get; init; }
        public Guid? OrderId { get; init; }
        public required EmailType Type { get; init; }
        public required string ToAddress { get; init; }
        public required string Subject { get; init; }
        public required bool WasSent { get; init; }
        public string? ErrorMessage { get; init; }
        public required DateTime SentAt { get; init; }
    }
}
