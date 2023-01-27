using System.Diagnostics;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        var requestMethod = context.Request.Method;
        var requestPath = context.Request.Path;

        Debug.WriteLine($"{requestMethod} {requestPath} {elapsedMilliseconds}ms");
    }
}