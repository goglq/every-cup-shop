namespace EveryCupShop.Core.Exceptions;

public class ApiUnauthorizedException : ApiException
{
    public ApiUnauthorizedException(string? message, Exception? innerException = null) : base(message, innerException) { }

    public ApiUnauthorizedException(string? message = "Unauthorized") : this(message, null) { }
}