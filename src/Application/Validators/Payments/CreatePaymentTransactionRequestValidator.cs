using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Payments;

namespace MarketplaceApi.src.Application.Validators.Payments
{
    public class CreatePaymentTransactionRequestValidator : AbstractValidator<CreatePaymentTransactionRequest>
    {
        public CreatePaymentTransactionRequestValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.StripeTransactionId).NotEmpty().MaximumLength(255);
            RuleFor(x => x.StripePaymentIntentId).MaximumLength(255);
            RuleFor(x => x.AmountPence).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Currency).NotEmpty().Length(3);
            RuleFor(x => x.Method).IsInEnum();
        }
    }
}
