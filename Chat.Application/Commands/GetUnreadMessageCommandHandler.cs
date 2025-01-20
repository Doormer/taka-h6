using Chat.ApiService.Application.Commands;
using Chat.Domain.AggregateModels.UnreadMessageAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Commands;

public class GetUnreadMessageCommand : IRequest<int>
{
    public Guid UserId { get; set; }
    public Guid UserContactId { get; set; }
}

public class GetUnreadMessageCommandHandler(
    IMediator mediator,
    IUnreadMessageRepo unreadMessageRepo,
    ILogger<GetUnreadMessageCommandHandler> logger)
    : IRequestHandler<GetUnreadMessageCommand, int>
{
    private readonly ILogger<GetUnreadMessageCommandHandler> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IUnreadMessageRepo _unreadMessageRepo =
        unreadMessageRepo ?? throw new ArgumentNullException(nameof(_unreadMessageRepo));

    public async Task<int> Handle(GetUnreadMessageCommand message, CancellationToken cancellationToken)
    {
        var unreadMessageCount = await _unreadMessageRepo.GetUnreadMessageCount(message.UserId, message.UserContactId);
        if (unreadMessageCount is null)
        {
            throw new Exception("UnreadMessageCount not found");
        }

        _logger.LogInformation(
            "Getting unread message count - UserId: {UserId}, ContactId: {ContactId}, Count: {Count}",
            message.UserId,
            message.UserContactId,
            unreadMessageCount.NumOfUnreadMessages);

        return unreadMessageCount.NumOfUnreadMessages;
    }
}