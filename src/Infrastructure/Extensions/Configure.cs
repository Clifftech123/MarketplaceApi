using MarketplaceApi.src.Application.Options;
using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Infrastructure.Persistence.Context;
using MarketplaceApi.src.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceApi.src.Infrastructure.Extensions
{
    public static class Configure
    {
        public static void AddInfrastructure(this WebApplicationBuilder builder)
        {
            // Adds user-secrets (keeps connection strings out of appsettings.json)
            builder.Configuration.AddApplicationSettings();

            // Binds "SqlServerOptions" config section → IOptions<SqlServerOptions>
            builder.Services.ConfigureApplicationOptions<SqlServerOptions>();

            // Register DbContext with SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var sqlServerOptions = serviceProvider.GetApplicationOptions<SqlServerOptions>();
                options.UseSqlServer(
                    sqlServerOptions.ConnectionString,
                    sql =>
                    {
                        sql.EnableRetryOnFailure(sqlServerOptions.MaxRetryCount);
                        sql.CommandTimeout(sqlServerOptions.CommandTimeout);
                    });
                options.EnableSensitiveDataLogging(sqlServerOptions.EnableSensitiveDataLogging);
                options.EnableDetailedErrors(sqlServerOptions.EnableDetailedErrors);
            });

            // Register generic repository — resolves IRepository<TEntity, TEntityId>
            builder.Services.AddScoped(
                typeof(IRepository<,>),
                typeof(Repository<,>));
        }
    }
}
