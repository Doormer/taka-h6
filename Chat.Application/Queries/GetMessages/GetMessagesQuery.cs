using Chat.Application.Common.Interfaces;
using MediatR;

namespace Chat.Application.Queries.GetMessages;

public record GetMessagesQuery : IRequest<List<MessageDto>>, ICacheableQuery
{
    public Guid User1Id { get; init; }
    public Guid User2Id { get; init; }
    public int Skip { get; init; } = 0;
    public int Take { get; init; } = 20;
}

public class MessageDto
{
    public Guid Id { get; init; }
    public Guid SenderId { get; init; }
    public Guid ReceiverId { get; init; }
    public required string Content { get; init; }
    public required string MessageType { get; init; }
    public required string Status { get; init; }
    public DateTime CreatedTime { get; init; }
    public DateTime? ReadTime { get; init; }
} 