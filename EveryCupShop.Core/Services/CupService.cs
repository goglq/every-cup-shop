using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Services;

public class CupService : ICupService
{
    private readonly ICupShapeRepository _cupShapeRepository;

    private readonly ICupAttachmentRepository _cupAttachmentRepository;

    private readonly ICupRepository _cupRepository;

    public CupService(ICupShapeRepository cupShapeRepository, ICupAttachmentRepository cupAttachmentRepository, ICupRepository cupRepository)
    {
        _cupShapeRepository = cupShapeRepository;
        _cupAttachmentRepository = cupAttachmentRepository;
        _cupRepository = cupRepository;
    }
    
    public Task<IList<CupShape>> GetCupShapes() => 
        _cupShapeRepository.GetAll();

    public Task<CupShape> GetCupShape(Guid id) => 
        _cupShapeRepository.Get(id);

    public async Task<CupShape> CreateCupShape(string name, string description, decimal price, int amount)
    {
        var newShape = new CupShape
        {
            Name = name,
            Description = description,
            Price = price,
            Amount = amount
        };

        var createdShape = await _cupShapeRepository.Add(newShape);
        await _cupShapeRepository.Save();
        return createdShape;
    }

    public async Task<CupShape> ChangeCupShape(Guid cupShapeId, string name, string description, decimal price, int amount)
    {
        var cupShape = await _cupShapeRepository.Find(cupShapeId) ?? throw new DataNotFoundException();
        cupShape.Name = name;
        cupShape.Description = description;
        cupShape.Price = price;
        cupShape.Amount = amount;
        return cupShape;
    }

    public async Task<CupShape> DeleteCupShape(Guid id)
    {
        var cupShape = await _cupShapeRepository.Find(id) ?? throw new DataNotFoundException();

        await _cupShapeRepository.Delete(cupShape);
        await _cupShapeRepository.Save();

        return cupShape;
    }

    public Task<IList<CupAttachment>> GetCupAttachments() => 
        _cupAttachmentRepository.GetAll();

    public Task<CupAttachment> GetCupAttachment(Guid id) => 
        _cupAttachmentRepository.Get(id);
    
    public async Task<CupAttachment> CreateAttachment(string name, string description, decimal price, int amount)
    {
        var newAttachment = new CupAttachment
        {
            Name = name,
            Description = description,
            Price = price,
            Amount = amount
        };
        var createdAttachment = await _cupAttachmentRepository.Add(newAttachment);
        await _cupAttachmentRepository.Save();
        return createdAttachment;
    }

    public async Task<CupAttachment> ChangeCupAttachment(Guid cupAttachmentId, string name, string description, decimal price, int amount)
    {
        var cupAttachment = await _cupAttachmentRepository.Find(cupAttachmentId) ?? throw new DataNotFoundException();
        cupAttachment.Name = name;
        cupAttachment.Description = description;
        cupAttachment.Price = price;
        cupAttachment.Amount = amount;
        return cupAttachment;
    }

    public async Task<CupAttachment> DeleteCupAttachment(Guid id)
    {
        var cupAttachment = await _cupAttachmentRepository.Find(id) ?? throw new DataNotFoundException();
        await _cupAttachmentRepository.Delete(cupAttachment);
        await _cupAttachmentRepository.Save();
        return cupAttachment;
    }
    
    public Task<IList<Cup>> GetCups() => 
        _cupRepository.GetAll();

    public Task<Cup> GetCup(Guid id) => 
        _cupRepository.Get(id);
    
    public async Task<Cup> CreateCup(Guid cupShapeId, Guid cupAttachmentId)
    {
        var newCup = new Cup
        {
            CupShapeId = cupShapeId,
            CupAttachmentId = cupAttachmentId
        };

        var createdCup = await _cupRepository.Add(newCup);
        await _cupRepository.Save();
        return createdCup;
    }
    
    public async Task<Cup> ChangeCup(Guid cupId, Guid cupShapeId, Guid cupAttachmentId)
    {
        var cup = await _cupRepository.Find(cupId) ?? throw new DataNotFoundException();

        cup.CupShapeId = cupShapeId;
        cup.CupAttachmentId = cupAttachmentId;
        
        var updatedCup = await _cupRepository.Update(cup);
        await _cupRepository.Save();

        return updatedCup;
    }

    public async Task<Cup> DeleteCup(Guid id)
    {
        var cup = await _cupRepository.Find(id) ?? throw new DataNotFoundException();
        await _cupRepository.Delete(cup);
        await _cupRepository.Save();
        return cup;
    }
}