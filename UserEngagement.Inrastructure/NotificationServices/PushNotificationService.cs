using UserEngagement.Infrastructure.Interfaces;

namespace UserEngagement.Infrastructure.NotificationServices;

public class PushNotificationService : IPushNotificationSercvice
{
    public Task PushAsync(string message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
