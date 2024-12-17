using Chat.Api.Models;
using Chat.Domain.AggregatesModel.ContactAggregate;

namespace Chat.Api.Mappers
{
    /// <summary>
    /// Mapper class for converting between Contact domain entities and DTOs
    /// Provides extension methods for easy mapping
    /// </summary>
    public static class ContactMapper
    {
        /// <summary>
        /// Converts a Contact domain entity to a ContactDto
        /// </summary>
        /// <param name="contact">The contact entity to convert</param>
        /// <returns>A new ContactDto containing the contact's data</returns>
        public static ContactDto ToDto(this Contact contact)
        {
            return new ContactDto
            {
                Id = contact.Id,
                UserName = contact.UserName,
                AvatarUrl = contact.AvatarUrl,
                LastMessage = contact.LastMessage,
                CreatedTime = contact.CreatedTime,
                IsArchived = contact.IsArchived
            };
        }

        /// <summary>
        /// Converts a collection of Contact entities to a list of ContactDtos
        /// </summary>
        /// <param name="contacts">The collection of contacts to convert</param>
        /// <returns>A list of ContactDtos</returns>
        public static List<ContactDto> ToDtos(this IEnumerable<Contact> contacts)
        {
            return contacts.Select(c => c.ToDto()).ToList();
        }
    }
} 