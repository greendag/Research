namespace NanoFrameworkWrapper.Dac
{
    public interface IDacFactory
    {
        IDacChannel OpenChannel(int channelNumber);
    }
}