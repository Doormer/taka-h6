using Chat.Application.Common.Interfaces;
using MediatR;

namespace Chat.Application.Queries.GetMessages;

public record GetMessagesQuery : IRequest<List<MessageDto>>
{
    public Guid UserId { get; init; }
    public Guid ContactId { get; init; }
    public DateTime? Cursor { get; init; }
    public int Limit { get; init; } = 20;
}

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, List<MessageDto>>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<List<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await _messageRepository.GetMessagesBetweenUsersAsync(
            request.UserId,
            request.ContactId,
            request.Cursor,
            request.Limit);

        return messages.Select(m => new MessageDto
        {
            Id = m.Id,
            SenderId = m.SenderId,
            ReceiverId = m.ReceiverId,
            Content = m.Content.Value,
            Type = m.Content.Type,
            FileUrl = m.Content.FileUrl,
            FileName = m.Content.FileName,
            Status = m.Status,
            CreatedTime = m.CreatedTime,
            ReadTime = m.ReadTime
        }).ToList();
    }
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

public record UpdateMessageStatusCommand : IRequest<bool>
{
    public Guid MessageId { get; init; }
    public Guid UserId { get; init; }  // 执行操作的用户ID
    public MessageStatus NewStatus { get; init; }
}

public class UpdateMessageStatusCommandHandler : IRequestHandler<UpdateMessageStatusCommand, bool>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMessageStatusCommandHandler(
        IMessageRepository messageRepository,
        IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateMessageStatusCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.FindByIdAsync(request.MessageId);
        if (message == null)
            throw new ChatNotFoundException($"Message {request.MessageId} not found");

        // 验证用户是否有权限更新消息状态
        if (message.ReceiverId != request.UserId)
            throw new UnauthorizedMessageAccessException("User not authorized to update this message status");

        switch (request.NewStatus)
        {
            case MessageStatus.Delivered:
                message.MarkAsDelivered();
                break;
            case MessageStatus.Read:
                message.MarkAsRead();
                break;
            default:
                throw new MessageOperationException($"Cannot update message to status {request.NewStatus}");
        }

        _messageRepository.Update(message);
        return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
} 