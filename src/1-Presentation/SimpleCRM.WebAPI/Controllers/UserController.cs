using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Attendant.Contracts.Services;

namespace SimpleCRM.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]s")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorRS), (int)HttpStatusCode.InternalServerError)]
    public async Task<UserRS> UserRegisterAsync(UserRegisterRQ userRegisterRQ, CancellationToken cancellationToken)
    {
        return await _userService.UserRegisterAsync(userRegisterRQ, cancellationToken);
    }
}