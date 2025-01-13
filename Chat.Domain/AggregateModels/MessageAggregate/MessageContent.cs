using Chat.Domain.SeedWork;

namespace Chat.Domain.AggregateModels.MessageAggregate;

public sealed class MessageContent : ValueObject
{
    public required string Value { get; init; }
    public required MessageType Type { get; init; }

    private MessageContent() { }

    public static MessageContent Create(string content, MessageType type)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentNullException(nameof(content));
            
        if (content.Length > 5000)
            throw new ArgumentException("Message content too long", nameof(content));

        ValidateContentByType(content, type);

        return new MessageContent { Value = content, Type = type };
    }

    private static void ValidateContentByType(string content, MessageType type)
    {
        switch (type)
        {
            case MessageType.Text:
                break;
            case MessageType.Image:
                break;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Type;
    }

    public bool IsText() => Type == MessageType.Text;
    public bool IsImage() => Type == MessageType.Image;
    public bool IsVoice() => Type == MessageType.Voice;
    public bool IsVideo() => Type == MessageType.Video;
    public bool IsFile() => Type == MessageType.File;

    public bool IsMediaType() => IsImage() || IsVoice() || IsVideo();
} 