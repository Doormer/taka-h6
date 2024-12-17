using Microsoft.AspNetCore.Mvc;
using Chat.Domain.AggregatesModel.ContactAggregate;
using Chat.Domain.Services;
using Chat.Api.Models;
using Chat.Api.Models.Requests;
using Chat.Api.Models.Responses;
using Chat.Api.Mappers;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// Controller for managing contacts
    /// Provides endpoints for CRUD operations on contacts
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        /// <summary>
        /// Initializes a new instance of the ContactsController
        /// </summary>
        /// <param name="contactService">The contact service instance</param>
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }

        /// <summary>
        /// Gets all contacts
        /// </summary>
        /// <returns>A list of all contacts</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ContactDto>>>> GetAll()
        {
            var contacts = await _contactService.GetAllContacts();
            return ApiResponse<List<ContactDto>>.Ok(contacts.ToDtos());
        }

        /// <summary>
        /// Gets a specific contact by ID
        /// </summary>
        /// <param name="id">The contact's unique identifier</param>
        /// <returns>The requested contact</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ContactDto>>> GetById(Guid id)
        {
            var contact = await _contactService.GetContactById(id);
            return ApiResponse<ContactDto>.Ok(contact.ToDto());
        }

        /// <summary>
        /// Creates a new contact
        /// </summary>
        /// <param name="request">The contact creation request</param>
        /// <returns>The newly created contact</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ContactDto>>> Create(CreateContactRequest request)
        {
            var contact = Contact.Create(request.UserName, request.AvatarUrl, request.LastMessage);
            var created = await _contactService.CreateContact(contact);
            return ApiResponse<ContactDto>.Ok(created.ToDto(), "Contact created successfully");
        }

        /// <summary>
        /// Updates an existing contact
        /// </summary>
        /// <param name="id">The contact's unique identifier</param>
        /// <param name="request">The update request</param>
        /// <returns>The updated contact</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ContactDto>>> Update(Guid id, UpdateContactRequest request)
        {
            var contact = await _contactService.GetContactById(id);
            contact.UpdateInfo(request.UserName, request.AvatarUrl);
            var updated = await _contactService.UpdateContact(contact);
            return ApiResponse<ContactDto>.Ok(updated.ToDto());
        }

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="id">The contact's unique identifier</param>
        /// <returns>Success indicator</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
        {
            await _contactService.DeleteContact(id);
            return ApiResponse<bool>.Ok(true, "Contact deleted successfully");
        }
    }
}