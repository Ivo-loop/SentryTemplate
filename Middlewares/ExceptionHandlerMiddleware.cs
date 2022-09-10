using System.Net;
using Sentry;
using SentryTemplate.HttpsException;

namespace SentryTemplate.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            context.Response.ContentType = "application/json";
            switch (e)
            {
                case BadRequestException:
                    _logger.LogWarning(e, $"Expected error: {e.Message}");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync("Bad Request");
                    break;
                default:
                    _logger.LogError(e, "Unexpected error");
                    SentrySdk.CaptureException(e);
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync("Unexpected error, contact Administrator.");
                    break;
            }
        }
    }
}