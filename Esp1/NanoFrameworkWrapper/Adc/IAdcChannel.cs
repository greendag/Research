using System;

namespace NanoFrameworkWrapper.Adc
{
    public interface IAdcChannel : IDisposable
    {
        IAdcController Controller { get; }
        
        int ChannelNumber { get; }

        int ReadValue();
        
        double ReadRatio();
    }
}