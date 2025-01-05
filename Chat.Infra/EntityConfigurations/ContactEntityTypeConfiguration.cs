using Chat.Domain.AggregateModels.ReciprocalContactAggregate;

namespace Chat.Infra.EntityConfigurations;

internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> contactConfiguration)
    {
        contactConfiguration.ToTable("contacts");

        contactConfiguration.Ignore(b => b.DomainEvents);

        contactConfiguration.Property(o => o.UserId);
        contactConfiguration.Property(o => o.ContactUserId);
        //todo 
        // add db constraint to not allow duplicate entries
        // add FK 
    }
}