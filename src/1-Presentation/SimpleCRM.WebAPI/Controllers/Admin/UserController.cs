using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Admin.Contracts.DTOs;
using SimpleCRM.Application.Admin.Contracts.Services;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.WebAPI.Controllers.Admin;

[Authorize]
[ApiController]
[Route("Admin/[controller]s")]
public class UserController : AppBaseController
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(UserRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorRS), (int)HttpStatusCode.InternalServerError)]
    public async Task<UserRS> UserRegisterAsync(UserRegisterRQ userRegisterRQ, CancellationToken cancellationToken)
    {
        var accessToken = this.GetAccessTokenFromHeader();
        return await _userService.UserRegisterAsync(accessToken, userRegisterRQ, cancellationToken);
    }
}