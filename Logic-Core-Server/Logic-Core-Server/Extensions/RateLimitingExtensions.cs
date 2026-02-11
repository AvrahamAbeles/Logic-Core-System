using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Logic_Core_Server.Extensions
{
    public static class RateLimitingExtensions
    {
        public static void AddRateLimitingConfiguration(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                // הגדרת מדיניות בשם "fixed"
                options.AddFixedWindowLimiter("fixed", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(10); // חלון זמן של 10 שניות
                    opt.PermitLimit = 10;                   // מקסימום 10 בקשות
                    opt.QueueLimit = 5;                    // אם עמוס, שמור 2 בקשות בתור
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                // אפשר להוסיף כאן קוד טיפול בשגיאה (Rejection)
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsync("יותר מדי בקשות. נסה שוב בעוד 10 שניות.", token);
                };
            });
        }
    }
}