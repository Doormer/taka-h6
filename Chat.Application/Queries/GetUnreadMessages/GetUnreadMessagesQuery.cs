using Chat.Application.Queries.GetMessages;
using MediatR;

namespace Chat.Application.Queries.GetUnreadMessages;

public record GetUnreadMessagesQuery : IRequest<List<MessageDto>>
{
    public Guid UserId { get; init; }
} 