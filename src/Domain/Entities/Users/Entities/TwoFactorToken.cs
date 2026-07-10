using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Domain.Entities.Users.Entities
{
    public class TwoFactorToken
    {
        public TwoFactorToken()
        {
            Id = Guid.NewGuid().ToString();
            IssuedAt = DateTimeOffset.UtcNow;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public required string UserIdentifier { get; set; }

        [Required]
        public required string Secret { get; set; }

        public DateTimeOffset IssuedAt { get; set; }

        [Required]
        public required string Token { get; set; }

        public DateTimeOffset ValidTo { get; set; }

        [StringLength(50)]
        public string? Purpose { get; set; } = "Login";

        public bool IsUsed { get; set; } = false;

        public DateTimeOffset? UsedAt { get; set; }

        [StringLength(45)]
        public string? IpAddress { get; set; }
    }
}
