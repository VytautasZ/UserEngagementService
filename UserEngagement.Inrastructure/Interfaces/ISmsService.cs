namespace UserEngagement.Infrastructure.Interfaces;

public interface ISmsService
{
    public Task SendSmsAsync(string phoneNumber, string message, CancellationToken cancellationToken);
}