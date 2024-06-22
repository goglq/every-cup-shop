namespace EveryCupShop.Dtos;

public class CreateCupAttachmentDto
{
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public decimal Price { get; init; }
    
    public int Amount { get; init; }
}