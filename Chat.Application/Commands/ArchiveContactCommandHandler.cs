using Chat.ApiService.Application.Commands;
using Chat.Domain.AggregateModels.ArchiveContactAggregate;
using Chat.Domain.AggregateModels.ReciprocalContactAggregate;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Commands;

public class ArchiveContactCommandHandler(
    IMediator mediator,
    IArchiveContactRepo archiveContactRepo,
    ILogger<ArchiveContactCommandHandler> logger)
    : IRequestHandler<ArchiveContactCommand, bool>
{
    
    private readonly IArchiveContactRepo _archiveContactRepo = archiveContactRepo ?? throw new ArgumentNullException(nameof(archiveContactRepo));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ILogger<ArchiveContactCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<bool> Handle(ArchiveContactCommand message, CancellationToken cancellationToken)
    {
        var reciprocalContact = new ArchiveContact(message.UserId);

        var contact = await _archiveContactRepo.FindContactAsync(message.UserId, message.UserContactId);
        if (contact is null)
        {
          throw new Exception("Contact not found");
        }
        else
        {
            contact.UpdateArchivedStatus(true);
        }
        
        _logger.LogInformation("ArchivingContact - contact: {@contact}", contact);
        _archiveContactRepo.UpdateArchiveStatus(contact);

        return await _archiveContactRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}