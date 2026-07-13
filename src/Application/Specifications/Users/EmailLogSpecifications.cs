using MarketplaceApi.src.Domain.Entities.Users.Entities;
using MarketplaceApi.src.Domain.Specifications;

namespace MarketplaceApi.src.Application.Specifications.Users
{
    public sealed class EmailLogsByRecipientSpecification : Specification<EmailLog>
    {
        public EmailLogsByRecipientSpecification(string email)
            : base(e => e.ToAddress == email)
        {
            AddOrderByDescending(e => e.SentAt);
        }
    }

    public sealed class EmailLogsByOrderIdSpecification : Specification<EmailLog>
    {
        public EmailLogsByOrderIdSpecification(Guid orderId)
            : base(e => e.OrderId != null && e.OrderId.Value == orderId)
        {
            AddOrderByDescending(e => e.SentAt);
        }
    }

    /// <summary>
    /// Retrieves soft-deleted email logs. Requires ignoring the global soft-delete query filter,
    /// since EmailLog has a HasQueryFilter(e => !e.IsDeleted) configured in EmailLogConfiguration.
    /// </summary>
    public sealed class DeletedEmailLogsSpecification : Specification<EmailLog>
    {
        public DeletedEmailLogsSpecification()
            : base(e => e.IsDeleted)
        {
            ApplyIgnoreQueryFilters();
            AddOrderByDescending(e => e.SentAt);
        }
    }
}
