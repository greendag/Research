using System;

namespace AdcServices.ConcreteClasses
{
    [Serializable]
    public class Lut : ILut
    {
        #region Fields

        private readonly Point[] _table;

        #endregion

        #region Properties

        public int Count { get; private set; }

        #endregion

        #region Initialization

        public Lut(int size)
        {
            _table = new Point[size];
        }

        #endregion

        #region Public Methods

        public void Add(Point point)
        {
            _table[Count] = point;
            Count++;
        }

        public void Add(int x, int y)
        {
            _table[Count] = new Point(x, y);
            Count++;
        }

        public Point GetPoint(int x)
        {
            int min = 0;
            int max = _table.Length - 1;
            int mid = 0;

            while (min <= max)
            {
                mid = (min + max) / 2;

                var point = _table[mid];

                if (x == point.X)
                {
                    return point;
                }

                if (x < point.X)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }

            // use interpolate to create a point
            var interpolatePoint = CreateInterpolatePoint(x, mid);

            return interpolatePoint;
        }

        #endregion

        #region Private Methods

        private Point CreateInterpolatePoint(int x, int approxIndex)
        {
            int startIndex = Math.Max(approxIndex - 1, 0);

            Point point1 = null;
            Point point2 = null;

            int i = startIndex;
            while (i < Count)
            {
                if (point1 != null && point2 != null)
                {
                    break;
                }

                var point = _table[i];

                if (point.X > x && point1 == null)
                {
                    point1 = _table[i];
                }
                else if (point.X < x)
                {
                    point2 = _table[i];
                }

                i++;
            }

            int x1;
            int y1;
            int x2;
            int y2;

            if (point1 == null && point2 == null)
            {
                return new Point(0, 0, true);
            }

            if (point1 == null)
            {
                x1 = 0;
                y1 = 0;
                x2 = point2.X;
                y2 = point2.Y;
            }
            else if (point2 == null)
            {
                x1 = point1.X;
                y1 = point1.Y;
                x2 = 256;
                y2 = 4096;
            }
            else
            {
                x1 = point1.X;
                y1 = point1.Y;
                x2 = point2.X;
                y2 = point2.Y;
            }

            ushort y = (ushort)(y1 + (x - x1) * ((y2 - y1) / (x2 - x1)));

            return new Point(x, y, true);
        }

        #endregion
    }
}
