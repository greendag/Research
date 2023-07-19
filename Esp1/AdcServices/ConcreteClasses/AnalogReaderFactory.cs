using ConfigurationManager.ConcreteClasses;
using Esp32;
using Microsoft.Extensions.Logging;
using NanoFrameworkWrapper.Adc;

namespace AdcServices.ConcreteClasses
{
    public class AnalogReaderFactory : IAnalogReaderFactory
    {
        #region Fields

        private readonly IHardware _hardware;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IAdcSensorController _adcSensorController;
        private readonly IAdcFactory _adcFactory;

        #endregion

        #region Initialization

        public AnalogReaderFactory(
            IHardware hardware,
            ILoggerFactory loggerFactory,
            IAdcSensorController adcSensorController,
            IAdcFactory adcFactory)
        {
            _hardware = hardware;
            _loggerFactory = loggerFactory;
            _adcSensorController = adcSensorController;
            _adcFactory = adcFactory;
        }

        #endregion

        #region Public Methods

        public IAnalogReader OpenChannel(Channel channel)
        {
            return new AnalogReader(_hardware, _loggerFactory, _adcSensorController, _adcFactory, channel);
        }

        #endregion
    }
}
