using Microsoft.AspNetCore.Builder;
using UserEngagement.Infrastructure;

namespace UserEngagement.Application.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task UseDatabases(this IApplicationBuilder builder)
    {
        await builder.CreateHangfireDatabaseAsync();
        await builder.ApplyMigrationsAsync();
    }
}