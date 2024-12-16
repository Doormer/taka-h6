namespace Chat.Domain.AggregatesModel.ArchiveAggregate;

using System;
using System.Collections.Generic;

public interface IArchivedContact
{
    /// <summary>
    /// Archives a single contact.
    /// </summary>
    /// <param name="contact">The contact to be archived.</param>
    /// <returns>The unique identifier of the archived contact.</returns>
    Guid ArchiveContact(Contact contact);

    /// <summary>
    /// Archives a list of contacts.
    /// </summary>
    /// <param name="contacts">The list of contacts to be archived.</param>
    /// <returns>A list of unique identifiers for the archived contacts.</returns>
    List<Guid> ArchiveContacts(List<Contact> contacts);

    /// <summary>
    /// Unarchives a single contact.
    /// </summary>
    /// <param name="contact">The contact to be unarchived.</param>
    /// <returns>The unique identifier of the unarchived contact.</returns>
    Guid UnArchiveContact(Contact contact);

    /// <summary>
    /// Unarchives a list of contacts.
    /// </summary>
    /// <param name="contacts">The list of contacts to be unarchived.</param>
    /// <returns>A list of unique identifiers for the unarchived contacts.</returns>
    List<Guid> UnArchiveContacts(List<Contact> contacts);

    /// <summary>
    /// Retrieves an archived contact by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the archived contact.</param>
    /// <returns>The archived contact corresponding to the given identifier.</returns>
    Contact GetArchivedContacts(Guid userId);
}
