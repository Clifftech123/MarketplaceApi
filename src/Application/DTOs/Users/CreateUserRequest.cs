using MarketplaceApi.src.Domain.Enums;

namespace MarketplaceApi.src.Application.DTOs.Users
{
    public sealed record CreateUserRequest
    {
        public required string Email { get; init; }

        public required string Password { get; init; }

        public required string FullName { get; init; }

        public AccountType AccountType { get; init; } = AccountType.Customer;
    }
}
