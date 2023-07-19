namespace NanoFrameworkWrapper.Dac
{
    public interface IDacController
    {
        int ChannelCount { get; }

        ushort MinValue { get; }

        ushort MaxValue { get; }

        int ResolutionInBits { get; }
    }
}