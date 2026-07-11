using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Products;

namespace MarketplaceApi.src.Application.Validators.Products
{
    public class AdjustProductStockRequestValidator : AbstractValidator<AdjustProductStockRequest>
    {
        public AdjustProductStockRequestValidator()
        {
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
        }
    }
}
