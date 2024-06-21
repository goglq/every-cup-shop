using EveryCupShop.Core.Interfaces;

namespace EveryCupShop.Core.Models;

public class User : IEntity
{
    public Guid Id { get; init; }

    public string Email { get; set; }
    
    public string Password { get; set; }
}