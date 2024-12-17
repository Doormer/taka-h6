namespace Chat.Domain.AggregatesModel.ContactAggregate;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Defines the contract for contact repository operations
/// </summary>
public interface IContactRepository
{
    /// <summary>
    /// Gets a contact by its unique identifier
    /// </summary>
    /// <param name="id">The contact's unique identifier</param>
    Task<Contact> GetById(Guid id);

    /// <summary>
    /// Gets all contacts
    /// </summary>
    Task<List<Contact>> GetAll();

    /// <summary>
    /// Adds a new contact
    /// </summary>
    /// <param name="contact">The contact to add</param>
    Task<Contact> Add(Contact contact);

    /// <summary>
    /// Updates an existing contact
    /// </summary>
    /// <param name="contact">The contact to update</param>
    Task<Contact> Update(Contact contact);

    /// <summary>
    /// Deletes a contact by its unique identifier
    /// </summary>
    /// <param name="id">The contact's unique identifier</param>
    Task DeleteContact(Guid id);
}
