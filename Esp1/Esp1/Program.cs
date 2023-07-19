using AdcServices.Extensions;
using ConfigurationManager.Extensions;
using DacServices.Extensions;
using Esp32.Extensions;
using Microsoft.Extensions.Logging;
using nanoFramework.DependencyInjection;
using nanoFramework.Hosting;
using nanoFramework.Logging.Debug;
using NanoFrameworkWrapper.Extensions;
using System;
using TestClassLibrary;

namespace Esp1
{
    public class Program
    {
        private static IHost _host;

        public static void Main()
        {
            Console.WriteLine($"Starting");

            _host = Host.CreateDefaultBuilder()
                .ConfigureEsp32Services()
                .ConfigureConfigurationManagerServices()
                .ConfigureNanoFrameworkServices()
                .ConfigureAdcServices()
                .ConfigureDacServices()
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton(typeof(ILoggerFactory), typeof(DebugLoggerFactory))
                        .AddHostedService(typeof(TestService));
                })
                .Build();

            _host.Start();
        }
    }
}
