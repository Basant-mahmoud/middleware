namespace PracticeMiddleware.Middlewares
{
    public class ABTestingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Random _random = new Random();

        public ABTestingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var version = context.Request.Cookies["TestVersion"];
            if (string.IsNullOrEmpty(version))
            {
                version = _random.Next(0, 2) == 0 ? "A" : "B";
                context.Response.Cookies.Append("TestVersion", version, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30)
                });
            }

            context.Items["ABVersion"] = version;

            await _next(context);
        }

    }
}
