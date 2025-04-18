using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserEngagement.Infrastructure;

namespace UserEngagement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDatabases(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
            serviceCollection.ConfigureDatabase(configuration);
            serviceCollection.ConfigureHangfireDatabase(configuration);
            
        return serviceCollection;
    }
}