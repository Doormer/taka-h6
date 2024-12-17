using Chat.Domain.AggregatesModel.ContactAggregate;
using Chat.Domain.Services;

namespace Chat.Infra.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Contact> GetContactById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            return await _repository.GetAll();
        }

        public async Task<Contact> CreateContact(Contact contact)
        {
            return await _repository.Add(contact);
        }

        public async Task<Contact> UpdateContact(Contact contact)
        {
            return await _repository.Update(contact);
        }

        public async Task DeleteContact(Guid id)
        {
            await _repository.DeleteContact(id);
        }
    }
} 