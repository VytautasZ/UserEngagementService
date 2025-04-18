using UserEngagement.Core.Domain.Enums;

namespace UserEngagement.Core.Domain;

public record MessageStatus
{
    public long Id { get; init; }
    public long MessageId { get; init; }
    public CommunicationChannelType CommunicationChannel { get; init; }
    public DateTime? DeliveryDate { get; init; }
    public MessageStatusType Status { get; init; }
}