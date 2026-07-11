using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record CreateUserRequest
    {
        [Required, EmailAddress, StringLength(320)]
        public required string Email { get; init; }

        [Required, DataType(DataType.Password), StringLength(100, MinimumLength = 8)]
        public required string Password { get; init; }

        [Required, StringLength(150)]
        public required string FullName { get; init; }
    }
}
