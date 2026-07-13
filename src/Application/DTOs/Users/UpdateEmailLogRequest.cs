namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record UpdateEmailLogRequest
    {
        public required bool WasSent { get; init; }

        public string? ErrorMessage { get; init; }
    }
}
