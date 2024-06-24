using EveryCupShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EveryCupShop.Extensions;

public static class WebApplicationExtensions
{
    public static void Migrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}