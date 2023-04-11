using Microsoft.AspNetCore.Mvc;

namespace SimpleCRM.WebAPI.Controllers;

public abstract class AppBaseController : ControllerBase
{
    protected string GetAccessTokenFromHeader()
    {
        var split = this.Request.Headers.Authorization.ToString().Split(" ");

        return split.Length > 1 ? split[1] : string.Empty;
    }
}