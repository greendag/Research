using System;

namespace Esp32.ConcreteClasses
{
    public static class Convert2
    {
        /// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 32-bit signed integer.</summary>
        /// <param name="value">The double-precision floating-point number to convert.</param>
        /// <returns>
        /// <paramref name="value" />, rounded to the nearest 32-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
        /// <exception cref="T:System.OverflowException">
        /// <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />.</exception>
        public static int ToInt32(double value)
        {
            if (value >= 0.0)
            {
                if (value < 2147483647.5)
                {
                    int int32 = (int)value;

                    double num = value - int32;
                    
                    if (num > 0.5 || num == 0.5 && (int32 & 1) != 0)
                    {
                        ++int32;
                    }

                    return int32;
                }
            }
            else if (value >= -2147483648.5)
            {
                int int32 = (int)value;

                double num = value - int32;
                
                if (num < -0.5 || num == -0.5 && (int32 & 1) != 0)
                {
                    --int32;
                }

                return int32;
            }

            throw new SystemException("Overflow");
        }
    }
}
