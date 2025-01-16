using MediatR;

namespace Chat.Application.Commands.MarkMessageAsDelivered;

public record MarkMessageAsDeliveredCommand : IRequest<bool>
{
    public Guid MessageId { get; init; }
    public Guid ReceiverId { get; init; }
} 