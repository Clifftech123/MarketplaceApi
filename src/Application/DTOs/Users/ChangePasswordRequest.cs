namespace MarketplaceApi.src.Application.DTOs.Users
{
    public record class ChangePasswordRequest(string CurrentPassword,
        string NewPassword
    );

}
