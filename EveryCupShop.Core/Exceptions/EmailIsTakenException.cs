namespace EveryCupShop.Core.Exceptions;

public class EmailIsTakenException : ApiException
{
    public EmailIsTakenException(string? message, Exception? exception = null) : base(message, exception) { } 
    
    public EmailIsTakenException(string? message = "Email is already taken") : this(message, null) { }
}