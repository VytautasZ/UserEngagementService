using UserEngagement.Core.Domain;
using UserEngagement.Core.Interfaces;
using UserEngagement.Infrastructure.Interfaces;

namespace UserEngagement.Application.Queries;

public class GetUserByIdQueryHandler : IQueryHandler<GetMessageByIdQuery, Message>
{
    public GetUserByIdQueryHandler() { }

    private readonly IMessageRepository _messageRepository;

    public GetUserByIdQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public Task<Message> Handle(GetMessageByIdQuery query, CancellationToken cancellationToken)
    {
        return _messageRepository.GetMessageByIdAsync(query.MessageId, cancellationToken);
    }
}
