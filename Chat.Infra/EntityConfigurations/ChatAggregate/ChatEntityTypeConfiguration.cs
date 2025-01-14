using Chat.Domain.AggregateModels.ChatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.EntityConfigurations.ChatAggregate;

public class ChatEntityTypeConfiguration : IEntityTypeConfiguration<Domain.AggregateModels.ChatAggregate.Chat>
{
    public void Configure(EntityTypeBuilder<Domain.AggregateModels.ChatAggregate.Chat> chatConfiguration)
    {
        chatConfiguration.ToTable("chats");

        chatConfiguration.HasKey(c => c.Id);
        chatConfiguration.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        chatConfiguration.Property(c => c.Name)
            .IsRequired();

        chatConfiguration.Property(c => c.CreatedAt)
            .IsRequired();

        chatConfiguration.HasMany(c => c.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId);

        chatConfiguration.Ignore(c => c.DomainEvents);
    }
}