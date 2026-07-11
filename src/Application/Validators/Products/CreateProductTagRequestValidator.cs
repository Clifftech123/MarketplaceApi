using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Products;

namespace MarketplaceApi.src.Application.Validators.Products
{
    public class CreateProductTagRequestValidator : AbstractValidator<CreateProductTagRequest>
    {
        public CreateProductTagRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
