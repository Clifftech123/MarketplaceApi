using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Validators.Users
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(320);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(100);
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(150);
        }
    }
}
