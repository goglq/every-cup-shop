namespace EveryCupShop.Core.Exceptions;

public class ApiValidationException : ApiException
{
    public ApiValidationException(string? message = "Validation Exception") : base(message) { }
}