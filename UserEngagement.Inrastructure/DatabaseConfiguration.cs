using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserEngagement.Infrastructure;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        string? dbConnectionString = configuration.GetConnectionString(Db.UserEngagement.CONNECTION_STRING_NAME);
        serviceCollection.AddDbContext<ServiceDbContext>(options => options.UseSqlServer(dbConnectionString,
            x => x.MigrationsHistoryTable("_MigrationHistory", Db.UserEngagement.SCHEMA)));
        return serviceCollection;
    }

    public static IServiceCollection ConfigureHangfireDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        string? dbConnectionString = configuration.GetConnectionString(Db.JobManagement.CONNECTION_STRING_NAME);
        serviceCollection.AddDbContext<HangfireDbContext>(options => options.UseSqlServer(dbConnectionString));
        return serviceCollection;
    }

    public static async Task CreateHangfireDatabaseAsync(this IApplicationBuilder builder)
    {
        using IServiceScope scope = builder.ApplicationServices.CreateScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        DbContext dbContext = serviceProvider.GetRequiredService<HangfireDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public static async Task ApplyMigrationsAsync(this IApplicationBuilder builder)
    {
        using IServiceScope scope = builder.ApplicationServices.CreateScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        DbContext dbContext = serviceProvider.GetRequiredService<ServiceDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}