using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Products
{
    public sealed record AdjustProductStockRequest
    {
        [Range(1, int.MaxValue)]
        public required int Quantity { get; init; }
    }
}
