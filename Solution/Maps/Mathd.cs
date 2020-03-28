using System;

namespace Maps
{
    /// <summary>
    /// A collection of missing math helpers from System.Math
    /// </summary>
    public struct Mathd
    {
        /// <summary>
        /// Tolerance of 1e-21
        /// </summary>
        public const double EpsilonE21 = 1e-21d;

        /// <summary>
        /// Tolerance of 1e-20
        /// </summary>
        public const double EpsilonE20 = 1e-20d;

        /// <summary>
        /// Tolerance of 1e-19
        /// </summary>
        public const double EpsilonE19 = 1e-19d;

        /// <summary>
        /// Tolerance of 1e-18
        /// </summary>
        public const double EpsilonE18 = 1e-18d;

        /// <summary>
        /// Tolerance of 1e-17
        /// </summary>
        public const double EpsilonE17 = 1e-17d;

        /// <summary>
        /// Tolerance of 1e-16
        /// </summary>
        public const double EpsilonE16 = 1e-16d;

        /// <summary>
        /// Tolerance of 1e-15
        /// </summary>
        public const double EpsilonE15 = 1e-15d;

        /// <summary>
        /// Tolerance of 1e-14
        /// </summary>
        public const double EpsilonE14 = 1e-14d;

        /// <summary>
        /// Tolerance of 1e-13
        /// </summary>
        public const double EpsilonE13 = 1e-13d;

        /// <summary>
        /// Tolerance of 1e-12
        /// </summary>
        public const double EpsilonE12 = 1e-12d;

        /// <summary>
        /// Tolerance of 1e-11
        /// </summary>
        public const double EpsilonE11 = 1e-11d;

        /// <summary>
        /// Tolerance of 1e-10
        /// </summary>
        public const double EpsilonE10 = 1e-10d;

        /// <summary>
        /// Tolerance of 1e-9
        /// </summary>
        public const double EpsilonE9 = 1e-9d;

        /// <summary>
        /// Tolerance of 1e-8
        /// </summary>
        public const double EpsilonE8 = 1e-8d;

        /// <summary>
        /// Tolerance of 1e-7
        /// </summary>
        public const double EpsilonE7 = 1e-7d;

        /// <summary>
        /// Tolerance of 1e-6
        /// </summary>
        public const double EpsilonE6 = 1e-6d;

        /// <summary>
        /// Tolerance of 1e-5
        /// </summary>
        public const double EpsilonE5 = 1e-5d;

        /// <summary>
        /// Tolerance of 1e-4
        /// </summary>
        public const double EpsilonE4 = 1e-4d;

        /// <summary>
        /// Tolerance of 1e-3
        /// </summary>
        public const double EpsilonE3 = 1e-3d;

        /// <summary>
        /// Tolerance of 1e-2
        /// </summary>
        public const double EpsilonE2 = 1e-2d;

        /// <summary>
        /// Tolerance of 1e-1
        /// </summary>
        public const double EpsilonE1 = 1e-1d;

        /// <summary>
        /// A very small floating point value
        /// </summary>
        public const double Epsilon = 2.2250738585072014e-308;

        /// <summary>
        /// Constant for degrees to radians conversion
        /// </summary>
        public const double Deg2Rad = 0.01745329251994329576923690768489d;

        /// <summary>
        /// Constant for radians to degrees conversion
        /// </summary>
        public const double Rad2Deg = 57.295779513082320876798154814105d;

        /// <summary>
        /// Constant for half of degrees to radians conversion
        /// </summary>
        public const double HalfDeg2Rad = Deg2Rad * 0.5d;

        /// <summary>
        /// Constant for major radius of earth in meters
        /// </summary>
        public const double RMajor = 6378137.0d;

        /// <summary>
        /// Constant for minor radius of earth in meters
        /// </summary>
        public const double RMinor = 6356752.314245d;

        /// <summary>
        /// Constant for average radius of earth in meters
        /// </summary>
        public const double RAverage = 6371000.0d;

        /// <summary>
        /// Constant for the equatorial circumference of the earth
        /// </summary>
        public const double CEquatorial = 40075017.0d;

        /// <summary>
        /// Constant for the meridional circumference of the earth
        /// </summary>
        public const double CMeridional = 40075017.0d;

        /// <summary>
        /// Clamps the given value between min and max
        /// </summary>
        /// <param name="d">The value to clamp</param>
        /// <param name="min">The minimum to clamp within</param>
        /// <param name="max">The maximum to clamp within</param>
        /// <returns></returns>
        public static double Clamp(double d, double min, double max)
        {
            if (d < min)
            {
                return min;
            }

            if (d > max)
            {
                return max;
            }

            return d;
        }

        /// <summary>
        /// Clamps the given value between 0-1
        /// </summary>
        /// <param name="d">The value to clamp</param>
        public static double Clamp01(double d)
        {
            if (d < 0f)
            {
                return 0d;
            }

            if (d > 1f)
            {
                return 1d;
            }

            return d;
        }

        /// <summary>
        /// Returns true if the difference between the given values
        /// falls within the epsilon tolerance given
        /// </summary>
        public static bool EpsilonEquals(double a, double b, double epsilon = Epsilon)
        {
            return Math.Abs(a - b) <= epsilon;
        }

        /// <summary>
        /// Evaluates the difference between two angles
        /// </summary>
        /// <param name="a">The first angle</param>
        /// <param name="b">The second angle</param>
        /// <returns>The difference between two angles</returns>
        public static double AngleDifference(double a, double b)
        {
            var difference = b - a;
            while (difference < -180) difference += 360;
            while (difference > 180) difference -= 360;
            return difference;
        }

        /// <summary>
        /// Evaluates the mid angle between two angles
        /// </summary>
        /// <param name="a">The first angle</param>
        /// <param name="b">The second angle</param>
        /// <returns>The mid angle between two angles</returns>
        public static double MidAngle(double a, double b)
        {
            return a + 0.5d * Mathd.AngleDifference(a, b);
        }
    }
}
