using AdcServices.ConcreteClasses;
using nanoFramework.DependencyInjection;
using nanoFramework.Hosting;

namespace AdcServices.Extensions
{
    public static class ExtensionMethods
    {
        public static IHostBuilder ConfigureAdcServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddSingleton(typeof(IAdcSensorController), typeof(AdcSensorController))
                    .AddSingleton(typeof(IAnalogReaderFactory), typeof(AnalogReaderFactory))
                    .AddSingleton(typeof(ILut), typeof(Lut))
                    .AddSingleton(typeof(IPoint), typeof(Point));
            });

            return hostBuilder;
        }
    }
}
