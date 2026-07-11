using MarketplaceApi.src.Application.Abstractions.Common;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.src.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult Success(string? message = null) =>
            Ok(ApiResponse.Ok(message));

        protected IActionResult Success<T>(T data, string? message = null) =>
            Ok(ApiResponse.Ok(data, message));

        protected IActionResult Created<T>(string actionName, object routeValues, T data, string? message = null) =>
            CreatedAtAction(actionName, routeValues, ApiResponse.Ok(data, message));
    }
}
