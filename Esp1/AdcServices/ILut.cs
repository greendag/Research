using AdcServices.ConcreteClasses;

namespace AdcServices
{
    public interface ILut
    {
        int Count { get; }

        void Add(Point point);

        void Add(int x, int y);

        Point GetPoint(int x);
    }
}