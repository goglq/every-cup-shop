namespace EveryCupShop.Core.Exceptions;

public class EfException : ApiException
{
    public EfException(string? message, Exception? exception) : base(message, exception) { }
    
    public EfException(string? message = "Ef error") : this(message, null) { }
}