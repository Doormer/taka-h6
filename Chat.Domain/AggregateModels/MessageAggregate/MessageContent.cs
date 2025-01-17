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
    public string? FileUrl { get; private set; }
    public string? FileName { get; private set; }
    public string? FileContentType { get; private set; }

    private MessageContent() { }

    public static MessageContent CreateTextContent(string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException(nameof(text));
            
        if (text.Length > 5000)
            throw new ArgumentException("Message content too long", nameof(text));

        return new MessageContent
        {
            Id = Guid.NewGuid(),
            Value = text,
            Type = MessageType.Text
        };
    }

    public static MessageContent CreateFileContent(string fileUrl, string fileName, string contentType)
    {
        return new MessageContent
        {
            Id = Guid.NewGuid(),
            Value = fileUrl,
            Type = MessageType.File,
            FileUrl = fileUrl,
            FileName = fileName,
            FileContentType = contentType
        };
    }

    public static MessageContent CreateEmojiContent(string emojiCode)
    {
        return new MessageContent
        {
            Id = Guid.NewGuid(),
            Value = emojiCode,
            Type = MessageType.Emoji
        };
    }
} 