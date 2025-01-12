using Chat.ApiService.Application.Commands;
using Chat.Domain.AggregateModels.ReciprocalContactAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Commands;

public class AddContactCommandHandler(
    IMediator mediator,
    IReciprocalContactRepo reciprocalContactRepo,
    ILogger<AddContactCommandHandler> logger)
    : IRequestHandler<AddContactCommand, bool>
{
    private readonly ILogger<AddContactCommandHandler> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IReciprocalContactRepo _reciprocalContactRepo =
        reciprocalContactRepo ?? throw new ArgumentNullException(nameof(reciprocalContactRepo));

    public async Task<bool> Handle(AddContactCommand message, CancellationToken cancellationToken)
    {
        var reciprocalContact = new ReciprocalContact(message.UserId, message.UserContactId);

        //todo
        //check whether the contact is already added
        _logger.LogInformation("AddingContact - contact: {@contact}", reciprocalContact);
        _reciprocalContactRepo.Add(reciprocalContact);

        return await _reciprocalContactRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}