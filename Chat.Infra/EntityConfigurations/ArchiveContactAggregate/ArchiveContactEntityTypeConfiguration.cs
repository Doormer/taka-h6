using Chat.Domain.AggregateModels.ContactArchivalAggregate;

namespace Chat.Infra.EntityConfigurations.ArchiveContactAggregate;

internal class ArchiveContactEntityTypeConfiguration : IEntityTypeConfiguration<ContactArchival>
{
    public void Configure(EntityTypeBuilder<ContactArchival> contactConfiguration)
    {
        contactConfiguration.ToTable("users");

        contactConfiguration.Ignore(c => c.DomainEvents);

        contactConfiguration.Property(c => c.UserId).HasColumnName("UserId");

        contactConfiguration.HasOne(ac => ac.Contact).WithOne().HasPrincipalKey<ContactArchival>(ac => ac.UserId)
                            .HasForeignKey<Contact>(c => c.UserId);
    }
}