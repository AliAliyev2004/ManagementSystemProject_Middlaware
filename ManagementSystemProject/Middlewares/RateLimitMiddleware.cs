using System.Collections.Concurrent;
using System.Net;
using System.Text.Json;
using ManagementSystem.Common.GlobalResponses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManagementSystemProject.Middlewares;

public class RateLimitMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private static readonly ConcurrentDictionary<string, int> _requestCounts = new();
    private static readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1);
    private static DateTime _resetTime = DateTime.UtcNow.Add(_timeWindow);
    private const int _maxRequest = 5;

    public async Task InvokeAsync(HttpContext context)
    {
        string clientKey = "global";

        if (DateTime.UtcNow >= _resetTime)
        {
            _requestCounts.Clear();
            _resetTime = DateTime.UtcNow.Add(_timeWindow);
        }

        _requestCounts.AddOrUpdate(clientKey, 1, (_, count) => count + 1);

        if (_requestCounts[clientKey] >= _maxRequest)
        {
            var message = new List<string>() { "Many Request. Biraz Gozleyin" };
            await WriteError(context, HttpStatusCode.TooManyRequests, message);
            return;
        }
        await _next(context);
    }

    public static async Task WriteError(HttpContext context, HttpStatusCode statusCode, List<string> messages)
    {
        context.Response.Clear();
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(new Result(messages));
        await context.Response.WriteAsync(json);
    }
}