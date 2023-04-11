using SimpleCRM.WebAPI.Handlers;

namespace SimpleCRM.WebAPI.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ExceptionHandler _exceptionHandler;

    public ErrorHandlerMiddleware(RequestDelegate next, ExceptionHandler exceptionHandler)
    {
        _next = next;
        _exceptionHandler = exceptionHandler;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            await _exceptionHandler.Handler(context, error);
        }
    }
}