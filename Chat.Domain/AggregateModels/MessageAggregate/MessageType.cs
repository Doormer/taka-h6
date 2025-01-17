namespace Chat.Domain.AggregateModels.MessageAggregate;

/// <summary>
/// Defines the different types of messages that can be sent in the chat system.
/// Supports text, image and file attachments.
/// </summary>
public enum MessageType
{
    Text = 1,
    Image = 2,
    File = 3,
    Emoji = 4
} 