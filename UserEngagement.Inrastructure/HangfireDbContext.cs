using Microsoft.EntityFrameworkCore;

namespace UserEngagement.Infrastructure;

public sealed class HangfireDbContext : DbContext
{
    public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
    {
    }
}