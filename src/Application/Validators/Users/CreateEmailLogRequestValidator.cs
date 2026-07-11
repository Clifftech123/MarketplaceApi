using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Validators.Users
{
    public class CreateEmailLogRequestValidator : AbstractValidator<CreateEmailLogRequest>
    {
        public CreateEmailLogRequestValidator()
        {
            RuleFor(x => x.ToAddress).NotEmpty().EmailAddress().MaximumLength(320);
            RuleFor(x => x.Subject).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}
