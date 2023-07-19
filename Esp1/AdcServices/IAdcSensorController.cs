namespace AdcServices
{
    public interface IAdcSensorController
    {
        int MinValue { get; }

        int MaxValue { get; }
        
        int ResolutionInBits { get; }

        ILut Lut { get; set; }
    }
}
