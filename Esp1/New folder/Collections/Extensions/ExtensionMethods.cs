using nanoFramework.Hosting;
using Collections.ConcreteClasses;
using nanoFramework.DependencyInjection;

namespace Collections.Extensions
{
    public static class ExtensionMethods
    {
        public static IHostBuilder ConfigureCollectionServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddSingleton(typeof(IMovingAverage), typeof(MovingAverage));
            });

            return hostBuilder;
        }
    }
}
