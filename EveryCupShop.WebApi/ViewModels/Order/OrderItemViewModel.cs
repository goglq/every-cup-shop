namespace EveryCupShop.ViewModels;

public class OrderItemViewModel
{
    public Guid Id { get; set; }
    
    public OrderViewModel Order { get; set; }
    
    public Guid OrderId { get; set; }
    
    public CupViewModel Cup { get; set; }
    
    public Guid CupId { get; set; }
    
    public int Amount { get; set; }
}