namespace EveryCupShop.Core.Exceptions;

public class DataNotFoundException : ApiException
{
    public DataNotFoundException(string? message, Exception? innerException = null) : base(message, innerException) { }
    
    public DataNotFoundException(string? message = "The data is not found") : this(message, null) { }
}