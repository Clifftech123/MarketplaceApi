namespace MarketplaceApi.src.Application.Abstractions.Auth
{
    public interface ICurrentUserService
    {
        public Task<string?> GetCurrentUserIdAsync();
    }
}
