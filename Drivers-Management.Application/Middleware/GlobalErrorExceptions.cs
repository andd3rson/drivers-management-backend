
using System.Text.Json;

namespace Drivers_Management.Application.Middleware
{
    public class GlobalErrorExceptions : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
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