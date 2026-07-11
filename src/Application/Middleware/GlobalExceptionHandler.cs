using MarketplaceApi.src.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.src.Application.Middleware
{

    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var (statusCode, title) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
                ForbiddenException => (StatusCodes.Status403Forbidden, "Forbidden"),
                BusinessException => (StatusCodes.Status400BadRequest, "Bad Request"),
                ConflictException => (StatusCodes.Status409Conflict, "Conflict"),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
                logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
            else
                logger.LogWarning("Handled exception [{Type}]: {Message}", exception.GetType().Name, exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = statusCode == StatusCodes.Status500InternalServerError
                    ? "An unexpected error occurred. Please try again later."
                    : exception.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}

