using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Abstractions.Auth
{
    public interface IOtpService
    {
        Task<OtpResponse> SendOtpAsync(SendOtpRequest request);
        Task<bool> VerifyOtpAsync(VerifyOtpRequest request);
        Task<UserLoginResponse> LoginWithOtpAsync(LoginWithOtpRequest request);
        Task<LoginStepOneResponse> ValidateCredentialsAndSendOtpAsync(LoginStepOneRequest request);
    }
}
