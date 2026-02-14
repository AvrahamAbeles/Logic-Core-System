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
                Log.Error(ex, "שגיאה לא מטופלת בבקשה בכתובת: {Path}", context.Request.Path);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new {
                    error = "Internal Server Error",
                    message = "אירעה שגיאה פנימית בשרת. נא לפנות לתמיכה הטכנית." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
