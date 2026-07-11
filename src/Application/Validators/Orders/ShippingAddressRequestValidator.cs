using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Orders;

namespace MarketplaceApi.src.Application.Validators.Orders
{
    public class ShippingAddressRequestValidator : AbstractValidator<ShippingAddressRequest>
    {
        public ShippingAddressRequestValidator()
        {
            RuleFor(x => x.Line1).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Line2).MaximumLength(200);
            RuleFor(x => x.City).NotEmpty().MaximumLength(100);
            RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(100);
        }
    }
}
