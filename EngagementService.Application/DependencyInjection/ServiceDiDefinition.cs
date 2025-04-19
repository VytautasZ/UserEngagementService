using Microsoft.Extensions.DependencyInjection;
using UserEngagement.Application.Commands;
using UserEngagement.Application.Dispatchers;
using UserEngagement.Application.Queries;
using UserEngagement.Core.Domain;
using UserEngagement.Core.Interfaces;
using UserEngagement.Infrastructure.Interfaces;
using UserEngagement.Infrastructure.NotificationServices;
using UserEngagement.Infrastructure.WebServices;

namespace UserEngagement.Application.DependencyInjection;

public static class ServiceDiDefinition
{
    public static IServiceCollection AddServiceDependencies(
        this IServiceCollection services)
    {
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddTransient<IUserContacService, UserContactsService>();

        services.AddScoped<ICommandHandler<SendMessageCommand, long>, SendMessageCommandHandler>();
        services.AddScoped<IQueryHandler<GetMessageByIdQuery, Message>, GetMessageByIdQueryHandler>();

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ISmsService, SmsService>();
        services.AddScoped<IPushNotificationSercvice, PushNotificationService>();
        services.AddScoped<IViberService, ViberService>();

        return services;
    }
}