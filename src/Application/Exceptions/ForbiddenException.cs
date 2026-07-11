namespace MarketplaceApi.src.Application.Exceptions
{
    public class ForbiddenException : DomainException
    {
        public ForbiddenException(string message) : base(message) { }

    }
}
