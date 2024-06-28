using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface ICupService
{
    Task<IList<Cup>> GetCups();
    
    Task<Cup> GetCup(Guid id);
    
    Task<Cup> CreateCup(string name, string description, decimal price, int amount);

    Task<Cup> ChangeCup(Guid cupId, string name, string description, decimal price, int amount);
    
    Task<Cup> DeleteCup(Guid id);
}