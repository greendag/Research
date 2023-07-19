using System;
using nanoFramework.Hosting;
using System.Threading;
using AdcServices;
using ConfigurationManager;
using DacServices;
using Microsoft.Extensions.Logging;

namespace TestClassLibrary
{
    public class TestService : BackgroundService
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IAnalogReader _analogReader;
        private readonly IAnalogWriter _analogWriter;

        #endregion

        #region Initialization

        public TestService(IConfiguration configuration, ILoggerFactory loggerFactory, IAnalogReaderFactory analogReaderFactory, IAnalogWriterFactory analogWriterFactory)
        {
            var waterHeater = configuration.WaterHeater;
            _logger = loggerFactory.CreateLogger(nameof(TestService));

            _analogReader = analogReaderFactory.OpenChannel(waterHeater.Intake.Channel);
            _analogWriter = analogWriterFactory.OpenChannel(1);
        }

        #endregion

        #region Protected Methods

        protected override void ExecuteAsync()
        {
            _logger.LogInformation($"ExecuteAsync called. {Thread.CurrentThread.ManagedThreadId}");

            int dacValue = 100;

            _logger.LogDebug("DAC Value\tDAC Scaled Value\tDAC Voltage\tADC Value\tADC Voltage\t");

            while (true)
            {
                int dacScaledValue = (int)Math.Round(dacValue / (double)255 * 4095);
                var dacVoltage = _analogWriter.Voltage;

                int adcValue = _analogReader.Value;
                var adcVoltage = _analogReader.Voltage;

                _logger.LogDebug($"{dacValue:d3}\t{dacScaledValue:d4}\t{dacVoltage:f3}\t{adcValue:d4}\t{adcVoltage:f3}\t");

                dacValue += 5;

                if (dacValue > _analogWriter.Controller.MaxValue)
                {
                    dacValue = _analogWriter.Controller.MinValue;
                }

                _analogWriter.Value = (ushort)dacValue;

                Thread.Sleep(5000);
            }
        }

        #endregion

        #region Public Methods

        public override void Start()
        {
            _logger.LogInformation($"Test service start up. {Thread.CurrentThread.ManagedThreadId}");

            base.Start();
        }

        public override void Stop()
        {
            _logger.LogInformation($"Test service shutdown. {Thread.CurrentThread.ManagedThreadId}");

            base.Stop();
        }

        #endregion
    }
}
