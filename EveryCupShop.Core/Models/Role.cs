using EveryCupShop.Core.Interfaces;

namespace EveryCupShop.Core.Models;

public class Role : IEntity
{
    public Guid Id { get; init; }
    
    public string Name { get; set; }
    
    public IList<User> Users { get; set; }
}