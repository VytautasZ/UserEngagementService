using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserEngagement.Core.Domain;

namespace UserEngagement.Infrastructure.Configurations;

internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable(Db.UserEngagement.Tables.MESSAGES, Db.UserEngagement.SCHEMA);

        builder.HasKey(message => message.Id);
        builder.Property(message => message.Id).IsRequired();
        builder.Property(message => message.UserId).IsRequired();
        builder.Property(message => message.Text).IsRequired();
    }
}