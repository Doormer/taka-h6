using Chat.Domain.AggregateModels.MessageAggregate;
using MediatR;

namespace Chat.Application.Queries.GetMessages;

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
            request.User1Id,
            request.User2Id,
            null,
            request.Take);

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