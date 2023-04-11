﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Domain.Common.DTOs;
using SimpleCRM.Domain.Common.Services;

namespace SimpleCRM.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
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
    [ProducesResponseType(typeof(InsertUserRS), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ValidationRS), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorRS), (int)HttpStatusCode.InternalServerError)]
    public async Task<InsertUserRS> InsertUserAsync(InsertUserRQ insertUserRQ, CancellationToken cancellationToken)
    {
        return await _userService.InsertUserAsync(insertUserRQ, cancellationToken);
    }
}