using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Application.Backoffice.Contracts.Services;
using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.WebAPI.Controllers.Backoffice;

[Authorize]
[ApiController]
[Route("Backoffice/[controller]s")]
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
    [ProducesResponseType(typeof(InteractionStartRS), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    public async Task<InteractionStartRS> StartAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return await _interactionService.StartAsync(orderId, cancellationToken);
    }
}