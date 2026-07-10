namespace MarketplaceApi.src.Domain.Constants
{
    public static class UserRoles
    {
        public const string Customer = nameof(Customer);
        public const string Seller = nameof(Seller);
        public const string Admin = nameof(Admin);

        public static readonly IReadOnlyCollection<string> All =
        [
            Customer,
            Seller,
            Admin
        ];
    }
}
