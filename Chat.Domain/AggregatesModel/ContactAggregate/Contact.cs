using System;

namespace Chat.Domain.AggregatesModel.ContactAggregate;

/// <summary>
/// Represents a contact in the chat system
/// This is the root entity of the Contact aggregate
/// </summary>
public class Contact
{
    /// <summary>
    /// Gets the unique identifier for the contact
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the username of the contact
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// Gets or sets the URL of the contact's avatar
    /// </summary>
    public required string AvatarUrl { get; set; }

    /// <summary>
    /// Gets or sets the last message exchanged with the contact
    /// </summary>
    public required string LastMessage { get; set; }

    /// <summary>
    /// Gets the creation time of the contact
    /// </summary>
    public DateTime? CreatedTime { get; private set; }

    /// <summary>
    /// Gets whether the contact is archived
    /// </summary>
    public bool IsArchived { get; private set; }

    /// <summary>
    /// Private constructor for EF Core
    /// </summary>
    private Contact() { }

    /// <summary>
    /// Creates a new contact with the specified details
    /// </summary>
    /// <param name="userName">The username of the contact</param>
    /// <param name="avatarUrl">The URL of the contact's avatar</param>
    /// <param name="lastMessage">The initial message</param>
    /// <returns>A new contact instance</returns>
    public static Contact Create(string userName, string avatarUrl, string lastMessage)
    {
        return new Contact
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            AvatarUrl = avatarUrl,
            LastMessage = lastMessage,
            CreatedTime = DateTime.UtcNow,
            IsArchived = false
        };
    }

    /// <summary>
    /// Archives the contact
    /// </summary>
    public void Archive() => IsArchived = true;

    /// <summary>
    /// Unarchives the contact
    /// </summary>
    public void Unarchive() => IsArchived = false;

    /// <summary>
    /// Updates the contact's basic information
    /// </summary>
    /// <param name="userName">New username</param>
    /// <param name="avatarUrl">New avatar URL</param>
    public void UpdateInfo(string userName, string avatarUrl)
    {
        UserName = userName;
        AvatarUrl = avatarUrl;
    }

    /// <summary>
    /// Updates the last message for the contact
    /// </summary>
    /// <param name="message">The new message</param>
    public void UpdateLastMessage(string message)
    {
        LastMessage = message;
    }
}