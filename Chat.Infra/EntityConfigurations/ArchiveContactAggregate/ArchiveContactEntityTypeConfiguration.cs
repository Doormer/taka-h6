using Chat.Domain.AggregateModels.ContactArchivalAggregate;

namespace Chat.Infra.EntityConfigurations.ArchiveContactAggregate;

internal class ArchiveContactEntityTypeConfiguration : IEntityTypeConfiguration<ContactArchival>
{
    public void Configure(EntityTypeBuilder<ContactArchival> contactConfiguration)
    {
        contactConfiguration.ToTable("users");

        contactConfiguration.Ignore(c => c.DomainEvents);

        contactConfiguration.Property(c => c.UserId).HasColumnName("UserId");
        contactConfiguration.Property(c => c.Username).HasColumnType("varchar(50)").IsRequired();
        contactConfiguration.Property(c => c.AvatarUrl).HasColumnType("varchar(255)");
        contactConfiguration.Property(c => c.CreatedAt).HasColumnType("datetime").IsRequired();

        contactConfiguration.HasOne(ac => ac.Contact).WithOne()
            .HasPrincipalKey<ContactArchival>(ac => ac.UserId)
            .HasForeignKey<Contact>(c => c.UserId);
    }
}