using Chat.Domain.AggregateModels.ArchiveContactAggregate;

namespace Chat.Infra.EntityConfigurations.ArchiveContactAggregate;

internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> contactConfiguration)
    {
        contactConfiguration.ToTable("contacts");


        // add FK 
    }
}