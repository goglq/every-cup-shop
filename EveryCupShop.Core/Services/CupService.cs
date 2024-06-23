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

    public Task<IList<CupAttachment>> GetCupAttachments() => 
        _cupAttachmentRepository.GetAll();

    public Task<CupAttachment> GetCupAttachment(Guid id) => 
        _cupAttachmentRepository.Get(id);

    public Task<IList<Cup>> GetCups() => 
        _cupRepository.GetAll();

    public Task<Cup> GetCup(Guid id) => 
        _cupRepository.Get(id);

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

    public async Task<CupShape> CreateShape(string name, string description, decimal price, int amount)
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
}