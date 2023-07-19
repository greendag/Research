namespace DacServices
{
    public interface IAnalogWriter
    {
        IAnalogWriterController Controller { get; }

        int ChannelNumber { get; }

        int MaxResolution { get; }

        ushort Value { get; set; }

        double Voltage { get; set; }
    }
}