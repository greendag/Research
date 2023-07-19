using nanoFramework.DependencyInjection;
using nanoFramework.Hosting;

namespace Logging.Extensions
{
    public static class ExtensionMethods
    {
        public static IHostBuilder ConfigureLoggingServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddSingleton(typeof(ILoggerFactory), typeof(LoggerFactory));
            });

            return hostBuilder;
        }
    }
}
