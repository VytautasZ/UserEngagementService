namespace UserEngagement.Application.Services.Jobs;

public interface IJob
{
    public Task ExecuteAsync();
}