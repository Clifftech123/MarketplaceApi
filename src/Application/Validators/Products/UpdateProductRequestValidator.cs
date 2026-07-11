using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Products;

namespace MarketplaceApi.src.Application.Validators.Products
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Description).MaximumLength(2000);
        }
    }
}
