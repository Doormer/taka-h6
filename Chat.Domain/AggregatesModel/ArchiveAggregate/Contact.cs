namespace Chat.Domain;

public record Contact(Guid Id, string UserName, string AvatarUrl);

public record Message(Guid Id, string Msg, DateTime CreatedAt);
