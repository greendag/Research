using nanoFramework.DependencyInjection;
using nanoFramework.Hosting;
using NanoFrameworkWrapper.Adc;
using NanoFrameworkWrapper.Adc.ConcreteClasses;
using NanoFrameworkWrapper.Dac;
using NanoFrameworkWrapper.Dac.ConcreteClasses;

namespace NanoFrameworkWrapper.Extensions
{
    public static class ExtensionMethods
    {
        public static IHostBuilder ConfigureNanoFrameworkServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddSingleton(typeof(IAdcController), typeof(AdcController))
                    .AddSingleton(typeof(IAdcFactory), typeof(AdcFactory))
                    .AddSingleton(typeof(IDacController), typeof(DacController))
                    .AddSingleton(typeof(IDacFactory), typeof(DacFactory));
            });

            return hostBuilder;
        }
    }
}
