using FluentValidation;
using MarketplaceApi.src.Application.DTOs.Products;

namespace MarketplaceApi.src.Application.Validators.Products
{
    public class CreateProductImageRequestValidator : AbstractValidator<CreateProductImageRequest>
    {
        public CreateProductImageRequestValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty()
                .MaximumLength(2000)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("'Url' must be a valid absolute URL.");

            RuleFor(x => x.AltText).MaximumLength(300);
        }
    }
}
