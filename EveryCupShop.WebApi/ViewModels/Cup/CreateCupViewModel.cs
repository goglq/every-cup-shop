namespace EveryCupShop.ViewModels;

public class CreateCupViewModel
{
    public Guid Id { get; set; }
    
    public CreateCupShapeViewModel CupShape { get; set; }
    
    public Guid CupShapeId { get; set; }
    
    public CreateCupAttachmentViewModel CupAttachment { get; set; }
    
    public Guid CupAttachmentId { get; set; }
}