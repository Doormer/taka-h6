namespace Chat.Domain.AggregateModels.MessageAggregate;

/// <summary>
/// Represents the status of a message in the chat system.
/// Tracks the lifecycle states from sending to reading.
/// </summary>
public enum MessageStatus
{
    Sent = 1,
    Delivered = 2,
    Read = 3,
    Failed = 4
} 