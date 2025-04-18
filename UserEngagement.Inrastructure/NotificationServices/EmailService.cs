using UserEngagement.Infrastructure.Interfaces;

namespace UserEngagement.Infrastructure.NotificationServices;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(string receiverEmail, string subject, string message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
