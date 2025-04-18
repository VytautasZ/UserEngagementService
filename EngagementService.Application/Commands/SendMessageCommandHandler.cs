using UserEngagement.Application.Strategies;
using UserEngagement.Core.Domain;
using UserEngagement.Core.Interfaces;
using UserEngagement.Infrastructure.Interfaces;
using UserEngagement.Infrastructure.WebServices.Models;

namespace UserEngagement.Application.Commands;

public class SendMessageCommandHandler : ICommandHandler<SendMessageCommand, long>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserContacService _userContacService;
    private readonly IServiceProvider _serviceProvider;

    public SendMessageCommandHandler(IMessageRepository messageRepository, IUserContacService userContacService, IServiceProvider serviceProvider)
    {
        _messageRepository = messageRepository;
        _userContacService = userContacService;
        _serviceProvider = serviceProvider;
    }

    public async Task<long> Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        var messageId = await _messageRepository.CreateMessageAsync(command.Message, cancellationToken);

        var userContactInfo = await _userContacService.GetUserContactsAsync(command.Message.UserId, cancellationToken);

        var results = await SendMessageAsync(userContactInfo, command.Message, cancellationToken);

        //save result to database

        return messageId;
    }


    private async Task<List<NotificationResult>> SendMessageAsync(UserContact userContact, Message message, CancellationToken cancellationToken)
    {
        List<Task<NotificationResult>> tasks = userContact.CommunicationChannels.Select(async channel =>
            await NotificationStrategyFactory.Create(_serviceProvider, channel)
                .NotifyAsync(userContact, message, cancellationToken))
            .ToList();

        await Task.WhenAll(tasks);

        return tasks.Select(completedTask => completedTask.Result).ToList();
    }
}
