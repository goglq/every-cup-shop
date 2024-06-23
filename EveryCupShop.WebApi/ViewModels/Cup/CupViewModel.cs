namespace EveryCupShop.ViewModels;

public class CupViewModel
{
    public Guid Id { get; set; }
    
    public CupShapeViewModel CupShape { get; set; }
    
    public Guid CupShapeId { get; set; }

    public CupAttachmentViewModel CupAttachment { get; set; }
    
    public Guid CupAttachmentId { get; set; }
}