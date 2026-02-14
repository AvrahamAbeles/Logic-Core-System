namespace Logic_Core_Server.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "שגיאה לא מטופלת בבקשה בכתובת: {Path}", context.Request.Path);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                error = "Internal Server Error",
                message = "אירעה שגיאה פנימית בשרת. נא לפנות לתמיכה הטכנית."
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}