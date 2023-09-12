using DOCOsoft.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DOCOsoft.Api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DOCOsoftContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("DOCOsoftContext");
        services.AddSqlServer<DOCOsoftContext>(connString)
            .AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}
