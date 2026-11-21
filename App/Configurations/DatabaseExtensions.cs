
using FinanceTracker.API.DataBase.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.API.App.Configurations;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Database connection string is missing."
            );

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(connectionString)
        );

        return services;
    }
}
