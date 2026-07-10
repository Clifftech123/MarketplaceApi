using MarketplaceApi.src.Application.Constracts.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MarketplaceApi.src.Application.Options
{
    /// <summary>
    /// Binds a configuration section (keyed by <typeparamref name="TOptions"/> class name)
    /// to the options instance registered via <see cref="IOptions{TOptions}"/>.
    /// </summary>
    public sealed class ConfigureApplicationOptions<TOptions>
        : IConfigureOptions<TOptions>
        where TOptions : class, IApplicationOption, new()
    {
        private readonly IConfiguration _configuration;

        public ConfigureApplicationOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(TOptions options)
        {
            _configuration
                .GetSection(typeof(TOptions).Name)
                .Bind(options);
        }
    }
}
