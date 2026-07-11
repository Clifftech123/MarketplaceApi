using MarketplaceApi.src.Application.Options;
using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using MarketplaceApi.src.Infrastructure.Interceptor;
using MarketplaceApi.src.Infrastructure.Persistence.Context;
using MarketplaceApi.src.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
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

            // Binds "JwtOptions" config section → IOptions<JwtOptions> (Secret lives in user-secrets, not appsettings.json)
            builder.Services.ConfigureApplicationOptions<JwtOptions>();

            // Binds "PayStackOptions" config section → IOptions<PayStackOptions> (keys live in user-secrets, not appsettings.json)
            builder.Services.ConfigureApplicationOptions<PayStackOptions>();

            // Needed by AuditInterceptor/SoftDeleteInterceptor to stamp the current user
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<AuditInterceptor>();
            builder.Services.AddScoped<SoftDeleteInterceptor>();

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
                // AuditInterceptor must run before SoftDeleteInterceptor so it records the
                // true Delete state before soft-delete rewrites it to Modified.
                options.AddInterceptors(
                    serviceProvider.GetRequiredService<AuditInterceptor>(),
                    serviceProvider.GetRequiredService<SoftDeleteInterceptor>());
            });

            // Register generic repository — resolves IRepository<TEntity, TEntityId>
            builder.Services.AddScoped(
                typeof(IRepository<,>),
                typeof(Repository<,>));


            builder.Services.AddIdentityCore<AppUser>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Backs OtpService's cooldown/attempt-tracking cache
            builder.Services.AddMemoryCache();
        }
    }
}
