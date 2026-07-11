using FluentValidation;
using MarketplaceApi.src.Application.Abstractions.Common.Messages;
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
                NotFoundException => (StatusCodes.Status404NotFound, ErrorMessages.NotFound),
                ForbiddenException => (StatusCodes.Status403Forbidden, ErrorMessages.Forbidden),
                BusinessException => (StatusCodes.Status400BadRequest, ErrorMessages.BadRequest),
                ConflictException => (StatusCodes.Status409Conflict, ErrorMessages.Conflict),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized),
                ValidationException => (StatusCodes.Status400BadRequest, ErrorMessages.ValidationFailed),
                _ => (StatusCodes.Status500InternalServerError, ErrorMessages.internalServerError)
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
                logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
            else
                logger.LogWarning("Handled exception [{Type}]: {Message}", exception.GetType().Name, exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception switch
                {
                    UnauthorizedAccessException => ErrorMessages.Unauthorized,
                    _ when statusCode == StatusCodes.Status500InternalServerError => ErrorMessages.Unexpected,
                    _ => exception.Message
                },
                Instance = httpContext.Request.Path
            };

            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions["errors"] = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            }

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}

