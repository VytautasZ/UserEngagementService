using Microsoft.AspNetCore.Builder;
using UserEngagement.Infrastructure;

namespace UserEngagement.Application.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task UseDatabasesAsync(this IApplicationBuilder builder)
    {
        await builder.CreateHangfireDatabaseAsync();
        await builder.ApplyMigrationsAsync();
    }
}