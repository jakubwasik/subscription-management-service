using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubscriptionManagement.Api.DTOs;
using SubscriptionManagement.Domain.Commands;
using SubscriptionManagement.Domain.Queries;

namespace SubscriptionManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users/")]
    public class SubscriptionManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SubscriptionManagementController> _logger;

        public SubscriptionManagementController(IMediator mediator, ILogger<SubscriptionManagementController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint for managing user's subscription (REST compliant)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [HttpPatch("{userId:int}/subscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ManageSubscription(int userId, [FromBody] OperationDto operation)
        {
            // We're assuming here that authN & authZ middleware succeeded. That means user can modify given subscription.
            IRequest command = operation.Type switch
            {
                ActionType.Start => new StartSubscriptionCommand(userId),
                ActionType.Stop => new StopSubscriptionCommand(userId),
                _ => throw new ArgumentOutOfRangeException()
            };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{userId:int}/subscription")]
        public async Task<ActionResult<SubscriptionDto>> GetSubscription(int userId)
        {
            // We're assuming here that authN & authZ middleware succeeded. That means user can access given subscription.
            var subscription = await _mediator.Send(new GetSubscriptionQuery(userId));
            if (subscription == null)
            {
                return NotFound();
            }

            // Note: this is a simplified version of mapping, for simplicity AutoMapper is not used here
            return Ok(Mapper.ToDto(subscription));
        }
    }
}