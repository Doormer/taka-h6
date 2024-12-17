namespace Chat.Api.Models.Requests
{
    /// <summary>
    /// Request model for updating an existing contact
    /// Contains fields that can be modified during update
    /// </summary>
    public class UpdateContactRequest
    {
        /// <summary>
        /// Gets or sets the new username for the contact
        /// Must be provided when updating a contact
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Gets or sets the new avatar URL for the contact
        /// Must be provided when updating a contact
        /// </summary>
        public required string AvatarUrl { get; set; }
    }
}