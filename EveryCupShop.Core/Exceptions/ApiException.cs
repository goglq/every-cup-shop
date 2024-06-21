namespace EveryCupShop.Core.Exceptions;

public abstract class ApiException : Exception
{
    protected ApiException(string? message = "Api Exception") : this(message, null) { }
    
    protected ApiException(string? message, Exception? exception) : base(message, exception) { }
}