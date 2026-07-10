using MarketplaceApi.src.Application.Options;
using MarketplaceApi.src.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarketplaceApi.src.Infrastructure.Extensions
{
    public static class Configure
    {

        public static void AddInfrastructure(this IServiceCollection services) 
        
        {
            services.AddDbContext<ApplicationDbContext>((serviceProvider, builder) =>
            {
                var sqlServerOptions = serviceProvider.GetApplicationOptions<SqlServerOptions>();
                builder.UseSqlServer(
                    sqlServerOptions.ConnectionString,
                    options =>
                    {
                        options.EnableRetryOnFailure(sqlServerOptions.MaxRetryCount);
                        options.CommandTimeout(sqlServerOptions.CommandTimeout);
                    });
                builder.EnableSensitiveDataLogging(sqlServerOptions.EnableSensitiveDataLogging);
                builder.EnableDetailedErrors(sqlServerOptions.EnableDetailedErrors);
            });


        }
    }
}
