namespace MarketplaceApi.src.Application.Options
{
    public class PayStackOptions
    {
        public const string SectionName = "PayStack";

        public string SecretKey { get; set; } = string.Empty;
        public string PublishableKey { get; set; } = string.Empty;
        public string WebhookSecret { get; set; } = string.Empty;

        /// <summary>ISO 4217 currency for all donations (e.g. "gbp", "usd").</summary>
        public string Currency { get; set; } = "GHS";

        /// <summary>Frontend URL Stripe redirects donors to after a successful checkout. Stripe will append <c>?session_id={CHECKOUT_SESSION_ID}</c>.</summary>
        public string SuccessUrl { get; set; } = string.Empty;

        /// <summary>Frontend URL Stripe redirects donors to after cancellation.</summary>
        public string CancelUrl { get; set; } = string.Empty;
    }
}
