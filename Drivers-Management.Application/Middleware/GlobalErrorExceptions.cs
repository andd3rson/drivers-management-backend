
using System.Text.Json;

namespace Drivers_Management.Application.Middleware
{
    public class GlobalErrorExceptions : IMiddleware
    {
        private readonly ILogger<GlobalErrorExceptions> _logger;

        public GlobalErrorExceptions(ILogger<GlobalErrorExceptions> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                _logger.LogError("Something went wrong", ex);
                var message = new
                {
                    Source = ex.Source,
                    Message = ex.Message,
                    StrackTrace = ex.StackTrace,
                };

                string toJson = JsonSerializer.Serialize(message);
                await context.Response.WriteAsJsonAsync(toJson);
            }
        }
    }
}