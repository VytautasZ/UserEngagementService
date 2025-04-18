using UserEngagement.Infrastructure.Interfaces;

namespace UserEngagement.Infrastructure.NotificationServices;

public class ViberService : IViberService
{
    public Task SendViberMessageAsync(string phoneNumber, string message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
