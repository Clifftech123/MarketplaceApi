namespace MarketplaceApi.src.Application.Exceptions
{
    public class BusinessException : DomainException
    {
        public BusinessException(string message) : base(message) { }
    }
}
