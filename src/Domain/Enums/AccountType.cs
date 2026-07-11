namespace MarketplaceApi.src.Domain.Enums
{
    /// <summary>
    /// The self-service account types a client may register as. Deliberately excludes
    /// Admin — that role can only ever be granted later by an existing admin via
    /// IAdminService.AssignRoleAsync, never chosen at registration.
    /// </summary>
    public enum AccountType
    {
        Customer,
        Vendor
    }
}
