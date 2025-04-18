namespace UserEngagement.Infrastructure.WebServices.Models
{
    public record UserContact
    {
        public string Email { get; init; } = string.Empty;
        public string MobileNumber { get; init; } = string.Empty;

        public List<PreferedCommunicationChannel> CommunicationChannels { get; init; }
    }
}