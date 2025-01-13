namespace Chat.Domain.AggregateModels.MessageAggregate;

public class MessageContent : ValueObject
{
    public string Value { get; private set; }
    public MessageType Type { get; private set; }

    private MessageContent() { }

    public static MessageContent Create(string content, MessageType type)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentNullException(nameof(content));

        return new MessageContent { Value = content, Type = type };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Type;
    }
} 