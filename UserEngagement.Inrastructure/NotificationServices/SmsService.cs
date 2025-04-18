using UserEngagement.Infrastructure.Interfaces;

namespace UserEngagement.Infrastructure.NotificationServices;

public class SmsService : ISmsService
{
    public Task SendSmsAsync(string mobilePhone, string message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
