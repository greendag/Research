using ConfigurationManager.ConcreteClasses;
using Esp32;
using NanoFrameworkWrapper.Adc;
using Microsoft.Extensions.Logging;

namespace AdcServices.ConcreteClasses
{
    public class AnalogReader : IAnalogReader
    {
        #region Fields

        private readonly IAdcChannel _adcChannel;

        private readonly IHardware _hardware;

        private readonly ILogger _logger;

        #endregion

        #region Properties

        public IAdcSensorController Controller { get; }

        public int ChannelNumber { get; }

        public int Offset { get; }

        public int Value { get; private set; }

        public double Ratio => Value / (double)_adcChannel.Controller.MaxValue;

        public double Voltage => Ratio * _hardware.ReferenceVoltage;

        #endregion

        #region Initialization

        public AnalogReader(
            IHardware hardware,
            ILoggerFactory loggerFactory,
            IAdcSensorController adcSensorAdcSensorController, 
            IAdcFactory adcFactory,
            Channel channel
            )
        {
            _hardware = hardware;
            _logger = loggerFactory.CreateLogger(nameof(AnalogReader));
            Controller = adcSensorAdcSensorController;
            ChannelNumber = channel.ChannelNumber;
            Offset = channel.ValueOffset;

            _adcChannel = adcFactory.OpenChannel(channel.ChannelNumber);
        }

        #endregion

        #region Public Methods

        public string ShowChannelConfiguration()
        {
            var minValue = $"Min Voltage: {_adcChannel.Controller.MinValue}";
            var maxValue = $"Max Voltage: {_adcChannel.Controller.MaxValue}";
            var resolution = $"Resolution: {_adcChannel.Controller.ResolutionInBits}";

            return $"{resolution}\r\n{minValue}\r\n{maxValue}";
        }

        public void Dispose()
        {
            _adcChannel.Dispose();
        }

        #endregion
    }
}
