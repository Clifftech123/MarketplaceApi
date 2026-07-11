namespace MarketplaceApi.src.Application.Abstractions.Common
{
    public record ApiResponse
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public string? RequestId { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;

        public static ApiResponse Ok(string? message = null) =>
            new() { Success = true, Message = message };

        public static ApiResponse<T> Ok<T>(T data, string? message = null) =>
            new() { Success = true, Data = data, Message = message };
    }

    public sealed record ApiResponse<T> : ApiResponse
    {
        public T? Data { get; init; }
    }
}
