using Chat.Domain.AggregateModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.EntityConfigurations.UserAggregate;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> userConfiguration)
    {
        userConfiguration.ToTable("users");

        userConfiguration.HasKey(u => u.Id);
        userConfiguration.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        userConfiguration.Property(u => u.UserId)
            .IsRequired()
            .HasColumnType("char(36)");

        userConfiguration.HasAlternateKey(u => u.UserId);

        userConfiguration.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        userConfiguration.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        userConfiguration.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);

        userConfiguration.Property(u => u.UserType)
            .IsRequired()
            .HasConversion<string>();

        userConfiguration.Property(u => u.ProfilePicture)
            .HasMaxLength(255);

        userConfiguration.Property(u => u.CreatedAt)
            .IsRequired();

        userConfiguration.Property(u => u.LastActive)
            .IsRequired();

        userConfiguration.HasIndex(u => u.Username)
            .IsUnique();

        userConfiguration.HasIndex(u => u.Email)
            .IsUnique();

        userConfiguration.Ignore(u => u.DomainEvents);
    }
}