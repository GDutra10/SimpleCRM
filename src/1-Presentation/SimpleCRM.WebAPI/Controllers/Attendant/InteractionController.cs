using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Attendant.Contracts.Services;
using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.WebAPI.Controllers.Attendant;

[Authorize]
[ApiController]
[Route("Attendant/[controller]s")]
public class InteractionController : AppBaseController
{
    private readonly ILogger<InteractionController> _logger;
    private readonly IInteractionService _interactionService;
    
    public InteractionController(ILogger<InteractionController> logger, IInteractionService interactionService)
    {
        _logger = logger;
        _interactionService = interactionService;
    }
    
    [Authorize]
    [HttpGet("")]
    [ProducesResponseType(typeof(List<InteractionRS>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<List<InteractionRS>> GetInteractionAsync(CancellationToken cancellationToken)
    {
        return await _interactionService.GetInteractionsInAttendanceAsync(this.GetAccessTokenFromHeader(), cancellationToken);
    }
    
    [Authorize]
    [HttpPost("Start")]
    [ProducesResponseType(typeof(InteractionRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<InteractionRS> InteractionStartAsync([FromQuery]InteractionStartRQ interactionStartRQ, CancellationToken cancellationToken)
    {
        return await _interactionService.InteractionStartAsync(this.GetAccessTokenFromHeader(), interactionStartRQ, cancellationToken);
    }

    [Authorize]
    [HttpPut("Finish")]
    [ProducesResponseType(typeof(InteractionRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<InteractionRS> InteractionFinishAsync(InteractionFinishRQ interactionFinishRQ, CancellationToken cancellationToken)
    {
        return await _interactionService.InteractionFinishAsync(this.GetAccessTokenFromHeader(), interactionFinishRQ, cancellationToken);
    }

    [Authorize]
    [HttpPost("Order")]
    [ProducesResponseType(typeof(OrderRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<OrderRS> AddOrderItemAsync(OrderItemAddRQ orderItemAddRQ, CancellationToken cancellationToken)
    {
        return await _interactionService.AddOrderItemAsync(this.GetAccessTokenFromHeader(), orderItemAddRQ, cancellationToken);
    }

    [Authorize]
    [HttpDelete("Order")]
    [ProducesResponseType(typeof(OrderRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<OrderRS> DeleteOrderItemAsync(OrderItemDeleteRQ orderItemDeleteRQ, CancellationToken cancellationToken)
    {
        return await _interactionService.DeleteOrderItemAsync(this.GetAccessTokenFromHeader(), orderItemDeleteRQ, cancellationToken);
    }
}