using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.MessageAggregate;

/// <summary>
/// Represents the content of a message, including its value and type.
/// Ensures content validation and type safety for different kinds of messages.
/// </summary>
public class MessageContent
{
    public Guid Id { get; private set; }
    public string Value { get; private set; }
    public MessageType Type { get; private set; }

    public MessageContent(string value, MessageType type)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException(nameof(value));
            
        if (value.Length > 5000)
            throw new ArgumentException("Message content too long", nameof(value));

        Id = Guid.NewGuid();
        Value = value;
        Type = type;
    }
} 