using Microsoft.Extensions.Logging;
using UserEngagement.Core.Domain;
using UserEngagement.Infrastructure.Interfaces;
using UserEngagement.Infrastructure.WebServices.Models;

namespace UserEngagement.Application.Strategies;

public class ViberNotificationStrategy : INotificationStrategy
{
    private readonly ILogger<ViberNotificationStrategy> _logger; 
    private readonly IViberService _viberService;

    public ViberNotificationStrategy(ILogger<ViberNotificationStrategy> logger, IViberService viberService)
    {
        _logger = logger;
        _viberService = viberService;
    }

    public async Task<NotificationResult> NotifyAsync(UserContact sender, Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending viber message to user");
        // TODO:  Put in hangfire queue
        await _viberService.SendViberMessageAsync(sender.MobileNumber, message.Text, cancellationToken);

        _logger.LogInformation("Viber sessage was successfully sent to the user");
        return await Task.FromResult(new NotificationResult { AcceptedInChannel = true });
    }

}