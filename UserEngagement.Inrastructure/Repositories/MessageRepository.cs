using Microsoft.EntityFrameworkCore;
using UserEngagement.Core.Domain;
using UserEngagement.Infrastructure.Interfaces;

namespace UserEngagement.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private ServiceDbContext _dbContext;

    public MessageRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<long> CreateMessageAsync(Message message, CancellationToken cancellationToken)
    {
        await _dbContext.Set<Message>().AddAsync(message, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return message.Id;
    }

    public async Task<Message?> GetMessageByIdAsync(long messageId, CancellationToken cancellationToken)
    => await _dbContext.Set<Message>()
        .AsNoTracking()
        .FirstOrDefaultAsync(cer => cer.Id == messageId, cancellationToken);
}