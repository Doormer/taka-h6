using Chat.Domain.AggregateModels.ReciprocalContactAggregate;
using Chat.Domain.AggregateModels.UnreadMessageAggregate;

namespace Chat.Infra.EntityConfigurations.UnreadMessageAggregate;

internal class UnreadMessageEntityTypeConfiguration : IEntityTypeConfiguration<UnreadMessage>
{
    public void Configure(EntityTypeBuilder<UnreadMessage> unreadMessageConfiguration)
    {
        unreadMessageConfiguration.ToTable("messages");

        unreadMessageConfiguration.Ignore(m => m.DomainEvents);

        unreadMessageConfiguration.Property(m => m.MessageID).HasColumnName("MessageID");
        unreadMessageConfiguration.Property(m => m.ContactId).HasColumnName("ContactId"); ;
        unreadMessageConfiguration.Property(m => m.ReadTime).HasColumnName("ReadTime");

        unreadMessageConfiguration.HasOne<Contact>()
            .WithMany()
            .HasForeignKey(m => m.ContactId);

        unreadMessageConfiguration.HasIndex(m => m.ContactId);
        unreadMessageConfiguration.HasIndex(m => m.ReadTime);
    }
}