using System;

namespace Maps
{
    /// <summary>
    /// A collection of common single precision math functions
    /// </summary>
    public struct Mathf
    {
        /// <summary>
        /// Tolerance of 1e-08
        /// </summary>
        public const float EpsilonE8 = 1e-08f;

        /// <summary>
        /// Tolerance of 1e-07
        /// </summary>
        public const float EpsilonE7 = 1e-07f;

        /// <summary>
        /// Tolerance of 1e-06
        /// </summary>
        public const float EpsilonE6 = 1e-06f;

        /// <summary>
        /// Tolerance of 1e-05
        /// </summary>
        public const float EpsilonE5 = 1e-05f;

        /// <summary>
        /// Tolerance of 1e-04
        /// </summary>
        public const float EpsilonE4 = 1e-04f;

        /// <summary>
        /// Tolerance of 1e-03
        /// </summary>
        public const float EpsilonE3 = 1e-03f;

        /// <summary>
        /// Tolerance of 1e-02
        /// </summary>
        public const float EpsilonE2 = 1e-02f;

        /// <summary>
        /// Tolerance of 1e-01
        /// </summary>
        public const float EpsilonE1 = 1e-01f;

        /// <summary>
        /// A very small floating point number
        /// </summary>
        public const float Epsilon = 1.175494351e-38f;

        /// <summary>
        /// PI constant
        /// </summary>
        public const float Pi = 3.14159274f;

        /// <summary>
        /// Constant for degrees to radians conversion
        /// </summary>
        public const float Deg2Rad = 0.0174532924f;

        /// <summary>
        /// Constant for radians to degrees conversion
        /// </summary>
        public const float Rad2deg = 57.29578f;

        /// <summary>
        /// Constant for mean radius of earth
        /// </summary>
        public const float R = 6.371e6f;

        /// <summary>
        /// Returns a given value raised to the given power
        /// </summary>
        public static float Pow(float f, float power)
        {
            return (float)Math.Pow(f, power);
        }

        /// <summary>
        /// Returns the square root of f
        /// </summary>
        public static float Sqrt(float f)
        {
            return (float)Math.Sqrt(f);
        }

        /// <summary>
        /// Returns the tangent for the given angle in radians
        /// </summary>
        public static float Tan(float f)
        {
            return (float)Math.Tan(f);
        }

        /// <summary>
        /// Returns the angle in radians for the given tangent
        /// </summary>
        public static float Atan(float f)
        {
            return (float)Math.Atan(f);
        }

        /// <summary>
        /// Returns the angle for the tangent of the given numbers
        /// </summary>
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }

        /// <summary>
        /// Returns the cosine of the given angle in radians
        /// </summary>
        public static float Cos(float f)
        {
            return (float)Math.Cos(f);
        }

        /// <summary>
        /// Returns the angle in radians for the given cosine
        /// </summary>
        public static float Acos(float f)
        {
            return (float)Math.Acos(f);
        }

        /// <summary>
        /// Returns the sine of the given angle in radians
        /// </summary>
        public static float Sin(float f)
        {
            return (float)Math.Sin(f);
        }

        /// <summary>
        /// Returns the angle in radians for the given sine
        /// </summary>
        public static float Asin(float f)
        {
            return (float)Math.Asin(f);
        }

        /// <summary>
        /// Clamps the given value between min and max
        /// </summary>
        /// <param name="f">The value to clamp</param>
        /// <param name="min">The minimum to clamp within</param>
        /// <param name="max">The maximum to clamp within</param>
        /// <returns></returns>
        public static float Clamp(float f, float min, float max)
        {
            if (f < min)
            {
                return min;
            }

            if (f > max)
            {
                return max;
            }

            return f;
        }

        /// <summary>
        /// Clamps the given value between 0-1
        /// </summary>
        /// <param name="f">The value to clamp</param>
        public static float Clamp01(float f)
        {
            if (f < 0f)
            {
                return 0f;
            }

            if (f > 1f)
            {
                return 1f;
            }

            return f;
        }

        /// <summary>
        /// Returns the absolute value of the given floating point value
        /// </summary>
        /// <param name="f">The value to return in absolute</param>
        public static float Abs(float f)
        {
            return Math.Abs(f);
        }

        /// <summary>
        /// Returns true if the difference between the given values
        /// falls within the epsilon tolerance given
        /// </summary>
        public static bool EpsilonEquals(float a, float b, float epsilon = Epsilon)
        {
            return Math.Abs(a - b) <= epsilon;
        }

        /// <summary>
        /// Returns the larger of the two values
        /// </summary>
        public static float Max(float a, float b)
        {          
            return Math.Max(a, b);
        }
        
        /// <summary>
        /// Returns the smaller of the two values
        /// </summary>
        public static float Min(float a, float b)
        {
            return Math.Min(a, b);
        }
    }
}