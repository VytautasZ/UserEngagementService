using UserEngagement.Infrastructure.WebServices.Models;

namespace UserEngagement.Infrastructure.Interfaces;

public interface IUserContacService
{
    public Task<UserContact> GetUserContactsAsync(Guid userId, CancellationToken cancellationToken);
}