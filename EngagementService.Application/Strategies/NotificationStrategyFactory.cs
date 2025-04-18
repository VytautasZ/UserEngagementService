using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserEngagement.Infrastructure.Interfaces;
using UserEngagement.Infrastructure.WebServices.Models;

namespace UserEngagement.Application.Strategies;

public static class NotificationStrategyFactory
{
    public static INotificationStrategy Create(IServiceProvider serviceProvider, PreferedCommunicationChannel channel)
    {
        return channel switch
        {
            PreferedCommunicationChannel.Email => CreateEMailNotificationStrategy(serviceProvider),
            PreferedCommunicationChannel.SMS => CreateSmsNotificationStrategy(serviceProvider),
            PreferedCommunicationChannel.PushNotifications => CreatePushNotificationStrategy(serviceProvider),
            PreferedCommunicationChannel.Viber => CreatePushNotificationStrategy(serviceProvider),
            _ => throw UnknownCommunicationChannelException(channel)
        };
    }

    private static EmailNotificationStrategy CreateEMailNotificationStrategy(IServiceProvider serviceProvider)
    {
        return new EmailNotificationStrategy
        (
            serviceProvider.GetRequiredService<ILogger<EmailNotificationStrategy>>(),
            serviceProvider.GetRequiredService<IEmailService>()
        );
    }

    private static SmsNotificationStrategy CreateSmsNotificationStrategy(IServiceProvider serviceProvider)
    {
        return new SmsNotificationStrategy
        (
            serviceProvider.GetRequiredService<ILogger<SmsNotificationStrategy>>(),
            serviceProvider.GetRequiredService<ISmsService>()
        );
    }

    private static PushNotificationStrategy CreatePushNotificationStrategy(IServiceProvider serviceProvider)
    {
        return new PushNotificationStrategy
        (
            serviceProvider.GetRequiredService<ILogger<PushNotificationStrategy>>(),
            serviceProvider.GetRequiredService<IPushNotificationSercvice>()
        );
    }

    private static ViberNotificationStrategy ViberNotificationStrategy(IServiceProvider serviceProvider)
    {
        return new ViberNotificationStrategy
        (
            serviceProvider.GetRequiredService<ILogger<ViberNotificationStrategy>>(),
            serviceProvider.GetRequiredService<IViberService>()
        );
    }

    private static ArgumentOutOfRangeException UnknownCommunicationChannelException(PreferedCommunicationChannel channel)
    {
        return new(
            nameof(PreferedCommunicationChannel),
            channel.GetType(),
            $"The communication channel {channel.GetType()} is not yet supported."
        );
    }
}