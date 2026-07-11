namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record UserResponse
    {
        public required Guid Id { get; init; }
        public required string Email { get; init; }
        public required string FullName { get; init; }
        public required bool IsActive { get; init; }
        public required bool NotifyOnOrderPlaced { get; init; }
        public required bool NotifyOnOrderShipped { get; init; }
        public required bool NotifyOnPriceDrop { get; init; }
        public required bool NotifyWeeklySummary { get; init; }
        public required DateTime CreatedAt { get; init; }
        public DateTime? LastLoginAt { get; init; }
    }
}
