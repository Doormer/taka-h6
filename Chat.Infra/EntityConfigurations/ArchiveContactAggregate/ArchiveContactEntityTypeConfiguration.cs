using Chat.Domain.AggregateModels.ArchiveContactAggregate;

namespace Chat.Infra.EntityConfigurations.ArchiveContactAggregate;

internal class ArchiveContactEntityTypeConfiguration : IEntityTypeConfiguration<ArchiveContact>
{
    public void Configure(EntityTypeBuilder<ArchiveContact> contactConfiguration)
    {
        contactConfiguration.ToTable("users");

        contactConfiguration.Ignore(c => c.DomainEvents);
        
        contactConfiguration.Property(c => c.UserId).HasColumnName("UserId");

        contactConfiguration.HasOne(ac => ac.Contact).WithOne().HasPrincipalKey<ArchiveContact>(ac => ac.UserId).HasForeignKey<Contact>(c => c.UserId);
    }
}