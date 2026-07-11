using MarketplaceApi.src.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record CreateEmailLogRequest
    {
        [Required, EmailAddress, StringLength(320)]
        public required string ToAddress { get; init; }

        [Required, StringLength(300)]
        public required string Subject { get; init; }

        [Required]
        public required EmailType Type { get; init; }

        public Guid? OrderId { get; init; }
    }
}
