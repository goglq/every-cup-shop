using EveryCupShop.Core.Interfaces;

namespace EveryCupShop.Core.Models;

public class CupAttachment : IEntity
{
    public Guid Id { get; init; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }

    public int Amount { get; set; }
    
    // public IList<OrderItem> OrderItems { get; set; }
    public IList<Cup> Cups { get; set; }
}