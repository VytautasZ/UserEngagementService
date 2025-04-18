using UserEngagement.Infrastructure.Interfaces;
using UserEngagement.Infrastructure.WebServices.Models;

namespace UserEngagement.Infrastructure.WebServices;

public class UserContactsService : IUserContacService
{
    public async Task<UserContact> GetUserContactsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var userContact = new UserContact()
        {
            MobileNumber = "+370000000000",
            Email = "simpe@email.com",
            CommunicationChannels = new List<PreferedCommunicationChannel>() {
               PreferedCommunicationChannel.Email,  
               PreferedCommunicationChannel.SMS,
               PreferedCommunicationChannel.PushNotifications,
               PreferedCommunicationChannel.Viber
            }
        };

        return await ValueTask.FromResult(userContact);
    }
}
