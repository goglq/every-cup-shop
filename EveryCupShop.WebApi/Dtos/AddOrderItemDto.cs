namespace EveryCupShop.Dtos;

public class AddOrderItemDto
{
    public Guid CupId { get; init; }
    
    public Guid OrderId { get; init; }
    
    public int Amount { get; set; }
}