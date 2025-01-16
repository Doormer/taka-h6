using Chat.Domain.AggregateModels.MessageAggregate;
using MediatR;

namespace Chat.Application.Commands.SendMessage;

public record SendMessageCommand : IRequest<Guid>
{
    public Guid SenderId { get; init; }
    public Guid ReceiverId { get; init; }
    public required string Content { get; init; }
    public MessageType Type { get; init; } = MessageType.Text;
} 