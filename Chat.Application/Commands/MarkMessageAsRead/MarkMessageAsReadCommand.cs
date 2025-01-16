using MediatR;

namespace Chat.Application.Commands.MarkMessageAsRead;

public record MarkMessageAsReadCommand : IRequest<bool>
{
    public Guid MessageId { get; init; }
    public Guid ReaderId { get; init; }
} 