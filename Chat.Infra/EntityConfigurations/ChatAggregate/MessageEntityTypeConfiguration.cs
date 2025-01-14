using Chat.Domain.AggregateModels.ChatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.EntityConfigurations.ChatAggregate;

public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> messageConfiguration)
    {
        messageConfiguration.ToTable("messages");

        messageConfiguration.HasKey(m => m.Id);
        messageConfiguration.Property(m => m.Id)
            .ValueGeneratedOnAdd();

        messageConfiguration.Property(m => m.ChatId)
            .IsRequired();

        messageConfiguration.Property(m => m.SenderId)
            .IsRequired();

        messageConfiguration.Property(m => m.Content)
            .IsRequired();

        messageConfiguration.Property(m => m.SentAt)
            .IsRequired();

        messageConfiguration.Property(m => m.IsRead)
            .IsRequired();

        messageConfiguration.Ignore(m => m.DomainEvents);
    }
}