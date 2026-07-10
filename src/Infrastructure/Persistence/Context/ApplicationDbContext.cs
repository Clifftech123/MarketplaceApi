using MarketplaceApi.src.Domain.Entities.Carts.Entities;
using MarketplaceApi.src.Domain.Entities.Categories.Entities;
using MarketplaceApi.src.Domain.Entities.Orders.Entities;
using MarketplaceApi.src.Domain.Entities.Payments.Entities;
using MarketplaceApi.src.Domain.Entities.Products.Entities;
using MarketplaceApi.src.Domain.Entities.Stores.Entities;
using MarketplaceApi.src.Domain.Entities.Users.Entities;
using MarketplaceApi.src.Infrastructure.Persistence.Audit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceApi.src.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Products
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
      

        // Stores
        public DbSet<Store> Stores { get; set; }

        // Orders
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        // Categories
        public DbSet<Category> Categories { get; set; }

        // Payments
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        // Carts
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        // Users
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<TwoFactorToken> TwoFactorTokens { get; set; }

        // Audit
        public DbSet<AuditTrail> AuditTrails { get; set; }
    }
}
