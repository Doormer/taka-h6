/// <summary>
/// Data Transfer Object for Contact entity
/// Used for API requests and responses
/// </summary>
namespace Chat.Api.Models
{
    public class ContactDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the contact
        /// </summary>
        public Guid Id { get; set; }

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
        /// Gets or sets the creation time of the contact
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets whether the contact is archived
        /// </summary>
        public bool IsArchived { get; set; }
    }
} 