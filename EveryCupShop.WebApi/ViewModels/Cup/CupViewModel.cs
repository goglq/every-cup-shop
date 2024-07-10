namespace EveryCupShop.ViewModels;

public class CupViewModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int Amount { get; set; }
}