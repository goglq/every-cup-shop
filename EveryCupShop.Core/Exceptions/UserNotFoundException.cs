namespace EveryCupShop.Core.Exceptions;

public class UserNotFoundException : ApiException
{
    public UserNotFoundException(string? message, Exception? exception = null) : base(message, exception) { }

    public UserNotFoundException(string? message = "User is not found") : this(message, null) { }
}