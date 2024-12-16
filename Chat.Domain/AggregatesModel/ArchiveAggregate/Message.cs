namespace Chat.Domain.AggregatesModel.ArchiveAggregate;

public record Message(Guid Id, string Msg, DateTime CreatedAt);
