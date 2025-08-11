using System.Diagnostics;

namespace middleware.Middleware
{
    public class ProfilingMiddleware
    {
        // to move to next middleware
        private readonly RequestDelegate _next;
        ILogger<ProfilingMiddleware> _logger;
        public ProfilingMiddleware(RequestDelegate next,ILogger<ProfilingMiddleware>logger)
        {
            _next = next;
            _logger = logger;
        }
        // HttpContent all information about request and response 
        public async Task InvokeAsync (HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Restart();
           await _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"Request `{context.Request.Path}` took `{stopwatch.Elapsed.TotalMilliseconds}ms`");


        }

    }
}
