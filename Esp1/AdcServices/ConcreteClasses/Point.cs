using System;

namespace AdcServices.ConcreteClasses
{
    [Serializable]
    public class Point : IPoint
    {
        #region Fields

        private bool _interpolate;

        #endregion

        #region Properties

        public int X { get; }

        public int Y { get; }

        #endregion

        #region Initialization

        public Point(int x, int y, bool interpolate = false)
        {
            X = x;
            Y = y;
            _interpolate = interpolate;
        }

        #endregion

        #region Public Methods

        public bool Equals(Point other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return X.Equals(other.X);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode();
        }

        public override string ToString()
        {
            if (_interpolate)
            {
                return $"{Y} ({X})*";
            }

            return $"{Y} ({X})";
        }

        #endregion
    }
}