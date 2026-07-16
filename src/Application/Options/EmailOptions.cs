using MarketplaceApi.src.Application.Contracts.Options;

public class EmailOptions : IApplicationOption
{
    public const string SectionName = "Email";

    // SMTP connection settings (Gmail)
    public string SmtpHost { get; set; } = "smtp.gmail.com";
    public int SmtpPort { get; set; } = 587; // 587 = STARTTLS, 465 = SSL
    public string SmtpUsername { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } = true;

    public string SenderName { get; set; } = "Market Place API";
    public string SenderEmail { get; set; } = string.Empty;

    /// <summary>Frontend URL used to build links inside emails (e.g. donation confirmation page).</summary>
    public string FrontendUrl { get; set; } = "http://localhost:5173";
}