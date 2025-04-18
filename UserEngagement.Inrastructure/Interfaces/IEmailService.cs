namespace UserEngagement.Infrastructure.Interfaces;

public interface IEmailService
{
    public Task SendEmailAsync(string receiverEmail, string subject, string message, CancellationToken cancellationToken);
}