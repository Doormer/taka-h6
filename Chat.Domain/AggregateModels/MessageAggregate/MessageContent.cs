using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.MessageAggregate;

public class MessageContent
{
    public required string Value { get; init; }
    public required MessageType Type { get; init; }

    private MessageContent() { } // For EF Core

    public static MessageContent Create(string content, MessageType type)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentNullException(nameof(content));
            
        if (content.Length > 5000)
            throw new ArgumentException("Message content too long", nameof(content));

        return new MessageContent { Value = content, Type = type };
    }
} 