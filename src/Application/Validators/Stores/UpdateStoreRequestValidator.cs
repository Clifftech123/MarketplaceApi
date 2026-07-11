using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Stores;

namespace MarketplaceApi.src.Application.Validators.Stores
{
    public class UpdateStoreRequestValidator : AbstractValidator<UpdateStoreRequest>
    {
        public UpdateStoreRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Description).MaximumLength(1000);
        }
    }
}
