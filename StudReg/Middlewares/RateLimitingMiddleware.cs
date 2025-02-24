using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;
    private readonly int _limit = 5; // Maximum requests allowed
    private readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(10); // Time window

    public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache)
    {
        _next = next;
        _cache = cache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        var cacheKey = $"RateLimit_{ipAddress}";

        if (!_cache.TryGetValue(cacheKey, out List<DateTime> requestTimes))
        {
            requestTimes = new List<DateTime>();
        }

        requestTimes.Add(DateTime.UtcNow);

        // Remove requests that are outside the time window
        requestTimes.RemoveAll(time => time < DateTime.UtcNow - _timeWindow);

        if (requestTimes.Count > _limit)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
            return;
        }

        _cache.Set(cacheKey, requestTimes, _timeWindow);
        await _next(context);
    }
}
