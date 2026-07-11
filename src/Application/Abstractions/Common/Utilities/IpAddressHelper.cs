using System.Net;

namespace MarketplaceApi.src.Application.Abstractions.Common.Utilities
{
    public static class IpAddressHelper
    {
        public static IPAddress GetClientIpAddress(HttpContext context)
        {
            var xRealIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(xRealIp) && IPAddress.TryParse(xRealIp, out var realIp))
                return realIp;

            var xForwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(xForwardedFor))
            {
                var firstIp = xForwardedFor.Split(',')[0].Trim();
                if (IPAddress.TryParse(firstIp, out var forwardedIp))
                    return forwardedIp;
            }

            return context.Connection.RemoteIpAddress ?? IPAddress.None;
        }

        public static string GetClientIpString(HttpContext context)
        {
            var ip = GetClientIpAddress(context);
            return ip.Equals(IPAddress.None) ? "unknown" : ip.ToString();
        }
    }
}
