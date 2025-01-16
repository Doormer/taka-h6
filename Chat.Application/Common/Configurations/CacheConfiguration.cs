namespace Chat.Application.Common.Configurations;

public class CacheConfiguration
{
    public int DefaultExpirationMinutes { get; set; } = 5;
    public int MaxCacheItems { get; set; } = 1000;
} 