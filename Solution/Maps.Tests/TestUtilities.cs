using System;
using System.IO;
using Maps.Geographical;
using Maps.Geographical.Features;
using NUnit.Framework;

namespace Maps.Tests
{
    /// <summary>
    /// Provides many methods and properties to assist in testing
    /// </summary>
    public static class TestUtilities
    {
        /// <summary>
        /// The path to the reference files directory
        /// </summary>
        public static readonly string ReferenceFilesDirectory = 
            AppDomain.CurrentDomain.BaseDirectory + 
            Path.DirectorySeparatorChar + ".." +
            Path.DirectorySeparatorChar + ".." +
            Path.DirectorySeparatorChar + "ReferenceFiles" +
            Path.DirectorySeparatorChar;

        /// <summary>
        /// The path to the directory of the executing test assembly
        /// </summary>
        public static readonly string WorkingDirectory = 
            AppDomain.CurrentDomain.BaseDirectory + 
            Path.DirectorySeparatorChar;

        /// <summary>
        /// The path to the working data directory
        /// </summary>
        public static readonly string WorkingDataDirectory = WorkingDirectory
            + Path.DirectorySeparatorChar + "Data" +
            Path.DirectorySeparatorChar;

        /// <summary>
        /// The centre of Ingolstadt, Germany
        /// </summary>
        public static Geodetic2d Ingolstadt => new Geodetic2d(48.76411d, 11.4209873d);

        /// <summary>
        /// A box surrounding Ingolstadt
        /// </summary>
        public static GeodeticBox2d IngolstadtBox => new GeodeticBox2d(
            new Geodetic2d(48.7893d, 11.3609d),
            new Geodetic2d(48.7350d, 11.4956d));

        /// <summary>
        /// A large box surrounding Ingolstadt, Germany
        /// </summary>
        public static GeodeticBox2d BigIngolstadtBox => new GeodeticBox2d(
            new Geodetic2d(48.8636d, 11.1638d),
            new Geodetic2d(48.6533d, 11.7028d));

        /// <summary>
        /// Very small slice of the city centre of Ingolstadt, Germany
        /// </summary>
        public static GeodeticBox2d TinyIngolstadtBox => new GeodeticBox2d(
            new Geodetic2d(48.76597d, 11.42552d), new Geodetic2d(48.76525d, 11.42681d));

        /// <summary>
        /// A massive box surrounding nurnberg to munich
        /// </summary>
        public static GeodeticBox2d NurnbergToMunichBox => new GeodeticBox2d(
            new Geodetic2d(49.639d, 10.514d),
            new Geodetic2d(47.798d, 12.349d));

        /// <summary>
        /// A box encompassing all of Bavaria
        /// </summary>
        public static GeodeticBox2d BavariaBox => new GeodeticBox2d(
            new Geodetic2d(50.395d, 7.333d),
            new Geodetic2d(47.029d, 15.958d));

        /// <summary>
        /// The centre of Cranbourne, Australia
        /// </summary>
        public static Geodetic2d Cranbourne => new Geodetic2d(-38.11074d, 145.25929d);

        /// <summary>
        /// Asserts that the given two single preceision floating point
        /// values are equal or within an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatSinglesAreEqual(float expected, 
            float actual, float epsilon = 0f)
        {
            if (expected == actual)
            {
                if (epsilon > 0f)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test.");
                }

                return;
            }

            var diff = Mathf.Abs(actual - expected);

            if (diff > epsilon)
            {
                throw new AssertionException("Expected: " + expected + "f +/- " + epsilon + 
                                             "f\n But was: " + actual +"f");
            }

