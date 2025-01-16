using Chat.Domain.AggregateModels.MessageAggregate;
using Chat.Domain.SeedWork;
using MediatR;
using Chat.Application.Common.Exceptions;

namespace Chat.Application.Commands.MarkMessageAsDelivered;

public class MarkMessageAsDeliveredCommandHandler : IRequestHandler<MarkMessageAsDeliveredCommand, bool>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkMessageAsDeliveredCommandHandler(
        IMessageRepository messageRepository,
        IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(MarkMessageAsDeliveredCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.FindByIdAsync(request.MessageId);
        if (message is null)
        {
            throw new ChatNotFoundException($"Message {request.MessageId} not found");
        }

        message.MarkAsDelivered();
        _messageRepository.Update(message);

        return await _messageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
} 