using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface ICupService
{
    Task<IList<CupShape>> GetCupShapes();
    
    Task<CupShape> GetCupShape(Guid id);
    
    Task<CupShape> CreateCupShape(string name, string description, decimal price, int amount);
    
    Task<CupShape> ChangeCupShape(Guid cupShapeId, string name, string description, decimal price, int amount);

    Task<CupShape> DeleteCupShape(Guid id);

    Task<IList<CupAttachment>> GetCupAttachments();

    Task<CupAttachment> GetCupAttachment(Guid id);

    Task<CupAttachment> CreateAttachment(string name, string description, decimal price, int amount);
    
    Task<CupAttachment> ChangeCupAttachment(Guid cupAttachmentId, string name, string description, decimal price, int amount);
    
    Task<CupAttachment> DeleteCupAttachment(Guid id);

    Task<IList<Cup>> GetCups();
    
    Task<Cup> GetCup(Guid id);
    
    Task<Cup> CreateCup(Guid cupShapeId, Guid cupAttachmentId);

    Task<Cup> ChangeCup(Guid cupId, Guid cupShapeId, Guid cupAttachmentId);
    
    Task<Cup> DeleteCup(Guid id);
}