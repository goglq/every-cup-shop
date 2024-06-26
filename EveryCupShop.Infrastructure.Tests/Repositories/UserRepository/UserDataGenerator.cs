using System.Collections;
using EveryCupShop.Core.Models;

namespace EveryCupShop.Infrastructure.Tests.Repositories;

public class UserDataGenerator : IEnumerable<object[]>
{
    private readonly IReadOnlyList<object[]> _data;

    public UserDataGenerator()
    {
        _data = new[]
        {
            new object[]
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "user1@gmail.com",
                    Password = "user1's password",
                    Roles = new List<Role>
                    {
                        new()
                        {
                            Name = "User"
                        }
                    }
                }
            },
            new object[]
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "user2@gmail.com",
                    Password = "user2's password",
                    Roles = new List<Role>
                    {
                        new()
                        {
                            Name = "User"
                        }
                    }
                },
            },
            new object[]
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "user3@gmail.com",
                    Password = "user3's password",
                    Roles = new List<Role>
                    {
                        new()
                        {
                            Name = "User"
                        }
                    }
                }
            }
        };
    }

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}