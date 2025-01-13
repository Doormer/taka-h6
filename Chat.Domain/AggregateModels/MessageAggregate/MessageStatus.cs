namespace Chat.Domain.AggregateModels.MessageAggregate;

public enum MessageStatus
{
    Sent = 1,
    Delivered = 2,
    Read = 3,
    Failed = 4
} 