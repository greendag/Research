using System.Device.Adc;

namespace NanoFrameworkWrapper.Adc
{
    public interface IAdcController
    {
        int ChannelCount { get; }

        AdcChannelMode ChannelMode { get; set; }

        int MinValue { get; }

        int MaxValue { get; }
        
        int ResolutionInBits { get; }

        bool IsChannelModeSupported(AdcChannelMode channelMode);
    }
}