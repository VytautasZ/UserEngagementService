using Microsoft.Extensions.Logging;
using UserEngagement.Core.Domain;
using UserEngagement.Infrastructure.Interfaces;
using UserEngagement.Infrastructure.WebServices.Models;

namespace UserEngagement.Application.Strategies;

public class EmailNotificationStrategy : INotificationStrategy
{
    private readonly ILogger<EmailNotificationStrategy> _logger; 
    private readonly IEmailService _emailService;

    public EmailNotificationStrategy(ILogger<EmailNotificationStrategy> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public async Task<NotificationResult> NotifyAsync(UserContact sender, Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending email to user");
        await _emailService.SendEmailAsync(sender.Email, message.Text, "Some default subject", cancellationToken);
        // TODO:  Put in hangfire queue

        _logger.LogInformation("Email was successfully sent to the user");
        return await Task.FromResult(new NotificationResult { AcceptedInChannel = true });
    }
}