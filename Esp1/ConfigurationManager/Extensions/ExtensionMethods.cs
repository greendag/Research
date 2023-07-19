using ConfigurationManager.ConcreteClasses;
using nanoFramework.DependencyInjection;
using nanoFramework.Hosting;

namespace ConfigurationManager.Extensions
{
    public static class ExtensionMethods
    {
        #region Public Methods

        public static IHostBuilder ConfigureConfigurationManagerServices(this IHostBuilder hostBuilder)
        {
            var configurationStore = new ConfigurationStore();

#if DEBUG
            configurationStore.ClearConfig();
            //TODO: Figure out how to store a new configuration after the software is loaded.
            configurationStore.WriteConfig(configurationStore.TestConfiguration());
#endif
            Configuration configuration = configurationStore.GetConfig();

            hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddSingleton(typeof(IConfiguration), configuration);
            });

            return hostBuilder;
        }

        #endregion
    }
}