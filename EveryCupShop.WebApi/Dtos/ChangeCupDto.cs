namespace EveryCupShop.Dtos;

public class ChangeCupDto
{
    public Guid CupId { get; init; }
    
    public Guid CupAttachmentId { get; init; }
    
    public Guid CupShapeId { get; init; }
}