namespace PracticeMiddleware.Middlewares
{
    public class MobileDetectionMiddleware
    {
        private readonly RequestDelegate _next;
        public MobileDetectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"].ToString().ToLower();
            bool isMobile=userAgent.Contains("android")|| userAgent.Contains("iphone") || userAgent.Contains("ipad");
            context.Items["IsMobile"] = isMobile;
            await _next(context);
        }
    }
}
