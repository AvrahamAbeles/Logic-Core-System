
namespace Logic_Core_Server.Extensions
{
    public static class SerilogExtensions
    {
        public static void AddSerilogConfiguration(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
