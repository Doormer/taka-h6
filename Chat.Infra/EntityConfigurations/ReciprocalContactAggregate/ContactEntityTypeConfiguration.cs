
using Chat.Domain.AggregateModels.ReciprocalContactAggregate;

namespace Chat.Infra.EntityConfigurations.ReciprocalContactAggregate;

internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> contactConfiguration)
    {
        contactConfiguration.ToTable("contacts");

        contactConfiguration.Ignore(c => c.DomainEvents);

        contactConfiguration.Property(c => c.UserId);
        contactConfiguration.Property(c => c.ContactUserId);
    }
}