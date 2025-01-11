using Chat.Domain.AggregateModels.ArchiveContactAggregate;

namespace Chat.Infra.EntityConfigurations.ArchiveContactAggregate;

internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> contactConfiguration)
    {
        contactConfiguration.ToTable("contacts");

        contactConfiguration.Ignore(c => c.DomainEvents);
        
        contactConfiguration.Property(c => c.ContactUserId);
        contactConfiguration.Property(c => c.IsArchived);
        
        contactConfiguration.HasOne<Contact>().WithOne().HasForeignKey<Chat.Domain.AggregateModels.ReciprocalContactAggregate.Contact>(e => e.Id);
        //todo 
        // add db constraint to not allow duplicate entries
        // add FK 
    }
}