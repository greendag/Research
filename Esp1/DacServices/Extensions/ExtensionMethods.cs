using DacServices.ConcreteClasses;
using nanoFramework.DependencyInjection;
using nanoFramework.Hosting;

namespace DacServices.Extensions
{
    public static class ExtensionMethods
    {
        public static IHostBuilder ConfigureDacServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddSingleton(typeof(IAnalogWriterController), typeof(AnalogWriterController))
                    .AddSingleton(typeof(IAnalogWriterFactory), typeof(AnalogWriterFactory));
            });

            return hostBuilder;
        }
    }
}
