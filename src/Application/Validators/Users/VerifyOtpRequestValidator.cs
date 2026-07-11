using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Validators.Users
{
    public class VerifyOtpRequestValidator : AbstractValidator<VerifyOtpRequest>
    {
        public VerifyOtpRequestValidator()
        {
            RuleFor(x => x.UserIdentifier).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.Purpose).IsInEnum();
        }
    }
}
