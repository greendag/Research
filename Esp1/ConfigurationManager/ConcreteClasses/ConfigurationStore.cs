using System.IO;
using System.Text;
using nanoFramework.Json;

namespace ConfigurationManager.ConcreteClasses
{
    public class ConfigurationStore
    {
        #region Fields

        private string ConfigFile { get; }

        #endregion

        #region Properties

        public bool IsConfigFileExisting => File.Exists(ConfigFile);

        #endregion

        #region Initialization

        public ConfigurationStore(string configFile = "I:\\configuration.json")
        {
            ConfigFile = configFile;
        }

        #endregion

        #region Public Methods

        public void ClearConfig()
        {
            if (File.Exists(ConfigFile))
            {
                File.Delete(ConfigFile);
            }
        }

        public Configuration GetConfig()
        {
            if (!IsConfigFileExisting)
            {
                return null;
            }

            var json = new FileStream(ConfigFile, FileMode.Open);
            var config = (Configuration)JsonConvert.DeserializeObject(json, typeof(Configuration));

            return config;
        }

        public bool WriteConfig(Configuration config)
        {
            try
            {
                var configJson = JsonConvert.SerializeObject(config);
                return WriteConfig(configJson);
            }
            catch
            {
                return false;
            }
        }

        public bool WriteConfig(string config)
        {
            try
            {
                ClearConfig();

                var json = new FileStream(ConfigFile, FileMode.Create);

                byte[] buffer = Encoding.UTF8.GetBytes(config);
                json.Write(buffer, 0, buffer.Length);
                json.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Configuration TestConfiguration()
        {
            return new Configuration
            {
                WaterHeater = new WaterHeater
                {
                    Intake = new WaterPort
                    {
                        Channel = new Channel
                        {
                            ChannelNumber = 7,
                            ValueOffset = 230
                        }
                    },
                    Outtake = new WaterPort
                    {
                        Channel = new Channel
                        {
                            ChannelNumber = 4,
                            ValueOffset = 0
                        }
                    }
                }
            };
        }

        #endregion
    }
}
