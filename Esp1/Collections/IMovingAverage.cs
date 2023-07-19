namespace Collections
{
    public interface IMovingAverage
    {
        int SampleSize { get; set; }
        int Value { get; }
        void Update(int value);
    }
}