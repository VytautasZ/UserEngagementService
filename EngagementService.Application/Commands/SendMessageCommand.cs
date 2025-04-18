using UserEngagement.Core.Domain;

namespace UserEngagement.Application.Commands;

public class SendMessageCommand
{
    public Message Message { get; set; }
}
