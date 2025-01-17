using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Chat.Application.DTOs;
using Chat.Domain.AggregateModels.ContactArchivalAggregate;

namespace Chat.Application.Queries
{
    /// <summary>
    /// Query to retrieve archived contacts for a specific user.
    /// </summary>
    public record GetArchivedContactsQuery(Guid UserId) : IRequest<List<ContactDto>>;

    /// <summary>
    /// Handles the retrieval of archived contacts.
    /// Returns a list of contacts that have been archived by the specified user.
    /// </summary>
    public class GetArchivedContactsQueryHandler : IRequestHandler<GetArchivedContactsQuery, List<ContactDto>>
    {
        private readonly IArchiveContactRepo _archiveContactRepo;
        private readonly ILogger<GetArchivedContactsQueryHandler> _logger;

        public GetArchivedContactsQueryHandler(
            IArchiveContactRepo archiveContactRepo,
            ILogger<GetArchivedContactsQueryHandler> logger)
        {
            _archiveContactRepo = archiveContactRepo;
            _logger = logger;
        }

        public async Task<List<ContactDto>> Handle(GetArchivedContactsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var archivedContacts = await _archiveContactRepo.GetArchivedContactsAsync(request.UserId);
                return archivedContacts.Select(c => new ContactDto(c.UserId, c.Contact.ContactUserId, c.Contact.IsArchived)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting archived contacts for user {UserId}", request.UserId);
                throw;
            }
        }
    }
} 