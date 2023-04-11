using SimpleCRM.WebAPI.Middleware;

namespace SimpleCRM.WebAPI.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseSimpleCRMMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();

        return app;
    }
}