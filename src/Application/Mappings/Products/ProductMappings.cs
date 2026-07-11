using MarketplaceApi.src.Application.DTOs.Products;
using MarketplaceApi.src.Domain.Entities.Categories.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Products.Entities;
using MarketplaceApi.src.Domain.Entities.Products.ValueObjects;
using MarketplaceApi.src.Domain.Entities.Stores.ValueObjects;

namespace MarketplaceApi.src.Application.Mappings.Products
{
    public static class ProductMappings
    {
        public static ProductResponse ToResponse(this Product product) => new()
        {
            Id = product.Id.Value,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CategoryId = product.CategoryId.Value,
            StoreId = product.StoreId.Value,
            StockQuantity = product.StockQuantity,
            Tags = [.. product.Tags.Select(ToResponse)],
            Images = [.. product.Images.Select(ToResponse)]
        };

        public static ProductTagResponse ToResponse(this ProductTag tag) => new()
        {
            Id = tag.Id.Value,
            Name = tag.Name,
            Description = tag.Description
        };

        public static ProductImageResponse ToResponse(this ProductImage image) => new()
        {
            Id = image.Id.Value,
            Url = image.Url,
            AltText = image.AltText
        };

        public static Product ToEntity(this CreateProductRequest request)
            => Product.Create(
                request.Name,
                request.Price,
                request.Description,
                new CategoryId(request.CategoryId),
                new StoreId(request.StoreId));

        public static void ApplyTo(this UpdateProductRequest request, Product product)
            => product.UpdateDetails(request.Name, request.Price, request.Description);

        public static ProductTag ToEntity(this CreateProductTagRequest request, ProductId productId)
            => ProductTag.Create(request.Name, request.Description, productId);

        public static ProductImage ToEntity(this CreateProductImageRequest request, ProductId productId)
            => ProductImage.Create(request.Url, productId, request.AltText);
    }
}
