using Microsoft.Extensions.Logging;
using UserEngagement.Core.Domain;
using UserEngagement.Infrastructure.Interfaces;
using UserEngagement.Infrastructure.WebServices.Models;

namespace UserEngagement.Application.Strategies;

public class PushNotificationStrategy : INotificationStrategy
{
    private readonly ILogger<PushNotificationStrategy> _logger; 
    private readonly IPushNotificationSercvice _pushNotificationService;

    public PushNotificationStrategy(ILogger<PushNotificationStrategy> logger, IPushNotificationSercvice pushNotificationService)
    {
        _logger = logger;
        _pushNotificationService = pushNotificationService;
    }

    public async Task<NotificationResult> NotifyAsync(UserContact sender, Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Pushing the notification to user");
        // TODO:  Put in hangfire queue
        await _pushNotificationService.PushAsync(message.Text, cancellationToken);

        _logger.LogInformation("Message was successfully pushed to the user");
        return await Task.FromResult(new NotificationResult { AcceptedInChannel = true });
    }

}