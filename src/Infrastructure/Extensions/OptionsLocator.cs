using MarketplaceApi.src.Application.Constracts.Options;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace MarketplaceApi.src.Infrastructure.Extensions
{
    public static class OptionsLocator
    {
        public static TOptions GetApplicationOptions<TOptions>(this IServiceCollection serviceCollection)
            where TOptions : class, IApplicationOption, new()
            => serviceCollection.BuildServiceProvider().GetApplicationOptions<TOptions>();

        public static TOptions GetApplicationOptions<TOptions>(this IServiceProvider serviceProvider)
            where TOptions : class, IApplicationOption, new()
        {
            var options = serviceProvider.GetService<IOptions<TOptions>>()?.Value;

            if (options != null) return options;

            throw new InvalidOperationException(
                $"No configuration found for {typeof(TOptions).Name}. "
                + $"Please ensure that services.ConfigureOptions<ConfigureApplicationOptions<{typeof(TOptions).Name}>>() "
                + $"is called in {Assembly.GetCallingAssembly().GetName().Name}.");
        }
    }
}
