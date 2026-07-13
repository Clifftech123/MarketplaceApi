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
}
