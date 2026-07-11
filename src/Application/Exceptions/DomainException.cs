namespace MarketplaceApi.src.Application.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message) { }
    }
}
