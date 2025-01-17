public enum UserStatus
{
    Offline = 0,
    Online = 1,
    Away = 2,
    Busy = 3
}

public class UserPresence
{
    public Guid UserId { get; private set; }
    public UserStatus Status { get; private set; }
    public DateTime LastSeen { get; private set; }
    
    public void UpdateStatus(UserStatus newStatus)
    {
        Status = newStatus;
        LastSeen = DateTime.UtcNow;
    }
} 