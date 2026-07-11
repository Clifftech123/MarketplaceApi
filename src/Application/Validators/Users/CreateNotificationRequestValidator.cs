using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Validators.Users
{
    public class CreateNotificationRequestValidator : AbstractValidator<CreateNotificationRequest>
    {
        public CreateNotificationRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Message).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}
