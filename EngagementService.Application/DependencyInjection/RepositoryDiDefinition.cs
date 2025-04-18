using Microsoft.Extensions.DependencyInjection;
using UserEngagement.Infrastructure.Interfaces;
using UserEngagement.Infrastructure.Repositories;

namespace UserEngagement.Application.DependencyInjection;

public static class RepositoryDiDefinition
{
    public static IServiceCollection AddRepositoryDependencies(
        this IServiceCollection services)
    {
        services.AddTransient<IMessageRepository, MessageRepository>();
        return services;
    }
}