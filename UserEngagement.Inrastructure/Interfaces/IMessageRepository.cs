using UserEngagement.Core.Domain;

namespace UserEngagement.Infrastructure.Interfaces;

public interface IMessageRepository
{
    Task<long> CreateMessageAsync(Message message, CancellationToken cancellationToken);
    Task<Message?> GetMessageByIdAsync(long messageId, CancellationToken cancellationToken);
}