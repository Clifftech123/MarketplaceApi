using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Orders;

namespace MarketplaceApi.src.Application.Validators.Orders
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.ShippingAddress).NotNull().SetValidator(new ShippingAddressRequestValidator());
        }
    }
}
