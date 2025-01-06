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
    private readonly IReciprocalContactRepo _reciprocalContactRepo = reciprocalContactRepo ?? throw new ArgumentNullException(nameof(reciprocalContactRepo));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ILogger<AddContactCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<bool> Handle(AddContactCommand message, CancellationToken cancellationToken)
    {
        // Check if contact already exists
        var existingContact = await _reciprocalContactRepo.FindByUserIdAndContactIdAsync(
            message.UserId, 
            message.UserContactId);

        if (existingContact is not null)
        {
            _logger.LogInformation(
                "Contact already exists between users {UserId} and {ContactId}", 
                message.UserId, 
                message.UserContactId);
            return true; // Return true as this case should not be treated as an error
        }

        var reciprocalContact = new ReciprocalContact(message.UserId, message.UserContactId);
        _logger.LogInformation("AddingContact - contact: {@contact}", reciprocalContact);
        _reciprocalContactRepo.Add(reciprocalContact);

        return await _reciprocalContactRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
