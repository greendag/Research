namespace DacServices
{
    public interface IAnalogWriterFactory
    {
        IAnalogWriter OpenChannel(int channelNumber);
    }
}