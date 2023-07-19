using System;
using Esp32;
using NanoFrameworkWrapper.Dac;

namespace DacServices.ConcreteClasses
{
    public class AnalogWriter : IAnalogWriter
    {
        #region Fields

        private readonly IDacChannel _dacChannel;
        private readonly IHardware _hardware;
        private ushort _value;

        #endregion

        #region Properties

        public IAnalogWriterController Controller { get; }

        public int ChannelNumber { get; }

        public int MaxResolution => _dacChannel.Controller.ResolutionInBits;

        public ushort Value
        {
            get => _value;
            set
            {
                if (value > MaxResolution)
                {
                    throw new ArgumentException($"{nameof(Value)} must be less than {MaxResolution}.", nameof(Value));
                }

                _value = value;

                _dacChannel.WriteValue(value);
            }
        }

        public double Voltage
        {
            get => Value / (double)MaxResolution * _hardware.ReferenceVoltage;
            set
            {
                if (value < 0f || value > _hardware.ReferenceVoltage)
                {
                    throw new ArgumentException($"{nameof(Voltage)} must be between 0V and {_hardware.ReferenceVoltage}.", nameof(Voltage));
                }

                Value = (ushort)(value / _hardware.ReferenceVoltage * MaxResolution);
            }
        }

        #endregion

        #region Initialization

        public AnalogWriter(
            IHardware hardware,
            IAnalogWriterController analogWriterController,
            IDacFactory dacFactory,
            int channelNumber)
        {
            _hardware = hardware;
            Controller = analogWriterController;
            ChannelNumber = channelNumber;

            _dacChannel = dacFactory.OpenChannel(channelNumber);
        }

        #endregion
    }
}
