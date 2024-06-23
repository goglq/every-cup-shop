using EveryCupShop.Core.Interfaces;

namespace EveryCupShop.Core.Models;

public class Cup : IEntity
{
    public Guid Id { get; init; }
    
    public CupShape CupShape { get; set; }
    
    public Guid CupShapeId { get; set; }
    
    public CupAttachment CupAttachment { get; set; }
    
    public Guid CupAttachmentId { get; set; }
}