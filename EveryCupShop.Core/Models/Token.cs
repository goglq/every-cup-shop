using EveryCupShop.Core.Interfaces;

namespace EveryCupShop.Core.Models;

public class Token : IEntity
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    public string RefreshToken { get; set; }
}