using Chat.Domain.AggregateModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.EntityConfigurations.UserAggregate;

public class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> contactConfiguration)
    {
        contactConfiguration.ToTable("contacts");

        contactConfiguration.HasKey(c => c.Id);
        contactConfiguration.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        contactConfiguration.Property(c => c.UserId)
            .IsRequired();

        contactConfiguration.Property(c => c.ContactUserId)
            .IsRequired();

        contactConfiguration.Property(c => c.IsArchived)
            .HasDefaultValue(false);

        contactConfiguration.HasIndex(c => new { c.UserId, c.ContactUserId })
            .IsUnique();

        contactConfiguration.Ignore(c => c.DomainEvents);
    }
}