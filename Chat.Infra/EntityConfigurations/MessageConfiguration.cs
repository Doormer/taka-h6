using Chat.Domain.AggregateModels.MessageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.EntityConfigurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");
        
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.SenderId)
            .IsRequired();
            
        builder.Property(m => m.ReceiverId)
            .IsRequired();

        builder.OwnsOne(m => m.Content, content =>
        {
            content.Property(c => c.Value)
                .HasColumnName("Content")
                .HasMaxLength(5000)
                .IsRequired();
                
            content.Property(c => c.Type)
                .HasColumnName("MessageType")
                .HasConversion<int>()
                .IsRequired();
        });

        builder.Property(m => m.Status)
            .HasConversion<int>()
            .IsRequired();
            
        builder.Property(m => m.CreatedTime)
            .IsRequired();
            
        builder.Property(m => m.ReadTime)
            .IsRequired(false);
    }
} 