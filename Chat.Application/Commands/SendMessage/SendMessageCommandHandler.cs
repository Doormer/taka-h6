using Chat.Domain.AggregateModels.MessageAggregate;
using Chat.Domain.SeedWork;
using MediatR;

namespace Chat.Application.Commands.SendMessage;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Guid>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendMessageCommandHandler(
        IMessageRepository messageRepository,
        IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var message = Message.Create(
            request.SenderId,
            request.ReceiverId,
            request.Content,
            request.Type);

        _messageRepository.Add(message);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        return message.Id;
    }
} 