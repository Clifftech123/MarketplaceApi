using System.Globalization;

namespace MarketplaceApi.src.Application.Abstractions.Common.Messages
{
    /// <summary>
    /// Centralized helpers for building formatted error messages from
    /// <see cref="ErrorMessages"/> templates. Use these instead of calling
    /// <see cref="string.Format(string, object?[])"/> directly so all
    /// formatting stays consistent (culture-invariant) and discoverable.
    /// </summary>
    public static class ErrorMessageFormatter
    {
        /// <summary>
        /// Generic invariant-culture format. Prefer the strongly-typed helpers
        /// below; use this only for one-off messages.
        /// </summary>
        public static string Format(string template, params object?[] args) =>
            string.Format(CultureInfo.InvariantCulture, template, args);


        public static string FileSizeExceedsMax(long maxSizeMb) =>
            Format(ErrorMessages.FileSizeExceedsMaxFormat, maxSizeMb);

        public static string FileTypeNotAllowed(IEnumerable<string> allowedTypes) =>
            Format(ErrorMessages.FileTypeNotAllowedFormat, string.Join(", ", allowedTypes));

        public static string FileExtensionNotAllowed(IEnumerable<string> allowedExtensions) =>
            Format(ErrorMessages.FileExtensionNotAllowedFormat, string.Join(", ", allowedExtensions));

        public static string ImageUploadFailed(string upstreamError) =>
            Format(ErrorMessages.ImageUploadFailedFormat, upstreamError);
    }
}
