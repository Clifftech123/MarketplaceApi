using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Carts;

namespace MarketplaceApi.src.Application.Validators.Carts
{
    public class UpdateCartItemQuantityRequestValidator : AbstractValidator<UpdateCartItemQuantityRequest>
    {
        public UpdateCartItemQuantityRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
        }
    }
}
