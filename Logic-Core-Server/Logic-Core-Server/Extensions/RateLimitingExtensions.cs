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
                options.AddFixedWindowLimiter("fixed", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(10); 
                    opt.PermitLimit = 10;                   
                    opt.QueueLimit = 5;                    
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsync("יותר מדי בקשות. נסה שוב בעוד 10 שניות.", token);
                };
            });
        }
    }
}