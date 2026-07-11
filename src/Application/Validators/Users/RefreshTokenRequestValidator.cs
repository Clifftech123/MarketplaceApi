using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Validators.Users
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
