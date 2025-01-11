using Chat.Domain.AggregateModels.ArchiveContactAggregate;

namespace Chat.Infra.EntityConfigurations.ArchiveContactAggregate;

internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> contactConfiguration)
    {
        contactConfiguration.ToTable("contacts");

        contactConfiguration.Ignore(c => c.DomainEvents);
        

        // add db constraint to not allow duplicate entries
        // add FK 
    }
}