using Chat.Domain.AggregateModels.MessageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.EntityConfigurations.MessageAggregate;

internal class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> messageConfiguration)
    {
        messageConfiguration.ToTable("messages");

        messageConfiguration.HasKey(m => m.Id);
        
        messageConfiguration.Ignore(m => m.DomainEvents);

        messageConfiguration.Property(m => m.SenderId)
            .HasColumnName("SenderId")
            .IsRequired();

        messageConfiguration.Property(m => m.ReceiverId)
            .HasColumnName("ReceiverId")
            .IsRequired();

        messageConfiguration.Property(m => m.Content)
            .HasColumnName("Content")
            .HasMaxLength(1000)
            .IsRequired();

        messageConfiguration.Property(m => m.SentTime)
            .HasColumnName("SentTime")
            .IsRequired();

        messageConfiguration.Property(m => m.IsRead)
            .HasColumnName("IsRead")
            .HasDefaultValue(false);

        // 创建索引以优化查询性能
        messageConfiguration.HasIndex(m => m.SenderId);
        messageConfiguration.HasIndex(m => m.ReceiverId);
        messageConfiguration.HasIndex(m => m.SentTime);
    }
}
