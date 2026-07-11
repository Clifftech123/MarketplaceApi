using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Application.DTOs.Users
{
    public record VerifyOtpRequest(string UserIdentifier, string Token, OtpPurpose Purpose);
}
