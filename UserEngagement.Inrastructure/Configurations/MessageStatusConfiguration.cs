using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserEngagement.Core.Domain;
using UserEngagement.Core.Domain.Enums;

namespace UserEngagement.Infrastructure.Configurations;

internal sealed class MessageStatusConfiguration : IEntityTypeConfiguration<MessageStatus>
{
    public void Configure(EntityTypeBuilder<MessageStatus> builder)
    {
        builder.ToTable(Db.UserEngagement.Tables.MESSAGE_STATUSES, Db.UserEngagement.SCHEMA);

        builder.HasKey(ms => ms.Id);
        builder.Property(ms => ms.Id).IsRequired();
        builder.Property(ms => ms.MessageId).IsRequired();
        builder.Property(ms => ms.DeliveryDate).IsRequired(false);
        builder.Property(ms => ms.CommunicationChannel)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<CommunicationChannelType>());
        builder.Property(ms => ms.Status)
            .IsRequired()
            .HasDefaultValue(MessageStatusType.Accepted)
            .HasConversion(new EnumToStringConverter<MessageStatusType>());

        builder.HasOne<Message>().WithMany().HasForeignKey(ms => ms.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}