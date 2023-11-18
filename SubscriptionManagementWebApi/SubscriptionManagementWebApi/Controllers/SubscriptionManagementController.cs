using Microsoft.AspNetCore.Mvc;

namespace SubscriptionManagement.Api.Controllers
{
    [ApiController]
    [Route("subscriptions")]
    public class SubscriptionManagementController : ControllerBase
    {
        private readonly ILogger<SubscriptionManagementController> _logger;

        public SubscriptionManagementController(ILogger<SubscriptionManagementController> logger)
        {
            _logger = logger;
        }


        [HttpPut("start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> StartSubscription()
        {
            //Claims.GetUser
            return Ok();
        }

        [HttpPut("stop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> StopSubscription()
        {
            return Ok();
        }
    }
}