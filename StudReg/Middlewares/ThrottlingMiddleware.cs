using System.Collections.Concurrent;

public class ThrottlingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, List<DateTime>> _requestTimes = new();
    private readonly int _requestLimit = 5; // Max 5 requests per 10s
    private readonly TimeSpan _timeWindow = TimeSpan.FromSeconds(10);
    
    public ThrottlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        
        _requestTimes.TryGetValue(clientIp, out var timestamps);
        if (timestamps == null)
        {
            timestamps = new List<DateTime>();
            _requestTimes[clientIp] = timestamps;
        }

        timestamps.Add(DateTime.UtcNow);
        timestamps.RemoveAll(t => t < DateTime.UtcNow - _timeWindow);

        if (timestamps.Count > _requestLimit)
        {
            // If the client exceeds the request limit, introduce a delay (throttle)
            await Task.Delay(10000); // Slow down the request by 2 seconds
        }

        await _next(context);
    }
}
