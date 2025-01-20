using Chat.Domain.AggregateModels.ContactArchivalAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Commands;

public class UnarchiveContactCommandHandler(
    IMediator mediator,
    IArchiveContactRepo archiveContactRepo,
    ILogger<UnarchiveContactCommandHandler> logger)
    : IRequestHandler<UnarchiveContactCommand, Unit>
{
    private readonly IArchiveContactRepo _archiveContactRepo =
        archiveContactRepo ?? throw new ArgumentNullException(nameof(archiveContactRepo));

    private readonly ILogger<UnarchiveContactCommandHandler> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<Unit> Handle(UnarchiveContactCommand request, CancellationToken cancellationToken)
    {
        var contactUserId = request.ContactUserId;
        var contact = await _archiveContactRepo.FindContactAsync(request.UserId, request.ContactUserId);
        if (contact is null)
        {
            throw new Exception("Contact not found");
        }

        contact.UpdateArchivedStatus(false);

        _logger.LogInformation("ArchivingContact - contact: {@contact}", contact);
        _archiveContactRepo.UpdateArchiveStatus(contact);
        await _archiveContactRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return Unit.Value;
    }
}