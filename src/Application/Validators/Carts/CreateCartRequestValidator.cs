using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Carts;

namespace MarketplaceApi.src.Application.Validators.Carts
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartRequestValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
        }
    }
}
