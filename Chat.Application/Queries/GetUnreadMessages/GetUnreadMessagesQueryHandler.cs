using Chat.Domain.AggregateModels.MessageAggregate;
using Chat.Application.Queries.GetMessages;
using MediatR;

namespace Chat.Application.Queries.GetUnreadMessages;

public class GetUnreadMessagesQueryHandler : IRequestHandler<GetUnreadMessagesQuery, List<MessageDto>>
{
    private readonly IMessageRepository _messageRepository;

    public GetUnreadMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<List<MessageDto>> Handle(GetUnreadMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await _messageRepository.GetUnreadMessagesAsync(request.UserId);

        return messages.Select(m => new MessageDto
        {
            Id = m.Id,
            SenderId = m.SenderId,
            ReceiverId = m.ReceiverId,
            Content = m.Content.Value,
            MessageType = m.Content.Type.ToString(),
            Status = m.Status.ToString(),
            CreatedTime = m.CreatedTime,
            ReadTime = m.ReadTime
        }).ToList();
    }
} 