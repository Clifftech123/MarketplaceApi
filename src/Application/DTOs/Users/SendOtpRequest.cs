using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Application.DTOs.Users
{
    public record SendOtpRequest(string UserIdentifier, OtpPurpose Purpose);
}
