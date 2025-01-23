using Chat.Domain.AggregateModels.ReciprocalContactAggregate;
using Chat.Domain.AggregateModels.UnreadMessageAggregate;

namespace Chat.Infra.EntityConfigurations.UnreadMessageAggregate;

internal class UnreadMessageEntityTypeConfiguration : IEntityTypeConfiguration<UnreadMessage>
{
    public void Configure(EntityTypeBuilder<UnreadMessage> unreadMessageConfiguration)
    {
        unreadMessageConfiguration.ToTable("messages");

        unreadMessageConfiguration.Ignore(m => m.DomainEvents);

        unreadMessageConfiguration.HasKey(m => m.Id);
        unreadMessageConfiguration.Property(m => m.Id).HasColumnType("int").ValueGeneratedOnAdd();
        unreadMessageConfiguration.Property(m => m.ContactId).HasColumnName("ContactId").HasColumnType("int");
        unreadMessageConfiguration.Property(m => m.ReadTime).HasColumnName("ReadTime");
        //unreadMessageConfiguration.Property(m => m.Content).HasColumnName("Content");

        unreadMessageConfiguration.HasOne<Contact>()
            .WithMany()
            .HasForeignKey(m => m.ContactId)
            .HasPrincipalKey(c => c.Id);

        unreadMessageConfiguration.HasIndex(m => m.ContactId);
        unreadMessageConfiguration.HasIndex(m => m.ReadTime);
    }
}