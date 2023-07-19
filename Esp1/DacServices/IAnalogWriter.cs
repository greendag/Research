namespace DacServices
{
    public interface IAnalogWriter
    {
        IAnalogWriterController Controller { get; }

        int ChannelNumber { get; }

        ushort Value { get; set; }

        double Voltage { get; set; }
    }
}