using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.MessageAggregate;

/// <summary>
/// Represents the content of a message, including its value and type.
/// Ensures content validation and type safety for different kinds of messages.
/// </summary>
public class MessageContent
{
    public Guid Id { get; private set; }
    public object Content { get; private set; }
    public MessageType Type { get; private set; }
    public string? ContentType { get; private set; } 

    public MessageContent(object content, MessageType type, string? contentType = null)
    {
        if (content == null)
            throw new ArgumentNullException(nameof(content));
            
        if (type == MessageType.Text && content is string textContent)
        {
            if (textContent.Length > 5000)
                throw new ArgumentException("Text content too long", nameof(content));
        }

        Id = Guid.NewGuid();
        Content = content;
        Type = type;
        ContentType = contentType;
    }
} 