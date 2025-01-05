using Chat.Domain.AggregateModels.ReciprocalContactAggregate;

namespace Chat.Infra.EntityConfigurations;

internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> orderConfiguration)
    {
        orderConfiguration.ToTable("contacts");

        orderConfiguration.Ignore(b => b.DomainEvents);

        orderConfiguration.Property(o => o.UserId);
        orderConfiguration.Property(o => o.ContactUserId);
        //todo 
        // add db constraint to not allow duplicate entries
        // add FK 
    }
}