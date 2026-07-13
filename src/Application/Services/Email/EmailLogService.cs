using MarketplaceApi.src.Application.Abstractions.Email;
using MarketplaceApi.src.Application.DTOs.Users;
using MarketplaceApi.src.Application.Mappings.Users;
using MarketplaceApi.src.Application.Specifications.Users;
using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using MarketplaceApi.src.Domain.Entities.Users.ValueObjects;

namespace MarketplaceApi.src.Application.Services.Email
{
    public class EmailLogService(IRepository<EmailLog, EmailLogId> repo) : IEmailLogService
    {

        public async Task<EmailLogResponse> CreateEmailLogAsync(CreateEmailLogRequest emailLogRequest, CancellationToken cancellationToken)
        {
            var entity = emailLogRequest.ToEntity();
            var created = await repo.AddAsync(entity, cancellationToken);
            return created.ToResponse();
        }

        public async Task DeleteEmailLogAsync(Guid id, CancellationToken cancellationToken)
        {
            var emailLog = await repo.GetByIdAsync(GetEmailLogId(id), cancellationToken)
                ?? throw new KeyNotFoundException($"Email log with ID {id} not found.");

            emailLog.Delete();
            await repo.UpdateAsync(emailLog, cancellationToken);
        }

        public async Task<EmailLogResponse> GetEmailLogByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var emailLog = await repo.GetByIdAsync(GetEmailLogId(id), cancellationToken)
                ?? throw new KeyNotFoundException($"Email log with ID {id} not found.");
            return emailLog.ToResponse();


        }

        public async Task<IReadOnlyList<EmailLogResponse>> GetEmailLogsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var emailLogs = await repo.GetManyAsync(new EmailLogsByRecipientSpecification(email), cancellationToken);
            return emailLogs.Select(log => log.ToResponse()).ToList();

        }

        public async Task<IReadOnlyList<EmailLogResponse>> GetEmailLogsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var emailLogs = await repo.GetManyAsync(new EmailLogsByOrderIdSpecification(orderId), cancellationToken);
            return emailLogs.Select(log => log.ToResponse()).ToList();

        }

        public async Task RestoreEmailLogAsync(Guid id, CancellationToken cancellationToken)
        {
            var emailLog = await repo.GetByIdAsync(new EmailLogId(id), cancellationToken)
                ?? throw new KeyNotFoundException($"Email log with ID {id} not found.");

            emailLog.Restore();
            await repo.UpdateAsync(emailLog, cancellationToken);
        }

        public async Task<EmailLogResponse> UpdateEmailLogAsync(Guid id, UpdateEmailLogRequest emailLogRequest, CancellationToken cancellationToken)
        {
            var emailLog = await repo.GetByIdAsync(GetEmailLogId(id), cancellationToken)
                ?? throw new KeyNotFoundException($"Email log with ID {id} not found.");

            if (emailLogRequest.WasSent)
                emailLog.MarkSent();
            else
                emailLog.MarkFailed(emailLogRequest.ErrorMessage ?? string.Empty);

            var updated = await repo.UpdateAsync(emailLog, cancellationToken);
            return updated.ToResponse();
        }



        #region Private Methods

        // EmailLog Id 

        private static EmailLogId GetEmailLogId(Guid id) => new EmailLogId(id);

        #endregion 
    }
}
