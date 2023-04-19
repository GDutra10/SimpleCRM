using System.Net;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.Services;

namespace SimpleCRM.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }
    
    [HttpPost("Login")]
    [ProducesResponseType(typeof(LoginRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorRS), (int)HttpStatusCode.InternalServerError)]
    public async Task<LoginRS> TryLoginAsync(LoginRQ loginRQ, CancellationToken cancellationToken)
    {
        return await _authenticationService.TryLoginAsync(loginRQ, cancellationToken);
    }
}