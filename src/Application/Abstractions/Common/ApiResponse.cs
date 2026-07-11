namespace MarketplaceApi.src.Application.Abstractions.Common
{
    public class ApiResponse
    {
        public bool Success { get; init; }
        public string? Message { get; init; }

        public static ApiResponse Ok(string? message = null) =>
            new() { Success = true, Message = message };

        public static ApiResponse<T> Ok<T>(T data, string? message = null) =>
            new() { Success = true, Data = data, Message = message };
    }

    public sealed class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; init; }
    }
}
