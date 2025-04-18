using Microsoft.Extensions.Logging;
using UserEngagement.Core.Domain;
using UserEngagement.Infrastructure.Interfaces;
using UserEngagement.Infrastructure.WebServices.Models;

namespace UserEngagement.Application.Strategies;

public class SmsNotificationStrategy : INotificationStrategy
{
    private readonly ILogger<SmsNotificationStrategy> _logger; 
    private readonly ISmsService _smsNotificationService;

    public SmsNotificationStrategy(ILogger<SmsNotificationStrategy> logger, ISmsService pushNotificationService)
    {
        _logger = logger;
        _smsNotificationService = pushNotificationService;
    }

    public async Task<NotificationResult> NotifyAsync(UserContact sender, Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending sms to user");
        // TODO:  Put in hangfire queue
        await _smsNotificationService.SendSmsAsync(sender.MobileNumber, message.Text, cancellationToken);

        _logger.LogInformation("Sms sessage was successfully sent to the user");
        return await Task.FromResult(new NotificationResult { AcceptedInChannel = true });
    }

}