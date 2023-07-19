using System;

namespace AdcServices
{
    public interface IAnalogReader : IDisposable
    {
        IAdcSensorController Controller { get; }

        int ChannelNumber { get; }

        int Value { get; }

        double Ratio { get; }

        double Voltage { get; }

        string ShowChannelConfiguration();
    }
}