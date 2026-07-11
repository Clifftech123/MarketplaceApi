using MarketplaceApi.src.Application.Contracts.Options;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceApi.src.Application.Options
{
    public class JwtOptions : IApplicationOption
    {
        [Required]
        public string Secret { get; set; } = default!;

        [Required]
        public string Issuer { get; set; } = default!;

        [Required]
        public string Audience { get; set; } = default!;

        public int AccessTokenExpiryMinutes { get; set; } = 15;
        public int RefreshTokenExpiryDays { get; set; } = 7;
    }
}
