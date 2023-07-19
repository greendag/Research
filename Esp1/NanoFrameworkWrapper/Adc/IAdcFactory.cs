namespace NanoFrameworkWrapper.Adc
{
    public interface IAdcFactory
    {
        IAdcChannel OpenChannel(int channelNumber);
    }
}