using UserEngagement.Core.Domain;
using UserEngagement.Core.Interfaces;
using UserEngagement.Infrastructure.Interfaces;

namespace UserEngagement.Application.Queries;

public class GetMessageByIdQueryHandler : IQueryHandler<GetMessageByIdQuery, Message>
{
    public GetMessageByIdQueryHandler() { }

    private readonly IMessageRepository _messageRepository;

    public GetMessageByIdQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public Task<Message?> Handle(GetMessageByIdQuery query, CancellationToken cancellationToken)
    {
        return _messageRepository.GetMessageByIdAsync(query.MessageId, cancellationToken);
    }
}
