using System.Diagnostics;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private Stopwatch _stopWatch;
        private readonly ILogger<RequestTimeMiddleware> _logger;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _stopWatch = new Stopwatch();
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();
            var timeElapsed = _stopWatch.ElapsedMilliseconds;
            if (timeElapsed/1000 > 4)
            {
                var message = $"Request[{context.Request.Method}] at {context.Request.Path} took {timeElapsed} ms";
                _logger.LogTrace(message);
            }
        }
    }
}
