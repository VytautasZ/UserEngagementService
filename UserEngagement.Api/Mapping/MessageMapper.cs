using UserEngagement.Api.Contracts;
using UserEngagement.Core.Domain;

namespace UserEngagement.Api.Mapping;

public static class MessageMapper
{
    public static Message ToMessage(this MessageBaseDto messageDto)
    {
        return new()
        {
            UserId = messageDto.UserId,
            Text = messageDto.Text
        };
    }

    public static MessageDto ToMessageDto(this Message message)
    {
        return new()
        {
            Id = message.Id,
            UserId = message.UserId,
            Text = message.Text
        };
    }
}