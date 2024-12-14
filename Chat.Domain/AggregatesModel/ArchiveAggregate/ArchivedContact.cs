namespace Chat.Domain.AggregatesModel.ArchiveAggregate;

public record ArchiveChat(Contact Contact, Message Msg, bool IsArchived);
