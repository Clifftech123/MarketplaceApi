using MarketplaceApi.src.Application.Abstractions.Common;
using MarketplaceApi.src.Application.Abstractions.Common.Messages;
using MarketplaceApi.src.Application.Abstractions.Email;
using MarketplaceApi.src.Application.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApi.src.Presentation.Controllers.Email
{
    public class EmailLogController(IEmailLogService emailLogService) : ApiControllerBase
    {
        /// <summary>
        /// Gets a single email log by its identifier.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<EmailLogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await emailLogService.GetEmailLogByIdAsync(id, cancellationToken);
            return Success(result);
        }

        /// <summary>
        /// Gets all email logs sent to the given recipient address.
        /// </summary>
        [HttpGet("by-email/{email}")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<EmailLogResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEmail(string email, CancellationToken cancellationToken)
        {
            var result = await emailLogService.GetEmailLogsByEmailAsync(email, cancellationToken);
            return Success(result);
        }

        /// <summary>
        /// Gets all email logs associated with the given order.
        /// </summary>
        [HttpGet("by-order/{orderId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<EmailLogResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByOrderId(Guid orderId, CancellationToken cancellationToken)
        {
            var result = await emailLogService.GetEmailLogsByOrderIdAsync(orderId, cancellationToken);
            return Success(result);
        }

        /// <summary>
        /// Creates a new email log entry.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<EmailLogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateEmailLogRequest request, CancellationToken cancellationToken)
        {
            var result = await emailLogService.CreateEmailLogAsync(request, cancellationToken);
            return Success(result, SuccessMessages.EmailLogCreated);
        }

        /// <summary>
        /// Updates an existing email log entry's delivery status.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<EmailLogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateEmailLogRequest request, CancellationToken cancellationToken)
        {
            var result = await emailLogService.UpdateEmailLogAsync(id, request, cancellationToken);
            return Success(result, SuccessMessages.EmailLogUpdated);
        }

        /// <summary>
        /// Soft-deletes an email log entry.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await emailLogService.DeleteEmailLogAsync(id, cancellationToken);
            return Success(SuccessMessages.EmailLogDeleted);
        }

        /// <summary>
        /// Restores a previously soft-deleted email log entry.
        /// </summary>
        [HttpPost("{id:guid}/restore")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
        {
            await emailLogService.RestoreEmailLogAsync(id, cancellationToken);
            return Success(SuccessMessages.EmailLogRestored);
        }

        /// <summary>
        /// Gets all soft-deleted email logs.
        /// </summary>
        [HttpGet("deleted")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<EmailLogResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDeleted(CancellationToken cancellationToken)
        {
            var result = await emailLogService.GetAllDeletedAsync(cancellationToken);
            return Success(result);
        }
    }
}
