namespace EveryCupShop.ViewModels;

public class CreateCupAttachmentViewModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int Amount { get; set; }
}