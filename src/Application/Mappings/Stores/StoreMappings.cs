using MarketplaceApi.src.Application.DTOs.Stores;
using MarketplaceApi.src.Domain.Entities.Stores.Entities;

namespace MarketplaceApi.src.Application.Mappings.Stores
{
    public static class StoreMappings
    {
        public static StoreResponse ToResponse(this Store store) => new()
        {
            Id = store.Id.Value,
            Name = store.Name,
            Description = store.Description,
            OwnerId = store.OwnerId
        };

        public static Store ToEntity(this CreateStoreRequest request)
            => Store.Create(request.Name, request.Description, request.OwnerId);

        public static void ApplyTo(this UpdateStoreRequest request, Store store)
        {
            store.Rename(request.Name);
            store.UpdateDescription(request.Description);
        }
    }
}
