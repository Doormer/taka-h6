namespace Chat.Application.Common.Configurations;

public class RetryPolicyConfiguration
{
    public int MaxRetries { get; set; } = 3;
    public int RetryDelayMilliseconds { get; set; } = 1000;
} 