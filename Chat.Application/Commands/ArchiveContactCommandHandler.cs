using Chat.ApiService.Application.Commands;
using Chat.Domain.AggregateModels.ContactArchivalAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Commands;

/// <summary>
/// Handles the archival operation for contacts.
/// Processes requests to archive or unarchive contacts and manages the persistence of these changes.
/// </summary>
public class ArchiveContactCommandHandler : IRequestHandler<ArchiveContactCommand, bool>
{
    private readonly IArchiveContactRepo _archiveContactRepo;
    private readonly ILogger<ArchiveContactCommandHandler> _logger;

    public ArchiveContactCommandHandler(
        IArchiveContactRepo archiveContactRepo,
        ILogger<ArchiveContactCommandHandler> logger)
    {
        _archiveContactRepo = archiveContactRepo;
        _logger = logger;
    }

    public async Task<bool> Handle(ArchiveContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var contactArchival = await _archiveContactRepo.FindContactAsync(request.UserId, request.UserContactId);
            
            if (contactArchival == null)
            {
                contactArchival = new ContactArchival(request.UserId);
                contactArchival.ArchiveContact(request.UserContactId);
            }
            else
            {
                // 切换归档状态
                if (contactArchival.Contact?.IsArchived == true)
                {
                    contactArchival.UnarchiveContact(request.UserContactId);
                }
                else
                {
                    contactArchival.ArchiveContact(request.UserContactId);
                }
            }

            _archiveContactRepo.UpdateArchiveStatus(contactArchival);
            await _archiveContactRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error archiving contact {ContactId} for user {UserId}", 
                request.UserContactId, request.UserId);
            return false;
        }
    }
}