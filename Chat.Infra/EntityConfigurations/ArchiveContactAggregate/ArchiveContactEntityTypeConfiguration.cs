using Chat.Domain.AggregateModels.ContactArchivalAggregate;

namespace Chat.Infra.EntityConfigurations.ArchiveContactAggregate;

internal class ArchiveContactEntityTypeConfiguration : IEntityTypeConfiguration<ContactArchival>
{
    public void Configure(EntityTypeBuilder<ContactArchival> contactConfiguration)
    {
        contactConfiguration.ToTable("archive_contacts");

        contactConfiguration.HasKey(c => c.Id);
        contactConfiguration.Property(c => c.Id).ValueGeneratedOnAdd();
        
        contactConfiguration.Ignore(c => c.DomainEvents);

        contactConfiguration.Property(c => c.UserId)
            .HasColumnName("UserId")
            .IsRequired();

        contactConfiguration.HasOne(ac => ac.Contact)
            .WithOne()
            .HasForeignKey<Contact>(c => c.UserId)
            .IsRequired();
    }
}