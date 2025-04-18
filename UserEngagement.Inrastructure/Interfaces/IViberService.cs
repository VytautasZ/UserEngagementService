namespace UserEngagement.Infrastructure.Interfaces;

public interface IViberService
{
    public Task SendViberMessageAsync(string phoneNumber, string message, CancellationToken cancellationToken);
}