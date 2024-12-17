namespace Chat.Api.Models.Requests
{
    /// <summary>
    /// Request model for creating a new contact
    /// Contains all required fields for contact creation
    /// </summary>
    public class CreateContactRequest
    {
        /// <summary>
        /// Gets or sets the username of the new contact
        /// Must be provided when creating a contact
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Gets or sets the URL of the contact's avatar
        /// Must be provided when creating a contact
        /// </summary>
        public required string AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the initial message for the contact
        /// Must be provided when creating a contact
        /// </summary>
        public required string LastMessage { get; set; }
    }
} 