using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubscriptionManagement.Api.Dto;
using SubscriptionManagement.Domain.Commands;

namespace SubscriptionManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class SubscriptionManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SubscriptionManagementController> _logger;

        public SubscriptionManagementController(IMediator mediator, ILogger<SubscriptionManagementController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpPatch("/{userId:int}/subscription")]
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
    }
}