using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Validators.Users
{
    public class SendOtpRequestValidator : AbstractValidator<SendOtpRequest>
    {
        public SendOtpRequestValidator()
        {
            RuleFor(x => x.UserIdentifier).NotEmpty();
            RuleFor(x => x.Purpose).IsInEnum();
        }
    }
}
