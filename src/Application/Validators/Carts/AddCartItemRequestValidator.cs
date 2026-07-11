using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Carts;

namespace MarketplaceApi.src.Application.Validators.Carts
{
    public class AddCartItemRequestValidator : AbstractValidator<AddCartItemRequest>
    {
        public AddCartItemRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
        }
    }
}
