using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.UserAggregate;

public enum UserType
{
    JobSeeker,
    Recruiter
}

public class User : Entity, IAggregateRoot
{
    public int Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserType UserType { get; private set; }
    public string? ProfilePicture { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastActive { get; private set; }

    protected User() { }

    public User(string username, string email, string passwordHash, UserType userType, string? profilePicture = null)
    {
        UserId = Guid.NewGuid();
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        UserType = userType;
        ProfilePicture = profilePicture;
        CreatedAt = DateTime.UtcNow;
        LastActive = DateTime.UtcNow;
    }

    public void UpdateLastActive()
    {
        LastActive = DateTime.UtcNow;
    }

    public void UpdateProfilePicture(string? profilePicture)
    {
        ProfilePicture = profilePicture;
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
}