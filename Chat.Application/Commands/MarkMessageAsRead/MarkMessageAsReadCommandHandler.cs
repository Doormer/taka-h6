using Chat.Domain.AggregateModels.MessageAggregate;
using Chat.Domain.SeedWork;
using MediatR;
using Chat.Application.Common.Exceptions;

namespace Chat.Application.Commands.MarkMessageAsRead;

public class MarkMessageAsReadCommandHandler : IRequestHandler<MarkMessageAsReadCommand, bool>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkMessageAsReadCommandHandler(
        IMessageRepository messageRepository,
        IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(MarkMessageAsReadCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.FindByIdAsync(request.MessageId);
        if (message is null)
        {
            throw new ChatNotFoundException($"Message {request.MessageId} not found");
        }

        message.MarkAsRead();
        _messageRepository.Update(message);

        return await _messageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
} 