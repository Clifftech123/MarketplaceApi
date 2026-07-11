using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Validators.Users
{
    public class LoginWithOtpRequestValidator : AbstractValidator<LoginWithOtpRequest>
    {
        public LoginWithOtpRequestValidator()
        {
            RuleFor(x => x.UserIdentifier).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
