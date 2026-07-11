using MarketplaceApi.src.Application.DTOs.Users;
using MarketplaceApi.src.Domain.Common;

namespace MarketplaceApi.src.Application.Abstractions.Admin
{
    public interface IAdminService
    {
        Task<IReadOnlyCollection<UserResponse>> GetUsersAsync(Pagination pagination, CancellationToken cancellationToken = default);

        Task<UserResponse?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task DeactivateUserAsync(Guid userId, CancellationToken cancellationToken = default);

        Task ActivateUserAsync(Guid userId, CancellationToken cancellationToken = default);

        Task AssignRoleAsync(Guid userId, string roleName, CancellationToken cancellationToken = default);

        Task RemoveRoleAsync(Guid userId, string roleName, CancellationToken cancellationToken = default);
    }
}
