using System.Net;
using System.Text.Json;

namespace Logic_Core_Server.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try { await _next(context); }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new { error = "שגיאת שרת פנימית", message = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
