using MarketplaceApi.src.Application.EventHandlers.Payments;
using MarketplaceApi.src.Application.Options;
using MarketplaceApi.src.Domain.Contracts;
using MarketplaceApi.src.Domain.Entities.Payments.Events;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using MarketplaceApi.src.Infrastructure.Events;
using MarketplaceApi.src.Infrastructure.Interceptor;
using MarketplaceApi.src.Infrastructure.Persistence.Context;
using MarketplaceApi.src.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceApi.src.Infrastructure.Extensions




// CQRS pattern: Application layer handles commands/queries, but the Infrastructure layer
// handles domain events. This is because domain events are part of the domain model, and their handling often involves infrastructure concerns (e.g., logging, sending notifications, etc.). By placing the event handlers in the Infrastructure layer, we can keep the Application layer focused on orchestrating commands and queries without being tightly coupled to specific infrastructure implementations.

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

            // Email service configuration (SMTP) — binds "SmtpOptions" config section → IOptions<SmtpOptions>

            builder.Services.ConfigureApplicationOptions<EmailOptions>();

            // Needed by AuditInterceptor/SoftDeleteInterceptor to stamp the current user
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<AuditInterceptor>();
            builder.Services.AddScoped<SoftDeleteInterceptor>();
            builder.Services.AddScoped<DomainEventDispatchInterceptor>();

            // Dispatches domain events raised by aggregate roots after SaveChanges succeeds
            builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            builder.Services.AddScoped<IDomainEventHandler<PaymentSucceededDomainEvent>, PaymentSucceededLoggingHandler>();

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
                // DomainEventDispatchInterceptor hooks SavedChangesAsync (after commit), so
                // ordering relative to the other two — which hook SavingChangesAsync — doesn't matter.
                options.AddInterceptors(
                    serviceProvider.GetRequiredService<AuditInterceptor>(),
                    serviceProvider.GetRequiredService<SoftDeleteInterceptor>(),
                    serviceProvider.GetRequiredService<DomainEventDispatchInterceptor>());
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
