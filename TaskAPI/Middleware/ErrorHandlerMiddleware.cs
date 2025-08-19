using System.Net;
using System.Text.Json;

namespace TaskApi.Middleware   // внимавај: TaskApi, не TaskAPI/TaskApi1
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try { await _next(context); }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    title = "Unexpected error",
                    status = context.Response.StatusCode,
                    traceId = context.TraceIdentifier,
                    detail = ex.Message
                }));
            }
        }
    }
}
