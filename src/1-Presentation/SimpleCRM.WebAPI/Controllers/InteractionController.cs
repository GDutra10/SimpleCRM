using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Attendant.Contracts;
using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class InteractionController : AppBaseController
{
    private readonly ILogger<InteractionController> _logger;
    private readonly IInteractionService _interactionService;
    
    public InteractionController(ILogger<InteractionController> logger, IInteractionService interactionService)
    {
        _logger = logger;
        _interactionService = interactionService;
    }
    
    [HttpPost("Start")]
    [ProducesResponseType(typeof(InteractionRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<InteractionRS> InteractionStartAsync([FromQuery]InteractionStartRQ interactionStartRQ, CancellationToken cancellationToken)
    {
        return await _interactionService.InteractionStartAsync(this.GetAccessTokenFromHeader(), interactionStartRQ, cancellationToken);
    }

    [HttpPut("Finish")]
    [ProducesResponseType(typeof(InteractionRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<InteractionRS> InteractionFinishAsync(InteractionFinishRQ interactionFinishRQ, CancellationToken cancellationToken)
    {
        return await _interactionService.InteractionFinishAsync(this.GetAccessTokenFromHeader(), interactionFinishRQ, cancellationToken);
    }
}