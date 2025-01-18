using Chat.Domain.QueryEntities;

namespace Chat.Infra.EntityConfigurations.QueryEntities;

internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> contactConfiguration)
    {
        contactConfiguration.ToTable("contacts");

        contactConfiguration.Ignore(c => c.DomainEvents);
        //TODO we havent implemented this
        contactConfiguration.Ignore(c => c.lastMessage);

        contactConfiguration.Property(c => c.UserId).HasColumnName("UserId");
        contactConfiguration.Property(c => c.ContactUserId).HasColumnName("ContactUserId");
        contactConfiguration.Property(c => c.IsArchived).HasColumnName("IsArchived");
        contactConfiguration.Property(c => c.AvatarUrl).HasColumnName("AvatarUrl");

        // need to for entity to shared table
        contactConfiguration.HasOne<Domain.AggregateModels.ReciprocalContactAggregate.Contact>().WithOne()
            .HasForeignKey<Contact>(e => e.Id);
    }
}