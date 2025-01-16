namespace Chat.Application.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message)
    {
    }
}

public class MessageNotFoundException : ApplicationException
{
    public MessageNotFoundException(Guid messageId) 
        : base($"消息 {messageId} 未找到")
    {
    }
}

public class UnauthorizedMessageAccessException : ApplicationException
{
    public UnauthorizedMessageAccessException(Guid userId, Guid messageId) 
        : base($"用户 {userId} 无权访问消息 {messageId}")
    {
    }
} 