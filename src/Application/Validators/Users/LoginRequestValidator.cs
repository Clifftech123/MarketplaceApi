using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Validators.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
