using MarketplaceApi.src.Application.Contracts.Options;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.Options
{
    public class SqlServerOptions : IApplicationOption
    {
        [Required]
        public string ConnectionString { get; set; } = default!;
        public int MaxRetryCount { get; set; } = 5;
        public int CommandTimeout { get; set; } = 30;
        public bool EnableDetailedErrors { get; set; } = true;
        public bool EnableSensitiveDataLogging { get; set; } = true;
    }
}
