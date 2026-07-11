using MarketplaceApi.src.Application.Contracts.Options;

namespace MarketplaceApi.src.Application.Options
{
    public static class Configure
    {
        /// <summary>
        /// Adds user-secrets so sensitive config (connection strings, keys)
        /// never live in appsettings.json.
        /// </summary>
        public static void AddApplicationSettings(this IConfigurationBuilder config)
            => config.AddUserSecrets<AssemblyReference>();

        /// <summary>
        /// Registers <typeparamref name="TOptions"/> with the DI container so it is
        /// resolvable via <c>IOptions&lt;TOptions&gt;</c> and <c>GetApplicationOptions&lt;TOptions&gt;</c>.

        /// </summary>
        public static void ConfigureApplicationOptions<TOptions>(this IServiceCollection services)
            where TOptions : class, IApplicationOption, new()
            => services.ConfigureOptions<ConfigureApplicationOptions<TOptions>>();
    }
}
