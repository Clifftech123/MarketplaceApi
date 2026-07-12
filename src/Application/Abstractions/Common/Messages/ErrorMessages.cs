namespace MarketplaceApi.src.Application.Abstractions.Common.Messages
{
    public static class ErrorMessages
    {
        #region General
        public const string Unexpected = "An unexpected error occurred. Please try again later.";
        public const string ValidationFailed = "One or more validation errors occurred.";
        public const string Unauthorized = "You are not authorized to perform this action.";
        public const string Forbidden = "You do not have permission to access this resource.";
        public const string NotFound = "The requested resource was not found.";
        public const string BadRequest = "The request is invalid.";
        public const string Conflict = "The request conflicts with the current state of the resource.";
        public const string TooManyRequests = "Too many requests. Please try again later.";
        public const string InvalidId = "The provided identifier is invalid.";
        public const string OperationFailed = "The operation could not be completed.";
        public const string internalServerError = "An internal server error occurred. Please try again later.";
        #endregion

        #region Auth
        public const string InvalidCredentials = "The email or password is incorrect.";
        public const string AccountDeactivated = "This account has been deactivated.";
        public const string RefreshTokenInvalid = "The refresh token is invalid, expired, or has already been revoked.";
        public const string EmailAlreadyRegistered = "An account with this email address already exists.";
        public const string UserNotAuthenticated = "User is not authenticated.";
        public const string UserNotFound = "User not found.";
        public const string PasswordChangeFailed = "Failed to change the password. Please ensure the current password is correct.";
        #endregion

        #region Otp
        public const string OtpRequestRejected = "Unable to process this OTP request.";
        public const string OtpCooldownActive = "Please wait before requesting another OTP.";
        public const string OtpInvalidOrExpired = "The OTP code is invalid or has expired.";
        public const string OtpAttemptsExceeded = "Too many incorrect attempts. Please request a new OTP.";
        #endregion

        #region File / Image Upload
        public const string FileSizeExceedsMaxFormat = "The file size exceeds the maximum allowed size of {0}MB.";
        public const string FileTypeNotAllowedFormat = "The file type is not allowed. Allowed types: {0}.";
        public const string FileExtensionNotAllowedFormat = "The file extension is not allowed. Allowed extensions: {0}.";
        public const string ImageUploadFailedFormat = "Image upload failed: {0}.";


        #endregion
    }
}
