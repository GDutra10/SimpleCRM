using Microsoft.AspNetCore.Mvc.Filters;
using SimpleCRM.Backoffice.WebApp.API;

namespace SimpleCRM.Backoffice.WebApp.Filters;

public class SimpleCRMAuthorizeFilter : IAsyncAuthorizationFilter
{
    private readonly ISimpleCRMApi _simpleCRMApi;

    public SimpleCRMAuthorizeFilter(ISimpleCRMApi simpleCRMApi)
    {
        _simpleCRMApi = simpleCRMApi;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        var accessToken =
            context.HttpContext.Session.GetString(Constants.Session.AccessToken);

        if (accessToken is null ||
            !await _simpleCRMApi.ValidateTokenAsync(accessToken, CancellationToken.None))
            context.HttpContext.Response.Redirect("/Login");
    }
}