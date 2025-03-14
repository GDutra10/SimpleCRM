namespace SimpleCRM.Backoffice.WebApp.Extensions;

public static class HttpContextExtension
{
    public static bool IsLogged(this HttpContext httpContext)
        => httpContext.Session.TryGetValue(Constants.Session.AccessToken, out _);
}