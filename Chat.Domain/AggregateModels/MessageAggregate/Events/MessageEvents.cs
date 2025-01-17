using MediatR;

namespace Chat.Domain.AggregateModels.MessageAggregate.Events;

public record MessageSentDomainEvent(Guid MessageId, Guid SenderId, Guid ReceiverId) : INotification;

public record MessageDeliveredDomainEvent(Guid MessageId, Guid ReceiverId) : INotification;

public record MessageReadDomainEvent(Guid MessageId, Guid ReceiverId) : INotification;

public record MessageFailedDomainEvent(Guid MessageId, string Reason) : INotification;