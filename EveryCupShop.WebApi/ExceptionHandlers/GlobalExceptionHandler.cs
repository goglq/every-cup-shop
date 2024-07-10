using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using EveryCupShop.Core.Exceptions;
using EveryCupShop.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EveryCupShop;

public class GlobalExceptionHandler : IExceptionHandler
{
    private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

    private readonly ILogger<GlobalExceptionHandler> _logger;

    private readonly IHostEnvironment _env;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception is ApiException ? exception.Message : UnhandledExceptionMsg);

        await httpContext.Response.WriteAsJsonAsync(CreateResponse(exception.Message), cancellationToken);

        return true;
    }
    
    private ResponseMessage<ProblemDetails> CreateResponse(string message) => new (
        new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = UnhandledExceptionMsg,
            Detail = _env.IsDevelopment() ? message : null
        }, 
        false, UnhandledExceptionMsg);
}