            if (diff < epsilon * 0.1f)
            {
                throw new AssertionException("Difference is lower than next epsilon step, " +
                                             "epsilon could be lowered.");
            }
        }

        /// <summary>
        /// Asserts that the given two double precision floating point
        /// values are equal or within an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatDoublesAreEqual(double expected, 
            double actual, double epsilon = 0d)
        {
            if (expected == actual)
            {
                if (epsilon > 0d)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test.");
                }

                return;
            }

            var diff = Math.Abs(actual - expected);

            if (diff > epsilon)
            {
                throw new AssertionException("Expected: " + expected + "d +/- " + epsilon + 
                                             "d\n But was: " + actual + "d");
            }

            if (diff < epsilon * 0.1d)
            {
                throw new AssertionException("Difference is lower than next epsilon step, " +
                                             "epsilon could be lowered.");
            }
        }

        /// <summary>
        /// Asserts that the two given vectors are equal or within
        /// an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatVector2d32sAreEqual(Vector2f expected, 
            Vector2f actual, float epsilon = 0f)
        {
            if (expected != actual)
            {
                var xdiff = Mathf.Abs(actual.x - expected.x);
                var ydiff = Mathf.Abs(actual.y - expected.y);

                if (xdiff > epsilon || ydiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " + 
                                                 epsilon + "f\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1f;

                if (xdiff < nextEpsilon && ydiff < nextEpsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step," +
                                                 " epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0f)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that the two given double precision vectors
        /// are equal or within an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatVector2dsAreEqual(Vector2d expected, 
            Vector2d actual, double epsilon = 0d)
        {
            if (expected != actual)
            {
                var xdiff = Math.Abs(actual.x - expected.x);
                var ydiff = Math.Abs(actual.y - expected.y);

                if (xdiff > epsilon || ydiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " + 
                                                 epsilon + "d\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1d;

                if (xdiff < nextEpsilon && ydiff < nextEpsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step, " +
                                                 "epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0d)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that the two given vectors are equal or within
        /// an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatVector3d32sAreEqual(Vector3f expected, 
            Vector3f actual, float epsilon = 0f)
        {
            if (expected != actual)
            {
                var xdiff = Mathf.Abs(actual.x - expected.x);
                var ydiff = Mathf.Abs(actual.y - expected.y);
                var zdiff = Mathf.Abs(actual.z - expected.z);

                if (xdiff > epsilon ||
                    ydiff > epsilon ||
                    zdiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " + 
                                                 epsilon + "f\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1f;

                if (xdiff < nextEpsilon &&
                    ydiff < nextEpsilon &&
                    zdiff < nextEpsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step, " +
                                                 "epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0f)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that the two given double precision vectors
        /// are equal or within an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatVector3dsAreEqual(Vector3d expected, 
            Vector3d actual, double epsilon = 0d)
        {
            if (expected != actual)
            {
                var xdiff = Math.Abs(actual.x - expected.x);
                var ydiff = Math.Abs(actual.y - expected.y);
                var zdiff = Math.Abs(actual.z - expected.z);

                if (xdiff > epsilon ||
                    ydiff > epsilon ||
                    zdiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " + 
                                                 epsilon + "d\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1d;

                if (xdiff < nextEpsilon &&
                    ydiff < nextEpsilon &&
                    zdiff < nextEpsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step, " +
                                                 "epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0d)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that the two given double precision vectors
        /// are equal or within an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatVector4dsAreEqual(Vector4d expected,
            Vector4d actual, double epsilon = 0d)
        {
            if (expected != actual)
            {
                var xdiff = Math.Abs(actual.x - expected.x);
                var ydiff = Math.Abs(actual.y - expected.y);
                var zdiff = Math.Abs(actual.z - expected.z);
                var wdiff = Math.Abs(actual.w - expected.w);

                if (xdiff > epsilon ||
                    ydiff > epsilon ||
                    zdiff > epsilon ||
                    wdiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " +
                                                 epsilon + "d\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1d;

                if (xdiff < nextEpsilon &&
                    ydiff < nextEpsilon &&
                    zdiff < nextEpsilon &&
                    wdiff > epsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step, " +
                                                 "epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0d)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that the two given double precision 2d coordinates
        /// are equal or within an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatGeodetic2dsAreEqual(Geodetic2d expected, 
            Geodetic2d actual, double epsilon = 0d)
        {
            if (expected != actual)
            {
                var xdiff = Math.Abs(actual.Latitude - expected.Latitude);
                var ydiff = Math.Abs(actual.Longitude - expected.Longitude);

                if (xdiff > epsilon || ydiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " +
                                                 epsilon + "d\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1d;

                if (xdiff < nextEpsilon && ydiff < nextEpsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step, " +
                                                 "epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0d)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that the two given double precision 3d coordinates
        /// are equal or within an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatGeodetic3dsAreEqual(Geodetic3d expected, 
            Geodetic3d actual, double epsilon = 0d)
        {
            if (expected != actual)
            {
                var xdiff = Math.Abs(actual.Latitude - expected.Latitude);
                var ydiff = Math.Abs(actual.Longitude - expected.Longitude);
                var zdiff = Math.Abs(actual.Height - expected.Height);

                if (xdiff > epsilon ||
                    ydiff > epsilon ||
                    zdiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " +
                                                 epsilon + "d\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1d;

                if (xdiff < nextEpsilon &&
                    ydiff < nextEpsilon &&
                    zdiff < nextEpsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step, " +
                                                 "epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0d)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that the two given 2x2 matrices are equal or within
        /// an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatMatricesAreEqual(Matrix2d expected,
            Matrix2d actual, double epsilon = 0d)
        {
            if (expected != actual)
            {
                var adiff = Math.Abs(actual.a - expected.a);
                var bdiff = Math.Abs(actual.b - expected.b);
                var cdiff = Math.Abs(actual.c - expected.c);
                var ddiff = Math.Abs(actual.d - expected.d);

                if (adiff > epsilon ||
                    bdiff > epsilon ||
                    cdiff > epsilon ||
                    ddiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " +
                                                 epsilon + "d\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1d;

                if (adiff < nextEpsilon &&
                    bdiff < nextEpsilon &&
                    cdiff < nextEpsilon &&
                    ddiff < nextEpsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step, " +
                                                 "epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0d)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that the two given 3x3 matrices are equal or within
        /// an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatMatricesAreEqual(Matrix3d expected,
            Matrix3d actual, double epsilon = 0d)
        {
            if (expected != actual)
            {
                var adiff = Math.Abs(actual.a - expected.a);
                var bdiff = Math.Abs(actual.b - expected.b);
                var cdiff = Math.Abs(actual.c - expected.c);
                var ddiff = Math.Abs(actual.d - expected.d);
                var ediff = Math.Abs(actual.e - expected.e);
                var fdiff = Math.Abs(actual.f - expected.f);
                var gdiff = Math.Abs(actual.g - expected.g);
                var hdiff = Math.Abs(actual.h - expected.h);
                var idiff = Math.Abs(actual.i - expected.i);

                if (adiff > epsilon ||
                    bdiff > epsilon ||
                    cdiff > epsilon ||
                    ddiff > epsilon ||
                    ediff > epsilon ||
                    fdiff > epsilon ||
                    gdiff > epsilon ||
                    hdiff > epsilon ||
                    idiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " +
                                                 epsilon + "d\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1d;

                if (adiff < nextEpsilon &&
                    bdiff < nextEpsilon &&
                    cdiff < nextEpsilon &&
                    ddiff < nextEpsilon &&
                    ediff < nextEpsilon &&
                    fdiff < nextEpsilon &&
                    gdiff < nextEpsilon &&
                    hdiff < nextEpsilon &&
                    idiff < nextEpsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step, " +
                                                 "epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0d)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that the two given 4x4 matrices are equal or within
        /// an optional epsilon
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatMatricesAreEqual(Matrix4d expected,
            Matrix4d actual, double epsilon = 0d)
        {
            if (expected != actual)
            {
                var adiff = Math.Abs(actual.a - expected.a);
                var bdiff = Math.Abs(actual.b - expected.b);
                var cdiff = Math.Abs(actual.c - expected.c);
                var ddiff = Math.Abs(actual.d - expected.d);
                var ediff = Math.Abs(actual.e - expected.e);
                var fdiff = Math.Abs(actual.f - expected.f);
                var gdiff = Math.Abs(actual.g - expected.g);
                var hdiff = Math.Abs(actual.h - expected.h);
                var idiff = Math.Abs(actual.i - expected.i);
                var jdiff = Math.Abs(actual.j - expected.j);
                var kdiff = Math.Abs(actual.k - expected.k);
                var ldiff = Math.Abs(actual.l - expected.l);
                var mdiff = Math.Abs(actual.m - expected.m);
                var ndiff = Math.Abs(actual.n - expected.n);
                var odiff = Math.Abs(actual.o - expected.o);
                var pdiff = Math.Abs(actual.p - expected.p);

                if (adiff > epsilon ||
                    bdiff > epsilon ||
                    cdiff > epsilon ||
                    ddiff > epsilon ||
                    ediff > epsilon ||
                    fdiff > epsilon ||
                    gdiff > epsilon ||
                    hdiff > epsilon ||
                    idiff > epsilon ||
                    jdiff > epsilon ||
                    kdiff > epsilon ||
                    ldiff > epsilon ||
                    mdiff > epsilon ||
                    ndiff > epsilon ||
                    odiff > epsilon ||
                    pdiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " +
                                                 epsilon + "d\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1d;

                if (adiff < nextEpsilon &&
                    bdiff < nextEpsilon &&
                    cdiff < nextEpsilon &&
                    ddiff < nextEpsilon &&
                    ediff < nextEpsilon &&
                    fdiff < nextEpsilon &&
                    gdiff < nextEpsilon &&
                    hdiff < nextEpsilon &&
                    idiff < nextEpsilon &&
                    jdiff < nextEpsilon &&
                    kdiff < nextEpsilon &&
                    ldiff < nextEpsilon &&
                    mdiff < nextEpsilon &&
                    ndiff < nextEpsilon &&
                    odiff < nextEpsilon &&
                    pdiff < nextEpsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step, " +
                                                 "epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0d)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that two quaternions are equal
        /// </summary>
        /// <param name="expected">The expected quaternion</param>
        /// <param name="actual">The actual quaternion</param>
        /// <param name="epsilon">The optional epsilon</param>
        public static void AssertThatQuaternionsAreEqual(Quaterniond expected, 
            Quaterniond actual, double epsilon = 0d)
        {
            if (expected != actual)
            {
                var xdiff = Math.Abs(actual.x - expected.x);
                var ydiff = Math.Abs(actual.y - expected.y);
                var zdiff = Math.Abs(actual.z - expected.z);
                var wdiff = Math.Abs(actual.w - expected.w);

                if (xdiff > epsilon ||
                    ydiff > epsilon ||
                    zdiff > epsilon ||
                    wdiff > epsilon)
                {
                    throw new AssertionException("Expected: " + expected + " +/- " +
                                                 epsilon + "d\n But was: " + actual);
                }

                var nextEpsilon = epsilon * 0.1d;

                if (xdiff < nextEpsilon &&
                    ydiff < nextEpsilon &&
                    zdiff < nextEpsilon &&
                    wdiff > epsilon)
                {
                    throw new AssertionException("Difference is lower than next epsilon step, " +
                                                 "epsilon could be lowered.");
                }
            }
            else
            {
                if (epsilon > 0d)
                {
                    throw new AssertionException("Actual value is exactly as expected but " +
                                                 "with epsilon passed, epsilon could be " +
                                                 "dropped from test");
                }
            }
        }

        /// <summary>
        /// Asserts that two 2d geodetic polygons are equal
        /// </summary>
        /// <param name="expected">The expected geodetic polygon</param>
        /// <param name="actual">The actual geodetic polygon</param>
        public static void AssertThatGeodeticPolygon2dsAreEqual(
            GeodeticPolygon2d expected, GeodeticPolygon2d actual)
        {
            if (expected == null && actual == null)
            {
                return;
            }

            if (ReferenceEquals(expected, actual))
            {
                return;
            }

            if (expected == null)
            {
                throw new AssertionException($"{nameof(expected)} is null, " +
                                             $"{nameof(actual)} is not");
            }

            if (actual == null)
            {
                throw new AssertionException($"{nameof(actual)} is null, " +
                                             $"{nameof(expected)} is not");
            }

            Assert.AreEqual(expected.Count, actual.Count);

            // ensure hole count and per-hole coordinate count
            Assert.AreEqual(expected.HoleCount,
                actual.HoleCount);

            for (var i = 0; i < expected.HoleCount; ++i)
            {
                var expectedHole = expected.Hole(i);
                var actualHole = actual.Hole(i);

                Assert.AreEqual(expectedHole.Count,
                    actualHole.Count);

                for (var j = 0; j < actualHole.Count; ++j)
                {
                    var expectedCoordinate = expectedHole[i];
                    var actualCoordinate = actualHole[i];

                    AssertThatGeodetic2dsAreEqual(expectedCoordinate, actualCoordinate);
                }
            }

            for (var i = 0; i < expected.Count; i++)
            {
                var expectedCoordinate = expected[i];
                var actualCoordinate = actual[i];

                AssertThatGeodetic2dsAreEqual(expectedCoordinate, actualCoordinate);
            }
        }

        /// <summary>
        /// Asserts that two geographical features are equal
        /// </summary>
        /// <param name="expected">The expected feature</param>
        /// <param name="actual">The actual feature</param>
        public static void AssertThatFeaturesAreEqual(Feature expected, Feature actual)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asserts that two areas are equal
        /// </summary>
        /// <param name="expected">The expected feature</param>
        /// <param name="actual">The actual feature</param>
        public static void AssertThatAreasAreEqual(Area expected, Area actual)
        {
            if (expected == null && actual == null)
            {
                return;
            }

            if (ReferenceEquals(expected, actual))
            {
                return;
            }

            if (expected == null)
            {
                throw new AssertionException($"{nameof(expected)} is null, " +
                                             $"{nameof(actual)} is not");
            }

            if (actual == null)
            {
                throw new AssertionException($"{nameof(actual)} is null, " +
                                             $"{nameof(expected)} is not");
            }

            // ensure all details of the Area instance
            Assert.AreEqual(expected.Guid, actual.Guid);
            Assert.AreEqual(expected.Name, actual.Name);
            AssertThatGeodeticPolygon2dsAreEqual(expected.Polygon,
                actual.Polygon);
        }
    }
}