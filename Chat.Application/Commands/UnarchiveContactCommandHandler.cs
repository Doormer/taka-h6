using Chat.Domain.AggregateModels.ContactArchivalAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Commands;

public class UnarchiveContactCommandHandler(
    IMediator mediator,
    IArchiveContactRepo archiveContactRepo,
    ILogger<UnarchiveContactCommandHandler> logger)
    : IRequestHandler<UnarchiveContactCommand, bool>
{
    private readonly IArchiveContactRepo _archiveContactRepo =
        archiveContactRepo ?? throw new ArgumentNullException(nameof(archiveContactRepo));

    private readonly ILogger<UnarchiveContactCommandHandler> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<bool> Handle(UnarchiveContactCommand message, CancellationToken cancellationToken)
    {
        var contact = await _archiveContactRepo.FindContactAsync(message.UserId, message.UserContactId);
        if (contact is null)
        {
            throw new Exception("Contact not found");
        }

        contact.UpdateArchivedStatus(false);

        _logger.LogInformation("ArchivingContact - contact: {@contact}", contact);
        _archiveContactRepo.UpdateArchiveStatus(contact);
        // or could be called as SaveAggregateAsync
        return await _archiveContactRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}