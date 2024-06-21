namespace EveryCupShop.Models;

public class ResponseMessage<TData> 
{
    public ResponseMessage(TData? data, bool isSuccess, string? message = default)
    {
        Message = message;
        IsSuccess = isSuccess;
        Data = data;
    }

    public string? Message { get; set; }
    
    public bool IsSuccess { get; set; }
    
    public TData? Data { get; set; }
}