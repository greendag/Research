using Esp32.ConcreteClasses;
using Microsoft.Extensions.Logging;

namespace Collections.ConcreteClasses
{
    internal class MovingAverage : IMovingAverage
    {
        #region Fields

        private int[] _values;

        private int _index;

        private long _sum;

        private int _sampleSize = 10;

        private readonly object _classLocker = new();

        private readonly ILogger _logger;

        #endregion

        #region Properties

        public int SampleSize
        {
            get
            {
                lock (_classLocker)
                {
                    return _sampleSize;
                }
            }
            set
            {
                lock (_classLocker)
                {
                    if (_sampleSize == value)
                    {
                        return;
                    }

                    _logger.LogDebug($"Sample size is changing from {_sampleSize} to {value}.");
                    _sampleSize = value;

                    ResizeSample(value);
                }
            }
        }

        public int Value { get; private set; }

        #endregion

        #region Initialization

        public MovingAverage(ILoggerFactory loggerFactory)
        {
            _values = new int[SampleSize];
            _logger = loggerFactory.CreateLogger(nameof(MovingAverage));
        }

        #endregion

        #region Public Methods

        public void Update(int value)
        {
            lock (_classLocker)
            {
                // remove the old value from the sum
                _sum -= _values[_index];

                // overwrite the old value with the new one
                _values[_index] = value;

                // add the new value to the sum
                _sum += _values[_index];

                // increment the index (wrapping back to 0)
                _index = (_index + 1) % SampleSize;

                // calculate the average
                var newAverage = Convert2.ToInt32(_sum / (double)SampleSize);

                if (Value != newAverage)
                {
                    // ReSharper disable InterpolatedStringExpressionIsNotIFormattable
                    _logger.LogDebug($"Moving average changing from {Value:f2} to {newAverage:f2}.");
                    // ReSharper restore InterpolatedStringExpressionIsNotIFormattable
                }

                Value = newAverage;
            }
        }

        #endregion

        #region Private Methods

        private void ResizeSample(int sampleSize)
        {
            lock (_classLocker)
            {
                int[] values = new int[_values.Length];
                _values.CopyTo(values, 0);

                _values = new int[sampleSize];
                values.CopyTo(_values, 0);
            }
        }

        #endregion
    }
}
