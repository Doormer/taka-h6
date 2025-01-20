using Chat.Domain.AggregateModels.ContactArchivalAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Commands;

public class ArchiveContactCommandHandler(
    IMediator mediator,
    IArchiveContactRepo archiveContactRepo,
    ILogger<ArchiveContactCommandHandler> logger)
    : IRequestHandler<ArchiveContactCommand, Unit>
{
    private readonly IArchiveContactRepo _archiveContactRepo =
        archiveContactRepo ?? throw new ArgumentNullException(nameof(archiveContactRepo));

    private readonly ILogger<ArchiveContactCommandHandler> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<Unit> Handle(ArchiveContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Finding contact for userId: {UserId}, contactUserId: {ContactUserId}",
                request.UserId, request.ContactUserId);

            var contact = await _archiveContactRepo.FindContactAsync(request.UserId, request.ContactUserId);
            if (contact is null)
            {
                _logger.LogWarning("Contact not found for userId: {UserId}, contactUserId: {ContactUserId}",
                    request.UserId, request.ContactUserId);
                throw new Exception("Contact not found");
            }

            contact.UpdateArchivedStatus(true);
            _logger.LogInformation("Updating archive status for contact: {@Contact}", contact);

            _archiveContactRepo.UpdateArchiveStatus(contact);
            await _archiveContactRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _logger.LogInformation("Archive status updated successfully for contact: {ContactId}", contact.Id);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error archiving contact for userId: {UserId}, contactUserId: {ContactUserId}",
                request.UserId, request.ContactUserId);
            throw;
        }
    }
}