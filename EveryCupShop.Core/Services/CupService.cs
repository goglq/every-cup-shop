using EveryCupShop.Core.Exceptions;
using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Interfaces.Services;
using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Services;

public class CupService : ICupService
{
    private readonly ICupRepository _cupRepository;

    public CupService(ICupRepository cupRepository)
    {
        _cupRepository = cupRepository;
    }
    
    public Task<IList<Cup>> GetCups() => 
        _cupRepository.GetAll();

    public Task<Cup> GetCup(Guid id) => 
        _cupRepository.Get(id);
    
    public async Task<Cup> CreateCup(string name, string description, decimal price, int amount)
    {
        var newCup = new Cup
        {
            Name = name,
            Description = description,
            Price = price,
            Amount = amount
        };

        var createdCup = await _cupRepository.Add(newCup);
        await _cupRepository.Save();
        return createdCup;
    }
    
    public async Task<Cup> ChangeCup(Guid cupId, string name, string description, decimal price, int amount)
    {
        var cup = await _cupRepository.Find(cupId) ?? throw new DataNotFoundException();

        cup.Name = name;
        cup.Description = description;
        cup.Price = price;
        cup.Amount = amount;
        
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