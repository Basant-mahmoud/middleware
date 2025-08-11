using System.Diagnostics.Metrics;

namespace middleware.Middleware
{
    public class RateLimittingMiddelware
    {
        private readonly RequestDelegate _next;
        private static int _counter=0;
        private static DateTime _lastRequestData = DateTime.Now;

        public RateLimittingMiddelware (RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            _counter++;
            if (DateTime.Now -_lastRequestData > TimeSpan.FromSeconds(10))
            {
                _counter = 1;
                _lastRequestData = DateTime.Now;
                await _next(context);

            }
            else
            {
                if (_counter > 5)
                {
                    _lastRequestData = DateTime.Now;
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Rate Limit Exceeded");
                    


                }
                else
                {
                    _lastRequestData = DateTime.Now;
                    await _next(context);

                }
            }
        
           
        }
    }
}
