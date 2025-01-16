using Chat.Application.Common.Interfaces;
using System.Text.Json;

namespace Chat.Application.Common.Caching;

public static class CacheKeyGenerator
{
    public static string Generate<T>(T request) where T : ICacheableQuery
    {
        var requestType = typeof(T).Name;
        var requestJson = JsonSerializer.Serialize(request, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return $"{requestType}_{requestJson}";
    }
} 