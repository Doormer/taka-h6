namespace Chat.Application.Common.Exceptions;

/// <summary>
/// 聊天应用的基础异常类
/// </summary>
public abstract class ChatException : Exception
{
    protected ChatException(string message) : base(message)
    {
    }
}

/// <summary>
/// 验证失败异常
/// </summary>
public class ChatValidationException : ChatException
{
    public ChatValidationException(string message) : base(message)
    {
    }
}

/// <summary>
/// 资源未找到异常
/// </summary>
public class ChatNotFoundException : ChatException
{
    public ChatNotFoundException(string message) : base(message)
    {
    }
}

/// <summary>
/// 未授权访问消息异常
/// </summary>
public class UnauthorizedMessageAccessException : ChatException
{
    public UnauthorizedMessageAccessException(string message) : base(message)
    {
    }
}

/// <summary>
/// 消息操作异常
/// </summary>
public class MessageOperationException : ChatException
{
    public MessageOperationException(string message) : base(message)
    {
    }
} 