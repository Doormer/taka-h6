using Chat.Domain.QueryEntities;

namespace Chat.Infra.EntityConfigurations.QueryEntities;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> contactConfiguration)
    {
        contactConfiguration.ToTable("users");

        contactConfiguration.Ignore(c => c.DomainEvents);
        
        contactConfiguration.Property(c => c.UserName).HasColumnName("UserName");
        contactConfiguration.Property(c => c.UserId).HasColumnName("UserId");
        contactConfiguration.Property(c => c.AvatarUrl).HasColumnName("AvatarUrl");
        
        // need to for entity to shared table
        contactConfiguration.HasOne<Domain.AggregateModels.ContactArchivalAggregate.ContactArchival>().WithOne()
                            .HasForeignKey<User>(e => e.Id);
    }
}