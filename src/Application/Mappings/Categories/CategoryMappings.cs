using MarketplaceApi.src.Application.DTOs.Categories;
using MarketplaceApi.src.Domain.Entities.Categories.Entities;

namespace MarketplaceApi.src.Application.Mappings.Categories
{
    public static class CategoryMappings
    {
        public static CategoryResponse ToResponse(this Category category) => new()
        {
            Id = category.Id.Value,
            Name = category.Name,
            Description = category.Description
        };

        public static Category ToEntity(this CreateCategoryRequest request)
            => Category.Create(request.Name, request.Description);

        public static void ApplyTo(this UpdateCategoryRequest request, Category category)
        {
            category.Rename(request.Name);
            category.UpdateDescription(request.Description);
        }
    }
}
