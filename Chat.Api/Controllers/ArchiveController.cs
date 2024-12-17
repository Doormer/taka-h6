using Microsoft.AspNetCore.Mvc;
using Chat.Domain.AggregatesModel.ArchiveAggregate;
using Chat.Api.Models;
using Chat.Api.Models.Responses;
using Chat.Api.Mappers;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// Controller for managing archived contacts
    /// Provides endpoints for archive-related operations
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ArchiveController : ControllerBase
    {
        private readonly IArchiveRepository _archiveRepository;

        /// <summary>
        /// Initializes a new instance of the ArchiveController
        /// </summary>
        /// <param name="archiveRepository">The archive repository instance</param>
        public ArchiveController(IArchiveRepository archiveRepository)
        {
            _archiveRepository = archiveRepository ?? throw new ArgumentNullException(nameof(archiveRepository));
        }

        /// <summary>
        /// Gets all unarchived contacts
        /// </summary>
        /// <returns>A list of unarchived contacts</returns>
        [HttpGet("unarchived")]
        public async Task<ActionResult<ApiResponse<List<ContactDto>>>> GetUnarchivedContacts()
        {
            var contacts = await _archiveRepository.GetUnarchivedContacts();
            return ApiResponse<List<ContactDto>>.Ok(contacts.ToDtos());
        }

        /// <summary>
        /// Gets all archived contacts
        /// </summary>
        /// <returns>A list of archived contacts</returns>
        [HttpGet("archived")]
        public async Task<ActionResult<ApiResponse<List<ContactDto>>>> GetArchivedContacts()
        {
            var contacts = await _archiveRepository.GetArchivedContacts();
            return ApiResponse<List<ContactDto>>.Ok(contacts.ToDtos());
        }

        /// <summary>
        /// Toggles the archive status of a contact
        /// </summary>
        /// <param name="id">The contact's unique identifier</param>
        /// <returns>Success indicator</returns>
        [HttpPost("{id}/toggle")]
        public async Task<ActionResult<ApiResponse<bool>>> ToggleArchiveStatus(Guid id)
        {
            await _archiveRepository.ToggleArchiveStatus(id);
            return ApiResponse<bool>.Ok(true, "Archive status toggled successfully");
        }
    }
}