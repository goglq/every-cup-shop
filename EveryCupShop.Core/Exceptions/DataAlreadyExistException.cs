namespace EveryCupShop.Core.Exceptions;

public class DataAlreadyExistException : ApiException
{
    public DataAlreadyExistException(string? message, Exception? innerException = null) : base(message, innerException) { }
    
    public DataAlreadyExistException(string? message = "The data already exists") : this(message, null) { }
}