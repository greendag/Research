using Esp32.ConcreteClasses;
using nanoFramework.DependencyInjection;
using nanoFramework.Hosting;

namespace Esp32.Extensions
{
    public static class ExtensionMethods
    {
        public static IHostBuilder ConfigureEsp32Services(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddSingleton(typeof(IHardware), typeof(Hardware));
            });

            return hostBuilder;
        }
    }
}
