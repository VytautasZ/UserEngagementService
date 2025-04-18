using UserEngagement.Core.Domain;
using UserEngagement.Infrastructure.WebServices.Models;

namespace UserEngagement.Application.Strategies;

public interface INotificationStrategy
{
    public Task<NotificationResult> NotifyAsync(UserContact sender, Message message, CancellationToken cancellationToken);
}