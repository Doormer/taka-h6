using Chat.Domain.AggregateModels.ContactArchivalAggregate;

namespace Chat.Infra.EntityConfigurations.ArchiveContactAggregate;

internal class ArchiveContactEntityTypeConfiguration : IEntityTypeConfiguration<ContactArchival>
{
    public void Configure(EntityTypeBuilder<ContactArchival> contactConfiguration)
    {
        contactConfiguration.ToTable("users");

        contactConfiguration.Ignore(c => c.DomainEvents);

        contactConfiguration.Property(c => c.UserId).HasColumnName("UserId");
        contactConfiguration.Property(c => c.UserName).HasColumnName("UserName").HasColumnType("varchar(50)").IsRequired();
        contactConfiguration.Property(c => c.AvatarUrl).HasColumnName("AvatarUrl").HasColumnType("varchar(255)");
        contactConfiguration.Property(c => c.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime").IsRequired()
                            .ValueGeneratedOnAddOrUpdate();

        // contactConfiguration.HasOne(ac => ac.Contact).WithOne()
        //     .HasPrincipalKey<ContactArchival>(ac => ac.UserId)
        //     .HasForeignKey<Contact>(c => c.UserId);
        //
        contactConfiguration.HasMany(ac => ac.Contacts).WithOne()
                            .HasPrincipalKey(ac => ac.UserId)
                            .HasForeignKey(c => c.UserId);
    }
}