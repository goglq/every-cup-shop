namespace EveryCupShop.Middlewares;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<LoggerMiddleware> _logger;

    public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request Header: {RequestHeaders}", context.Request.Headers);
        await _next(context);
        _logger.LogInformation("Response Header: {ResponseHeaders}", context.Response.Headers);
    }
}