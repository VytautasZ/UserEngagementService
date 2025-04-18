namespace UserEngagement.Infrastructure.Interfaces;

public interface IPushNotificationSercvice
{
    public Task PushAsync(string message, CancellationToken cancellationToken);
}