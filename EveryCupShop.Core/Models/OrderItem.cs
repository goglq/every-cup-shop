using EveryCupShop.Core.Interfaces;

namespace EveryCupShop.Core.Models;

public class OrderItem : IEntity
{
    public Guid Id { get; init; }
    
    public Order Order { get; set; }
    
    public Guid OrderId { get; set; }
    
    public CupShape CupShape { get; set; }
    
    public Guid CupShapeId { get; set; }
    
    public CupAttachment CupAttachment { get; set; }
    
    public Guid CupAttachmentId { get; set; }

    public int Amount { get; set; } = 1;
}