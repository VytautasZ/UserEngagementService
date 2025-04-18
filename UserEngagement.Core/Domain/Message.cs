namespace UserEngagement.Core.Domain;

public record Message
{
    public long Id { get; init; }
    public Guid UserId { get; init; }

    public DateTime Date { get; init; }
    public string Text { get; init; } = string.Empty;
}
