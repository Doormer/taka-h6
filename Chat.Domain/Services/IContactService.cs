using Chat.Domain.AggregatesModel.ContactAggregate;

namespace Chat.Domain.Services
{
    public interface IContactService
    {
        Task<Contact> GetContactById(Guid id);
        Task<List<Contact>> GetAllContacts();
        Task<Contact> CreateContact(Contact contact);
        Task<Contact> UpdateContact(Contact contact);
        Task DeleteContact(Guid id);
    }
}