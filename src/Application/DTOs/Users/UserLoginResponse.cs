namespace MarketplaceApi.src.Application.DTOs.Users
{
    public record UserLoginResponse(Guid UserId, string Email, string FullName);
}
