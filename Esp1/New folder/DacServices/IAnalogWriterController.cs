namespace DacServices
{
    public interface IAnalogWriterController
    {
        ushort MinValue { get; }

        ushort MaxValue { get; }

        int ResolutionInBits { get; }
    }
}