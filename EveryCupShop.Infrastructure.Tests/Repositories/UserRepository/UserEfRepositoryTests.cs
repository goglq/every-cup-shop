using EveryCupShop.Core.Models;
using EveryCupShop.Infrastructure.Database;
using EveryCupShop.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EveryCupShop.Infrastructure.Tests.Repositories;

public class UserEfRepositoryTests : IDisposable
{
    private readonly DbContextOptions<AppDbContext> _options;
    
    private readonly IReadOnlyList<User> _pocketUsers;
    
    public UserEfRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        
        using var context = new AppDbContext(_options);
        var roles = context.Roles.ToList();
        _pocketUsers = Enumerable.Range(0, 3)
            .Select(i => new User
            {
                Id = Guid.NewGuid(),
                Email = $"user{i}@email.com",
                Password = $"user{i}'s password",
                Roles = roles.Where(role => role.Name == "User").ToList()
            })
            .ToList();
    }
    
    [Theory]
    [ClassData(typeof(UserDataGenerator))]
    public async Task Add_Single_User_Success(User user)
    {
        // Arrange
        await using var context = new AppDbContext(_options);
        var repository = new UserEfRepository(context);
        
        // Act
        var newUser = Clone(user);
        var result = await repository.Add(newUser);
        await repository.Save();

        // Assert
        var userFromContext = await context.Users.FirstAsync();
        
        result.Should().NotBeNull();
        result.Should().NotBeSameAs(user);
        result.Id.Should().Be(userFromContext.Id).And.Be(user.Id);
        result.Email.Should().Be(userFromContext.Email).And.Be(user.Email);
    }

    [Fact]
    public async Task Get_Many_Users_Success()
    {
        // Arrange
        await using var context = new AppDbContext(_options);
        await context.Users.AddRangeAsync(_pocketUsers);
        await context.SaveChangesAsync();
        
        var repository = new UserEfRepository(context);
        
        // Act
        var usersFromRepo = await repository.GetAll();
        
        // Assert
        usersFromRepo.Should().NotBeNullOrEmpty();
        usersFromRepo.Count.Should().Be(_pocketUsers.Count);
    }

    [Fact]
    public async Task Get_Single_User_By_Id_Success()
    {
        // Arrange
        await using var context = new AppDbContext(_options);

        var pocketUser = _pocketUsers[0];
        var expectedUser = Clone(pocketUser);
        
        await context.Users.AddAsync(pocketUser);
        await context.SaveChangesAsync();
        
        var repository = new UserEfRepository(context);

        // Act
        var userFromRepo = await repository.Get(pocketUser.Id);

        // Assert
        userFromRepo.Should().NotBeNull();
        userFromRepo.Should().NotBeSameAs(expectedUser);
        userFromRepo.Id.Should().Be(expectedUser.Id);
    }

    [Fact]
    public async Task Get_Single_User_By_Email_Success()
    {
        // Arrange
        await using var context = new AppDbContext(_options);

        var pocketUser = _pocketUsers[1];
        var expectedUser = Clone(pocketUser);

        await context.Users.AddAsync(pocketUser);
        await context.SaveChangesAsync();

        var repository = new UserEfRepository(context);

        // Act
        var userFromRepo = await repository.Get(pocketUser.Email);

        // Assert
        userFromRepo.Should().NotBeNull();
        userFromRepo.Should().NotBeSameAs(expectedUser);
        userFromRepo.Email.Should().Be(expectedUser.Email);
    }

    private static User Clone(User user) => new()
    {
        Id = user.Id,
        Email = user.Email,
        Password = (string)user.Password.Clone(),
        Roles = user.Roles,
        Orders = user.Orders,
        Token = user.Token
    };
    
    public void Dispose()
    {
        using var context = new AppDbContext(_options);
        context.Database.EnsureDeleted();
    }
}