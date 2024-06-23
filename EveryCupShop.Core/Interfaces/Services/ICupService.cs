using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface ICupService
{
    Task<IList<CupShape>> GetCupShapes();
    
    Task<CupShape> GetCupShape(Guid id);

    Task<IList<CupAttachment>> GetCupAttachments();

    Task<CupAttachment> GetCupAttachment(Guid id);

    Task<IList<Cup>> GetCups();

    Task<Cup> GetCup(Guid id);

    Task<CupAttachment> CreateAttachment(string name, string description, decimal price, int amount);

    Task<CupShape> CreateShape(string name, string description, decimal price, int amount);

    Task<Cup> CreateCup(Guid cupShapeId, Guid cupAttachmentId);

    Task<Cup> ChangeCup(Guid cupId, Guid cupShapeId, Guid cupAttachmentId);
}