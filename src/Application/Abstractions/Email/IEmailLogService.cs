using MarketplaceApi.src.Application.DTOs.Users;

namespace MarketplaceApi.src.Application.Abstractions.Email
{
    public interface IEmailLogService
    {
        /// <summary>
        /// Gets a single email log by its identifier.
        /// </summary>
        Task<EmailLogResponse> GetEmailLogByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all email logs sent to the given recipient address.
        /// </summary>
        Task<IReadOnlyList<EmailLogResponse>> GetEmailLogsByEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all email logs associated with the given order.
        /// </summary>
        Task<IReadOnlyList<EmailLogResponse>> GetEmailLogsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);

        Task<EmailLogResponse> CreateEmailLogAsync(CreateEmailLogRequest emailLogRequest, CancellationToken cancellationToken);

        Task<EmailLogResponse> UpdateEmailLogAsync(Guid id, UpdateEmailLogRequest emailLogRequest, CancellationToken cancellationToken);

        Task DeleteEmailLogAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Restores a previously soft-deleted email log.
        /// </summary>
        Task RestoreEmailLogAsync(Guid id, CancellationToken cancellationToken);
    }
}
