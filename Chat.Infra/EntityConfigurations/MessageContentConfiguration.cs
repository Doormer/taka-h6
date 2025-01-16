using Chat.Domain.AggregateModels.MessageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.EntityConfigurations;

public class MessageContentConfiguration : IEntityTypeConfiguration<MessageContent>
{
    public void Configure(EntityTypeBuilder<MessageContent> builder)
    {
        builder.HasNoKey();
        
        builder.Property(m => m.Value)
            .IsRequired()
            .HasMaxLength(5000);
            
        builder.Property(m => m.Type)
            .IsRequired();
    }
} 