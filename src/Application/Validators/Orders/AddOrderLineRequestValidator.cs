using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Orders;

namespace MarketplaceApi.src.Application.Validators.Orders
{
    public class AddOrderLineRequestValidator : AbstractValidator<AddOrderLineRequest>
    {
        public AddOrderLineRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
        }
    }
}
