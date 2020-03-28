using System;
using Maps.Geographical;
using NUnit.Framework;

namespace Maps.Tests
{
    /// <summary>
    /// Series of tests for the Vector2d class, appearing in order of
    /// members in class
    /// </summary>
    [TestFixture]
    internal sealed class Vector2dTests
    {
        [Test]
        public void TestConstructor()
        {
            var a = new Vector2d(0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(0d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(0d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[1]);

            a = new Vector2d(1d, 1d);

            TestUtilities.AssertThatDoublesAreEqual(1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(1d, a[1]);

            a = new Vector2d(1d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[1]);

            a = new Vector2d(0d, 1d);

            TestUtilities.AssertThatDoublesAreEqual(0d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(0d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(1d, a[1]);

            a = new Vector2d(-1d, -1d);

            TestUtilities.AssertThatDoublesAreEqual(-1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(-1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a[1]);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(67.76451324678221d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(67.76451324678221d, a[1]);

            a = new Vector2d(-72.00231159357315d, -67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(-67.76451324678221d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(-67.76451324678221d, a[1]);

            a = new Vector2d(double.MaxValue, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a.y);

            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a[1]);

            a = new Vector2d(double.MinValue, double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a.y);

            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a[1]);

            a = new Vector2d(double.PositiveInfinity, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.y);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a[1]);

            a = new Vector2d(double.NegativeInfinity, double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a.y);

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a[1]);
        }

        [Test]
        public void TestSingleParameterConstructor()
        {
            var a = new Vector2d(0d);

            TestUtilities.AssertThatDoublesAreEqual(0d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(0d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[1]);

            a = new Vector2d(1d);

            TestUtilities.AssertThatDoublesAreEqual(1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(1d, a[1]);

            a = new Vector2d(-1d);

            TestUtilities.AssertThatDoublesAreEqual(-1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(-1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a[1]);

            a = new Vector2d(72.00231159357315d);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a[1]);

            a = new Vector2d(-72.00231159357315d);

            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a.y);

            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a[1]);

            a = new Vector2d(double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a.y);

            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a[1]);

            a = new Vector2d(double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a.y);

            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a[1]);

            a = new Vector2d(double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.y);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a[1]);

            a = new Vector2d(double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a.y);

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a[1]);
        }

        [Test]
        public void TestCoordinateConstructor()
        {
            var coordinate = new Geodetic2d(0d, 0d);
            var a = new Vector2d(coordinate);

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, a);

            coordinate = new Geodetic2d(1d, 1d);
            a = new Vector2d(coordinate);

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, a);

            coordinate = new Geodetic2d(-1d, -1d);
            a = new Vector2d(coordinate);

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, a);

            coordinate = new Geodetic2d(67.76451324678221d, 72.00231159357315d);
            a = new Vector2d(coordinate);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, 67.76451324678221d), a);

            coordinate = new Geodetic2d(67.76451324678221d, -72.00231159357315d);
            a = new Vector2d(coordinate);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-72.00231159357315d, 67.76451324678221d), a);

            coordinate = new Geodetic2d(-67.76451324678221d, 72.00231159357315d);
            a = new Vector2d(coordinate);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, -67.76451324678221d), a);

            coordinate = new Geodetic2d(-67.76451324678221d, -72.00231159357315d);
            a = new Vector2d(coordinate);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-72.00231159357315d, -67.76451324678221d), a);
        }

        [Test]
        public void TestIndexAccessor()
        {
            var a = Vector2d.Zero;

            Assert.Throws<IndexOutOfRangeException>(() => { var d = a[2]; });
            Assert.Throws<IndexOutOfRangeException>(() => { var d = a[-1]; });
        }

        [Test]
        public void TestHighProperty()
        {
            var a = Vector2d.Zero;
            var b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(a.High, b);

            a = Vector2d.One;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(a.High, b);

            a = new Vector2d(67.76451324678221d, 72.00231159357315d);
            b = new Vector2f(67.76451324678221f, 72.00231159357315f);

            TestUtilities.AssertThatVector2d32sAreEqual(a.High, b);
        }

        [Test]
        public void TestLowProperty()
        {
            var a = Vector2d.Zero;
            var b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(a.Low, b);

            a = Vector2d.One;

            TestUtilities.AssertThatVector2d32sAreEqual(a.Low, Vector2f.Zero);

            a = new Vector2d(67.76451324678221d, 72.00231159357315d);
            b = new Vector2f((float)(a.x - a.High.x), (float)(a.y - a.High.y));

            TestUtilities.AssertThatVector2d32sAreEqual(a.Low, b);
        }

        [Test]
        public void TestMinComponentProperty()
        {
            var a = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MinComponent);

            a = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.MinComponent);

            a = Vector2d.Right;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MinComponent);

            a = Vector2d.Up;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MinComponent);

            a = Vector2d.Left;

            TestUtilities.AssertThatDoublesAreEqual(-1d, a.MinComponent);

            a = Vector2d.Down;

            TestUtilities.AssertThatDoublesAreEqual(-1d, a.MinComponent);
        }

        [Test]
        public void TestMaxComponentProperty()
        {
            var a = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MaxComponent);

            a = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.MaxComponent);

            a = Vector2d.Right;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.MaxComponent);

            a = Vector2d.Up;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.MaxComponent);

            a = Vector2d.Left;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MaxComponent);

            a = Vector2d.Down;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MaxComponent);
        }

        [Test]
        public void TestMagnitudeProperty()
        {
            var a = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(1.41421356237309d, a.Magnitude, Mathd.EpsilonE14);

            a = -Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(1.41421356237309d, a.Magnitude, Mathd.EpsilonE14);

            a = Vector2d.Right;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector2d.Left;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector2d.Up;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector2d.Down;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(98.8754880159451d, a.Magnitude, Mathd.EpsilonE13);

            a = new Vector2d(72.00231159357315d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a.Magnitude);

            a = new Vector2d(0d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(67.76451324678221d, a.Magnitude);

            a = new Vector2d(72.00231159357315d, -67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(98.8754880159451d, a.Magnitude, Mathd.EpsilonE13);

            a = new Vector2d(-72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(98.8754880159451d, a.Magnitude, Mathd.EpsilonE13);

            a = new Vector2d(-72.00231159357315d, -67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(98.8754880159451d, a.Magnitude, Mathd.EpsilonE13);

            a = Vector2d.MaxValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = Vector2d.MinValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector2d(double.MinValue, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector2d(double.MaxValue, double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector2d(double.PositiveInfinity, double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector2d(double.NegativeInfinity, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);
        }

        [Test]
        public void TestSqrMagnitudeProperty()
        {
            var a = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.SqrMagnitude);

            a = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(2d, a.SqrMagnitude);

            a = Vector2d.Right;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.SqrMagnitude);

            a = Vector2d.Up;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.SqrMagnitude);

            a = -Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(2d, a.SqrMagnitude);

            a = new Vector2d(-1d, 1d);

            TestUtilities.AssertThatDoublesAreEqual(2d, a.SqrMagnitude);

            a = Vector2d.Left;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.SqrMagnitude);

            a = new Vector2d(1d, -1d);

            TestUtilities.AssertThatDoublesAreEqual(2d, a.SqrMagnitude);

            a = Vector2d.Down;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.SqrMagnitude);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(9776.36213039131d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector2d(72.00231159357315d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(5184.33287481799d, a.SqrMagnitude, Mathd.EpsilonE11);

            a = new Vector2d(0d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(4592.02925557332d, a.SqrMagnitude, Mathd.EpsilonE11);

            a = new Vector2d(72.00231159357315d, -67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(9776.36213039131d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector2d(-72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(9776.36213039131d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector2d(-72.00231159357315d, -67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(9776.36213039131d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector2d(0d, -67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(4592.02925557332d, a.SqrMagnitude, Mathd.EpsilonE11);

            a = new Vector2d(-72.00231159357315d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(5184.33287481799d, a.SqrMagnitude, Mathd.EpsilonE11);

            a = Vector2d.MaxValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = Vector2d.MinValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector2d(double.MinValue, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector2d(double.MaxValue, double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector2d(double.PositiveInfinity, double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector2d(double.NegativeInfinity, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);
        }

        [Test]
        public void TestXySwizzleProperty()
        {
            var a = new Vector2d(1d, -1d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(a.x, a.y), a.xy);
        }

        [Test]
        public void TestYxSwizzleProperty()
        {
            var a = new Vector2d(1d, -1d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(a.y, a.x), a.yx);
        }

        [Test]
        public void TestNormalisedProperty()
        {
            var a = Vector2d.Zero;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector2d.One;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = Vector2d.Right;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector2d.Up;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = -Vector2d.One;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector2d(-1d, 1d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = Vector2d.Left;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector2d(1d, -1d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = Vector2d.Down;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector2d(72.00231159357315d, 0d);

            a = a.Normalised; 

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector2d(0d, 67.76451324678221d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector2d(72.00231159357315d, -67.76451324678221d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector2d(-72.00231159357315d, 67.76451324678221d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector2d(-72.00231159357315d, -67.76451324678221d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector2d(0d, -67.76451324678221d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector2d(-72.00231159357315d, 0d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector2d.MaxValue;

            // vectors of infinite length are zero length when normalised

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector2d.MinValue;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = new Vector2d(double.MinValue, double.MaxValue);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = new Vector2d(double.MaxValue, double.MinValue);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector2d.PositiveInfinity;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector2d.NegativeInfinity;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = new Vector2d(double.PositiveInfinity, double.NegativeInfinity);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = new Vector2d(double.NegativeInfinity, double.PositiveInfinity);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);
        }

        [Test]
        public void TestPerpendicularProperty()
        {
            var a = Vector2d.One;
            var b = new Vector2d(-1d, 1d);

            TestUtilities.AssertThatVector2dsAreEqual(b, a.Perpendicular);

            a = b;
            b = -Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(b, a.Perpendicular);

            a = b;
            b = new Vector2d(1d, -1d);

            TestUtilities.AssertThatVector2dsAreEqual(b, a.Perpendicular);

            a = b;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(b, a.Perpendicular);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(-67.76451324678221d, 72.00231159357315d);

            TestUtilities.AssertThatVector2dsAreEqual(b, a.Perpendicular);

            a = new Vector2d(72.00231159357315d, -67.76451324678221d);
            b = new Vector2d(67.76451324678221d, 72.00231159357315d);

            TestUtilities.AssertThatVector2dsAreEqual(b, a.Perpendicular);
        }

        [Test]
        public void TestUnaryNegationOperator()
        {
            var a = Vector2d.Zero;
            var aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = Vector2d.One;

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = Vector2d.Right;

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = Vector2d.Up;

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = -Vector2d.One;

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(-1d, 1d);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = Vector2d.Left;

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(1d, -1d);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = Vector2d.Down;

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(72.00231159357315d, 0d);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(0d, 67.76451324678221d);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(72.00231159357315d, -67.76451324678221d);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(-72.00231159357315d, 67.76451324678221d);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(-72.00231159357315d, -67.76451324678221d);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(0d, -67.76451324678221d);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(-72.00231159357315d, 0d);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = Vector2d.MaxValue;

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = Vector2d.MinValue;

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(double.MinValue, double.MaxValue);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(double.MaxValue, double.MinValue);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = Vector2d.PositiveInfinity;

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = Vector2d.NegativeInfinity;

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(double.PositiveInfinity, double.NegativeInfinity);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);

            a = new Vector2d(double.NegativeInfinity, double.PositiveInfinity);

            aNegative = -a;

            TestUtilities.AssertThatDoublesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatDoublesAreEqual(-a.y, aNegative.y);
        }

        [Test]
        public void TestSubtractionOperator()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;
            var c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = Vector2d.One;
            b = Vector2d.One;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = Vector2d.Zero;
            b = Vector2d.One;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, c);

            a = Vector2d.One;
            b = Vector2d.Zero;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, c);

            a = Vector2d.One;
            b = Vector2d.Right;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Up, c);

            a = Vector2d.One;
            b = Vector2d.Up;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Right, c);

            a = Vector2d.Zero;
            b = Vector2d.Right;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Left, c);

            a = Vector2d.Zero;
            b = Vector2d.Up;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Down, c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = Vector2d.Zero;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, 67.76451324678221d), c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = Vector2d.One;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(71.00231159357315d, 66.76451324678221d), c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(10d, 10d);
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(62.00231159357315d, 57.76451324678221d), c, Mathd.EpsilonE14);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(0.00231159357315d, 0.76451324678221d);
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72d, 67d), c);

            // infinity - infinity = undetermined
            a = Vector2d.PositiveInfinity;
            b = Vector2d.PositiveInfinity;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            // -infinity - -infinity = undetermined
            a = Vector2d.NegativeInfinity;
            b = Vector2d.NegativeInfinity;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            // -infinity - infinity = -infinity
            a = Vector2d.NegativeInfinity;
            b = Vector2d.PositiveInfinity;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            // infinity - -infinity = infinity
            a = Vector2d.PositiveInfinity;
            b = Vector2d.NegativeInfinity;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            // max - max = 0
            a = Vector2d.MaxValue;
            b = Vector2d.MaxValue;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            // min - min = 0
            a = Vector2d.MinValue;
            b = Vector2d.MinValue;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            // max - min = infinity
            a = Vector2d.MaxValue;
            b = Vector2d.MinValue;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            // min - max = -infinity
            a = Vector2d.MinValue;
            b = Vector2d.MaxValue;
            c = a - b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);
        }

        [Test]
        public void TestAdditionOperator()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;
            var c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = Vector2d.One;
            b = Vector2d.One;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, 2d), c);

            a = Vector2d.Zero;
            b = Vector2d.One;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, c);

            a = Vector2d.One;
            b = Vector2d.Zero;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, c);

            a = Vector2d.One;
            b = Vector2d.Right;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, 1d), c);

            a = Vector2d.One;
            b = Vector2d.Up;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d, 2d), c);

            a = Vector2d.Zero;
            b = Vector2d.Right;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Right, c);

            a = Vector2d.Zero;
            b = Vector2d.Up;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Up, c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = Vector2d.Zero;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, 67.76451324678221d), c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(144.0046231871463d, 135.5290264935644d), c, Mathd.EpsilonE13);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = Vector2d.One;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(73.00231159357315d, 68.76451324678221d), c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(10d, 10d);
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(82.00231159357315d, 77.76451324678221d), c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(0.00231159357315d, 0.76451324678221d);
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.0046231871463d, 68.52902649356442d), c, Mathd.EpsilonE13);

            // infinity + infinity = infinity
            a = Vector2d.PositiveInfinity;
            b = Vector2d.PositiveInfinity;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            // -infinity + -infinity = -infinity
            a = Vector2d.NegativeInfinity;
            b = Vector2d.NegativeInfinity;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            // -infinity + infinity = NaN
            a = Vector2d.NegativeInfinity;
            b = Vector2d.PositiveInfinity;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            // infinity + -infinity = infinity
            a = Vector2d.PositiveInfinity;
            b = Vector2d.NegativeInfinity;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            // max + max = infinity
            a = Vector2d.MaxValue;
            b = Vector2d.MaxValue;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            // min + min = -infinity
            a = Vector2d.MinValue;
            b = Vector2d.MinValue;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            // max + min = 0
            a = Vector2d.MaxValue;
            b = Vector2d.MinValue;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            // min + max = 0
            a = Vector2d.MinValue;
            b = Vector2d.MaxValue;
            c = a + b;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);
        }

        [Test]
        public void TestMultiplicationOperator()
        {
            var a = Vector2d.Zero;
            var c = a * 0d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            c = 0d * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = Vector2d.Zero;
            c = a * 1d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            c = 1d * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = Vector2d.Zero;
            c = a * -1d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            c = -1d * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = Vector2d.One;
            c = a * 0d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            c = 0d * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = Vector2d.One;
            c = a * 1d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, c);

            c = 1d * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, c);

            a = Vector2d.One;
            c = a * -1d;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, c);

            c = -1d * a;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, c);

            a = Vector2d.One;
            c = a * 2d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, 2d), c);

            c = 2d * a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, 2d), c);

            a = Vector2d.One;
            c = a * 0.5d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0.5d), c);

            c = 0.5d * a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0.5d), c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a * 0d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            c = 0d * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a * 1d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, 67.76451324678221d), c);

            c = 1d * a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, 67.76451324678221d), c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a * -1d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-72.00231159357315d, -67.76451324678221d), c);

            c = -1d * a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-72.00231159357315d, -67.76451324678221d), c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a * 2d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(144.0046231871463d, 135.5290264935644d), c, Mathd.EpsilonE13);

            c = 2d * a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(144.0046231871463d, 135.5290264935644d), c, Mathd.EpsilonE13);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a * 0.5d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(36.00115579678657d, 33.8822566233911d), c, Mathd.EpsilonE14);

            c = 0.5d * a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(36.00115579678657d, 33.8822566233911d), c, Mathd.EpsilonE14);

            // infinity * infinity = infinity
            a = Vector2d.PositiveInfinity;
            c = a * double.PositiveInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            c = double.PositiveInfinity * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            // infinity * -infinity = -infinity
            a = Vector2d.PositiveInfinity;
            c = a * double.NegativeInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            c = double.NegativeInfinity * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            // -infinity * infinity = -infinity
            a = Vector2d.NegativeInfinity;
            c = a * double.PositiveInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            c = double.PositiveInfinity * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            // -infinity * -infinity = infinity
            a = Vector2d.NegativeInfinity;
            c = a * double.NegativeInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            c = double.NegativeInfinity * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            // max * max = infinity
            a = Vector2d.MaxValue;
            c = a * double.MaxValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            c = double.MaxValue * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            // max * min = -infinity
            a = Vector2d.MaxValue;
            c = a * double.MinValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            c = double.MinValue * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            // min * max = -infinity
            a = Vector2d.MinValue;
            c = a * double.MaxValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            c = double.MaxValue * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            // min * min = infinity
            a = Vector2d.MinValue;
            c = a * double.MinValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            c = double.MinValue * a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);
        }

        [Test]
        public void TestDivisionOperator()
        {
            var a = Vector2d.Zero;
            var c = a / 0d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            c = 0d / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            a = Vector2d.Zero;
            c = a / 1d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            c = 1d / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            a = Vector2d.Zero;
            c = a / -1d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            c = -1d / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, c);

            a = Vector2d.One;
            c = a / 0d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            c = 0d / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = Vector2d.One;
            c = a / 1d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, c);

            c = 1d / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, c);

            a = Vector2d.One;
            c = a / -1d;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, c);

            c = -1d / a;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, c);

            a = Vector2d.One;
            c = a / 2d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0.5d), c);

            c = 2d / a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, 2d), c);

            a = Vector2d.One;
            c = a / 0.5d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, 2d), c);

            c = 0.5d / a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0.5d), c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a / 0d;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, c);

            c = 0d / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, c);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a / 1d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, 67.76451324678221d), c);

            c = 1d / a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.01388844299395047d, 0.01475698639431288d), c, Mathd.EpsilonE17);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a / -1d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-72.00231159357315d, -67.76451324678221d), c);

            c = -1d / a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-0.01388844299395047d, -0.01475698639431288d), c, Mathd.EpsilonE17);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a / 2d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(36.00115579678657d, 33.8822566233911d), c, Mathd.EpsilonE14);

            c = 2d / a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.02777688598790094d, 0.02951397278862576d), c, Mathd.EpsilonE17);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            c = a / 0.5d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(144.0046231871463d, 135.5290264935644d), c, Mathd.EpsilonE13);

            c = 0.5d / a;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.00694422149697523d, 0.00737849319715644d), c, Mathd.EpsilonE17);

            // infinity / infinity = NaN
            a = Vector2d.PositiveInfinity;
            c = a / double.PositiveInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            c = double.PositiveInfinity / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            // infinity / -infinity = NaN
            a = Vector2d.PositiveInfinity;
            c = a / double.NegativeInfinity;
            c = double.NegativeInfinity / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            // -infinity / infinity = NaN
            a = Vector2d.NegativeInfinity;
            c = a / double.PositiveInfinity;
            c = double.PositiveInfinity / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            // -infinity / -infinity = NaN
            a = Vector2d.NegativeInfinity;
            c = a / double.NegativeInfinity;
            c = double.NegativeInfinity / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NaN, c);

            // max / max = 1
            a = Vector2d.MaxValue;
            c = a / double.MaxValue;
            c = double.MaxValue / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, c);

            // max / min = -1
            a = Vector2d.MaxValue;
            c = a / double.MinValue;
            c = double.MinValue / a;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, c);

            // min / max = -infinity
            a = Vector2d.MinValue;
            c = a / double.MaxValue;
            c = double.MaxValue / a;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, c);

            // min / min = infinity
            a = Vector2d.MinValue;
            c = a / double.MinValue;
            c = double.MinValue / a;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, c);
        }

        [Test]
        public void TestEqualityOperator()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;
            
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.One;
            b = Vector2d.One;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.Right;
            b = Vector2d.Right;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.Up;
            b = Vector2d.Up;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = -Vector2d.One;
            b = -Vector2d.One;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.Left;
            b = Vector2d.Left;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.Down;
            b = Vector2d.Down;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector2d(-72.00231159357315d, -67.76451324678221d);
            b = new Vector2d(-72.00231159357315d, -67.76451324678221d);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector2d(-72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(-72.00231159357315d, 67.76451324678221d);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector2d(72.00231159357315d, -67.76451324678221d);
            b = new Vector2d(72.00231159357315d, -67.76451324678221d);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.PositiveInfinity;
            b = Vector2d.PositiveInfinity;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.NegativeInfinity;
            b = Vector2d.NegativeInfinity;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.MaxValue;
            b = Vector2d.MaxValue;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.MinValue;
            b = Vector2d.MinValue;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [Test]
        public void TestInequalityOperator()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.One;
            b = Vector2d.One;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2d.Right;
            b = Vector2d.Up;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector2d.Up;
            b = Vector2d.Right;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = new Vector2d(-1d, 1d);
            b = new Vector2d(1d, -1d);

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector2d.Left;
            b = Vector2d.Down;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector2d.Down;
            b = Vector2d.Left;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector2d(-72.00231159357315d, -67.76451324678221d);
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = new Vector2d(-72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, -67.76451324678221d);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = new Vector2d(72.00231159357315d, -67.76451324678221d);
            b = new Vector2d(-72.00231159357315d, 67.76451324678221d);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector2d.PositiveInfinity;
            b = Vector2d.NegativeInfinity;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector2d.NegativeInfinity;
            b = Vector2d.PositiveInfinity;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector2d.MaxValue;
            b = Vector2d.MinValue;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector2d.MinValue;
            b = Vector2d.MaxValue;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);
        }

        [Test]
        public void TestOneProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d, 1d), Vector2d.One);
        }

        [Test]
        public void TestZeroProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, 0d), Vector2d.Zero);
        }

        [Test]
        public void TestUpProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, 1d), Vector2d.Up);
        }

        [Test]
        public void TestDownProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, -1d), Vector2d.Down);
        }

        [Test]
        public void TestRightProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d, 0d), Vector2d.Right);
        }

        [Test]
        public void TestLeftProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1d, 0d), Vector2d.Left);
        }

        [Test]
        public void TestMaxValueProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(double.MaxValue, double.MaxValue), Vector2d.MaxValue);
        }

        [Test]
        public void TestMinValueProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(double.MinValue, double.MinValue), Vector2d.MinValue);
        }

        [Test]
        public void TestPositiveInfinityProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(double.PositiveInfinity, double.PositiveInfinity), Vector2d.PositiveInfinity);
        }

        [Test]
        public void TestNegativeInfinityProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(double.NegativeInfinity, double.NegativeInfinity), Vector2d.NegativeInfinity);
        }

        [Test]
        public void TestNaNProperty()
        {
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(double.NaN, double.NaN), Vector2d.NaN);
        }

        [Test]
        public void TestIsNaNMethod()
        {
            var a = Vector2d.One;

            Assert.IsFalse(Vector2d.IsNaN(a));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);

            Assert.IsFalse(Vector2d.IsNaN(a));

            a = Vector2d.NaN;

            Assert.IsTrue(Vector2d.IsNaN(a));
        }

        [Test]
        public void TestComponentMultiplyMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.ComponentMultiply(a, b));

            a = Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.ComponentMultiply(a, b));

            a = Vector2d.Zero;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.ComponentMultiply(a, b));

            a = Vector2d.One;
            b = -Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, Vector2d.ComponentMultiply(a, b));

            a = Vector2d.One;
            b = new Vector2d(-1d, 1d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1d, 1d), Vector2d.ComponentMultiply(a, b));

            a = Vector2d.One;
            b = new Vector2d(1d, -1d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d, -1d), Vector2d.ComponentMultiply(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(-1d, 1d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-72.00231159357315d, 67.76451324678221d), Vector2d.ComponentMultiply(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(1d, -1d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, -67.76451324678221d), Vector2d.ComponentMultiply(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(2d, 2d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(144.0046231871463d, 135.5290264935644d), Vector2d.ComponentMultiply(a, b), Mathd.EpsilonE13);
        }

        [Test]
        public void TestPowMethod()
        {
            var a = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Pow(a, 0d));
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Pow(a, 1d));

            a = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Pow(a, 0d));
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Pow(a, 1d));
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Pow(a, 2d));
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Pow(a, 3d));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Pow(a, 0d));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, 67.76451324678221d), Vector2d.Pow(a, 1d));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(5184.33287481799d, 4592.02925557332d), Vector2d.Pow(a, 2d), Mathd.EpsilonE11);
        }

        [Test]
        public void TestDistanceMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Distance(a, b));

            a = Vector2d.Zero;
            b = Vector2d.Up;

            TestUtilities.AssertThatDoublesAreEqual(1d, Vector2d.Distance(a, b));

            a = Vector2d.Zero;
            b = Vector2d.Right;

            TestUtilities.AssertThatDoublesAreEqual(1d, Vector2d.Distance(a, b));

            a = Vector2d.Zero;
            b = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(1.414213562373095d, Vector2d.Distance(a, b), Mathd.EpsilonE15);

            a = Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Distance(a, b));

            a = Vector2d.Left;
            b = Vector2d.Right;

            TestUtilities.AssertThatDoublesAreEqual(2d, Vector2d.Distance(a, b));

            a = Vector2d.Down;
            b = Vector2d.Up;

            TestUtilities.AssertThatDoublesAreEqual(2d, Vector2d.Distance(a, b));

            a = -Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(2.828427124746190d, Vector2d.Distance(a, b), Mathd.EpsilonE15);

            a = new Vector2d(72.00231159357315d, 0d);
            b = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, Vector2d.Distance(a, b));

            a = Vector2d.Zero;
            b = new Vector2d(72.00231159357315d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, Vector2d.Distance(a, b));

            a = new Vector2d(0d, 67.76451324678221d);
            b = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(67.76451324678221d, Vector2d.Distance(a, b));

            a = Vector2d.Zero;
            b = new Vector2d(0d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(67.76451324678221d, Vector2d.Distance(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(98.8754880159451d, Vector2d.Distance(a, b), Mathd.EpsilonE13);

            a = Vector2d.Zero;
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(98.8754880159451d, Vector2d.Distance(a, b), Mathd.EpsilonE13);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(67.76451324678221d, Vector2d.Distance(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(0d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, Vector2d.Distance(a, b));
            
            a = Vector2d.PositiveInfinity;
            b = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Distance(a, b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Distance(a, b));

            a = Vector2d.PositiveInfinity;
            b = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector2d.Distance(a, b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector2d.Distance(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.MaxValue;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Distance(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.MinValue;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Distance(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.MinValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector2d.Distance(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.MaxValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector2d.Distance(a, b));
        }

        [Test]
        public void TestComponentDistanceMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.ComponentDistance(a, b));

            a = Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.ComponentDistance(a, b));

            a = Vector2d.One;
            b = -Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, 2d), Vector2d.ComponentDistance(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, 67.76451324678221d), Vector2d.ComponentDistance(a, b));

            a = Vector2d.Zero;
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(72.00231159357315d, 67.76451324678221d), Vector2d.ComponentDistance(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(-72.00231159357315d, -67.76451324678221d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(144.0046231871463d, 135.52902649356442d), Vector2d.ComponentDistance(a, b));
        }

        [Test]
        public void TestScaleMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Scale(a, b));

            a = Vector2d.Zero;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Scale(a, b));

            a = Vector2d.One;
            b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Scale(a, b));

            a = Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Scale(a, b));

            a = Vector2d.One;
            b = -Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, Vector2d.Scale(a, b));

            a = Vector2d.One;
            b = new Vector2d(2d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d), Vector2d.Scale(a, b));

            a = new Vector2d(2d);
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d), Vector2d.Scale(a, b));

            a = Vector2d.One;
            b = new Vector2d(-2d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-2d), Vector2d.Scale(a, b));

            a = new Vector2d(-2d);
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-2d), Vector2d.Scale(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.MaxValue, Vector2d.Scale(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.MinValue, Vector2d.Scale(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.MaxValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, Vector2d.Scale(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.MinValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, Vector2d.Scale(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.MaxValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, Vector2d.Scale(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.MinValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, Vector2d.Scale(a, b));
        }

        [Test]
        public void TestMidpointMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0.5d), Vector2d.Midpoint(a, b));

            a = Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Midpoint(a, b));

            a = Vector2d.One;
            b = -Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Midpoint(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(36.001155796786575d, 33.882256623391105d), Vector2d.Midpoint(a, b));

            a = Vector2d.Zero;
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(36.001155796786575d, 33.882256623391105d), Vector2d.Midpoint(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(-72.00231159357315d, -67.76451324678221d);

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Midpoint(a, b));
        }

        [Test]
        public void TestMaxMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Max(a, b));

            a = Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Max(a, b));

            a = -Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Max(a, b));

            a = Vector2d.One;
            b = -Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Max(a, b));

            a = Vector2d.Zero;
            b = -Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Max(a, b));

            a = -Vector2d.One;
            b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Max(a, b));

            a = Vector2d.One;
            b = Vector2d.One * 2;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One * 2, Vector2d.Max(a, b));

            a = Vector2d.One * 2;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One * 2, Vector2d.Max(a, b));

            a = Vector2d.Up;
            b = Vector2d.Right;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Max(a, b));

            a = Vector2d.Right;
            b = Vector2d.Up;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Max(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(a, Vector2d.Max(a, b));

            a = Vector2d.Zero;
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatVector2dsAreEqual(b, Vector2d.Max(a, b));

            a = Vector2d.PositiveInfinity;
            b = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, Vector2d.Max(a, b));

            a = Vector2d.PositiveInfinity;
            b = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, Vector2d.Max(a, b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, Vector2d.Max(a, b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, Vector2d.Max(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.MaxValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.MaxValue, Vector2d.Max(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.MinValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.MaxValue, Vector2d.Max(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.MaxValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.MaxValue, Vector2d.Max(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.MinValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.MinValue, Vector2d.Max(a, b));
        }

        [Test]
        public void TestMinMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Min(a, b));

            a = Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Min(a, b));

            a = -Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, Vector2d.Min(a, b));

            a = Vector2d.One;
            b = -Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, Vector2d.Min(a, b));

            a = Vector2d.Zero;
            b = -Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, Vector2d.Min(a, b));

            a = -Vector2d.One;
            b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One, Vector2d.Min(a, b));

            a = Vector2d.One;
            b = Vector2d.One * 2;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Min(a, b));

            a = Vector2d.One * 2;
            b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Min(a, b));

            a = Vector2d.Up;
            b = Vector2d.Right;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Min(a, b));

            a = Vector2d.Right;
            b = Vector2d.Up;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Min(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = Vector2d.Zero;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Min(a, b));

            a = Vector2d.Zero;
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Min(a, b));

            a = Vector2d.PositiveInfinity;
            b = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.PositiveInfinity, Vector2d.Min(a, b));

            a = Vector2d.PositiveInfinity;
            b = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, Vector2d.Min(a, b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, Vector2d.Min(a, b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.NegativeInfinity, Vector2d.Min(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.MaxValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.MaxValue, Vector2d.Min(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.MinValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.MinValue, Vector2d.Min(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.MaxValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.MinValue, Vector2d.Min(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.MinValue;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.MinValue, Vector2d.Min(a, b));
        }

        [Test]
        public void TestToStringMethod()
        {
            var a = Vector2d.Zero;
            Assert.AreEqual(@"[0]d,[0]d", a.ToString());

            a = Vector2d.One;
            Assert.AreEqual(@"[1]d,[1]d", a.ToString());

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            Assert.AreEqual(@"[72.0023115935732]d,[67.7645132467822]d", a.ToString());

            a = Vector2d.PositiveInfinity;
            Assert.AreEqual(@"[Infinity]d,[Infinity]d", a.ToString());

            a = Vector2d.NegativeInfinity;
            Assert.AreEqual(@"[-Infinity]d,[-Infinity]d", a.ToString());
        }

        [Test]
        public void TestEqualsMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;

            Assert.IsTrue(a.Equals(b));

            a = Vector2d.One;
            b = Vector2d.One;

            Assert.IsTrue(a.Equals(b));

            a = Vector2d.Right;
            b = Vector2d.Right;

            Assert.IsTrue(a.Equals(b));

            a = Vector2d.Up;
            b = Vector2d.Up;

            Assert.IsTrue(a.Equals(b));

            a = -Vector2d.One;
            b = -Vector2d.One;

            Assert.IsTrue(a.Equals(b));

            a = Vector2d.Left;
            b = Vector2d.Left;

            Assert.IsTrue(a.Equals(b));

            a = Vector2d.Down;
            b = Vector2d.Down;

            Assert.IsTrue(a.Equals(b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            Assert.IsTrue(a.Equals(b));

            // test rounding up
            b = new Vector2d(72.002311593573149d, 67.764513246782209d);

            Assert.IsTrue(a.Equals(b));

            // test rounding down
            b = new Vector2d(72.002311593573151d, 67.764513246782211d);

            Assert.IsTrue(a.Equals(b));

            a = Vector2d.PositiveInfinity;
            b = Vector2d.PositiveInfinity;

            Assert.IsTrue(a.Equals(b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.NegativeInfinity;

            Assert.IsTrue(a.Equals(b));

            a = Vector2d.MaxValue;
            b = Vector2d.MaxValue;

            Assert.IsTrue(a.Equals(b));

            a = Vector2d.MinValue;
            b = Vector2d.MinValue;

            Assert.IsTrue(a.Equals(b));

            var c = "I am not a Vector2d";

            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(a.Equals(c));
        }

        [Test]
        public void TestGetHashCodeMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2d.One;
            b = Vector2d.One;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2d.Right;
            b = Vector2d.Right;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2d.Up;
            b = Vector2d.Up;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = -Vector2d.One;
            b = -Vector2d.One;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2d.Left;
            b = Vector2d.Left;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2d.Down;
            b = Vector2d.Down;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            // test rounding up
            b = new Vector2d(72.002311593573149d, 67.764513246782209d);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            // test rounding down
            b = new Vector2d(72.002311593573151d, 67.764513246782211d);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2d.PositiveInfinity;
            b = Vector2d.PositiveInfinity;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2d.NegativeInfinity;
            b = Vector2d.NegativeInfinity;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2d.MaxValue;
            b = Vector2d.MaxValue;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2d.MinValue;
            b = Vector2d.MinValue;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void TestDotMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Dot(a, b));

            a = Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(2d, Vector2d.Dot(a, b));

            a = Vector2d.Zero;
            b = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Dot(a, b));

            a = Vector2d.One;
            b = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Dot(a, b));

            a = Vector2d.One;
            b = -Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(-2d, Vector2d.Dot(a, b));

            a = -Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(-2d, Vector2d.Dot(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(-72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(-592.3036192446763d, Vector2d.Dot(a, b), Mathd.EpsilonE12);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, -67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(592.3036192446763d, Vector2d.Dot(a, b), Mathd.EpsilonE12);

            a = Vector2d.PositiveInfinity;
            b = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector2d.Dot(a, b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector2d.Dot(a, b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, Vector2d.Dot(a, b));

            a = Vector2d.PositiveInfinity;
            b = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, Vector2d.Dot(a, b));
        }

        [Test]
        public void TestCrossMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.Zero;
            b = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.One;
            b = Vector2d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.One;
            b = -Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = -Vector2d.One;
            b = Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.Right;
            b = Vector2d.Up;

            TestUtilities.AssertThatDoublesAreEqual(1d, Vector2d.Cross(a, b));

            a = Vector2d.Up;
            b = Vector2d.Right;

            TestUtilities.AssertThatDoublesAreEqual(-1d, Vector2d.Cross(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(-72.00231159357315d, -67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(72.00231159357315d, -67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(-9758.403195563256d, Vector2d.Cross(a, b), Mathd.EpsilonE11);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);
            b = new Vector2d(-72.00231159357315d, 67.76451324678221d);

            TestUtilities.AssertThatDoublesAreEqual(9758.403195563256d, Vector2d.Cross(a, b), Mathd.EpsilonE11);

            a = Vector2d.PositiveInfinity;
            b = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.NegativeInfinity;
            b = Vector2d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.PositiveInfinity;
            b = Vector2d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = new Vector2d(double.PositiveInfinity, 0d);
            b = new Vector2d(0d, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector2d.Cross(a, b));

            a = new Vector2d(0d, double.PositiveInfinity);
            b = new Vector2d(double.PositiveInfinity, 0d);

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, Vector2d.Cross(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.MaxValue;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.MinValue;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.MinValue;
            b = Vector2d.MaxValue;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = Vector2d.MaxValue;
            b = Vector2d.MinValue;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Cross(a, b));

            a = new Vector2d(double.MaxValue, 0d);
            b = new Vector2d(0d, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector2d.Cross(a, b));

            a = new Vector2d(0d, double.MaxValue);
            b = new Vector2d(double.MaxValue, 0d);

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, Vector2d.Cross(a, b));
        }

        [Test]
        public void TestPolarAngleMethod()
        {
            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Zero.PolarAngle());
            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Right.PolarAngle());
            TestUtilities.AssertThatDoublesAreEqual(45d, Vector2d.One.PolarAngle());
            TestUtilities.AssertThatDoublesAreEqual(90d, Vector2d.Up.PolarAngle());
            TestUtilities.AssertThatDoublesAreEqual(180d, Vector2d.Left.PolarAngle());
            TestUtilities.AssertThatDoublesAreEqual(-135d, (-Vector2d.One).PolarAngle());
            TestUtilities.AssertThatDoublesAreEqual(-90d, Vector2d.Down.PolarAngle());
        }

        [Test]
        public void TestCardinalHeadingMethod()
        {
            //TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Zero.CardinalHeading());
            TestUtilities.AssertThatDoublesAreEqual(90d, Vector2d.Right.CardinalHeading());
            TestUtilities.AssertThatDoublesAreEqual(45d, Vector2d.One.CardinalHeading());
            TestUtilities.AssertThatDoublesAreEqual(0d, Vector2d.Up.CardinalHeading());
            TestUtilities.AssertThatDoublesAreEqual(270d, Vector2d.Left.CardinalHeading());
            TestUtilities.AssertThatDoublesAreEqual(225d, (-Vector2d.One).CardinalHeading());
            TestUtilities.AssertThatDoublesAreEqual(180d, Vector2d.Down.CardinalHeading());
        }

        [Test]
        public void TestRotateMethod()
        {
            var a = Vector2d.Right;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Right, a.Rotate(0d));
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One.Normalised, a.Rotate(45d), Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Up, a.Rotate(90d), Mathd.EpsilonE16);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1d, 1d).Normalised, a.Rotate(135d), Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Left, a.Rotate(180d), Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(-Vector2d.One.Normalised, a.Rotate(225d), Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Down, a.Rotate(270d), Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d, -1d).Normalised, a.Rotate(315d), Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Right, a.Rotate(360d), Mathd.EpsilonE15);
        }

        [Test]
        public void TestAngleMethod()
        {
            var a = Vector2d.One;
            var b = new Vector2d(-1d, 1d);

            TestUtilities.AssertThatDoublesAreEqual(90d, Vector2d.Angle(a, b));

            a = Vector2d.One;
            b = -Vector2d.One;

            TestUtilities.AssertThatDoublesAreEqual(180d, Vector2d.Angle(a, b), Mathd.EpsilonE5);

            a = Vector2d.One;
            b = new Vector2d(1d, -1d);

            TestUtilities.AssertThatDoublesAreEqual(90d, Vector2d.Angle(a, b));
        }

        [Test]
        public void TestNormaliseMethod()
        {
            var a = Vector2d.Zero;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector2d.One;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = Vector2d.Right;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector2d.Up;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = -Vector2d.One;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = Vector2d.Left;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector2d.Down;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector2d(72.00231159357315d, 67.76451324678221d);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector2d(72.00231159357315d, 0d);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector2d(0d, 67.76451324678221d);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector2d(72.00231159357315d, -67.76451324678221d);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector2d(-72.00231159357315d, 67.76451324678221d);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector2d(-72.00231159357315d, -67.76451324678221d);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector2d(0d, -67.76451324678221d);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector2d(-72.00231159357315d, 0d);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector2d.MaxValue;

            // vectors of infinite length are zero length when normalised

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector2d.MinValue;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = new Vector2d(double.MinValue, double.MaxValue);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = new Vector2d(double.MaxValue, double.MinValue);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector2d.PositiveInfinity;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector2d.NegativeInfinity;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = new Vector2d(double.PositiveInfinity, double.NegativeInfinity);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = new Vector2d(double.NegativeInfinity, double.PositiveInfinity);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);
        }

        [Test]
        public void TestNormalizeMethod()
        {
            // we know normalize just expands to normalise
            var a = Vector2d.One;

            a.Normalize();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);
        }

        [Test]
        public void TestLerpMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.One;

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, Vector2d.Lerp(a, b, 0d));
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One * 0.25d, Vector2d.Lerp(a, b, 0.25d));
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One * 0.5d, Vector2d.Lerp(a, b, 0.5d));
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One * 0.75d, Vector2d.Lerp(a, b, 0.75d));
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, Vector2d.Lerp(a, b, 1d));
        }

        [Test]
        public void TestLiesBetweenMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.One;

            Assert.IsTrue(Vector2d.LiesBetween(Vector2d.Zero, a, b));
            Assert.IsTrue(Vector2d.LiesBetween(Vector2d.One * 0.25d, a, b));
            Assert.IsTrue(Vector2d.LiesBetween(Vector2d.One * 0.5d, a, b));
            Assert.IsTrue(Vector2d.LiesBetween(Vector2d.One * 0.75d, a, b));
            Assert.IsTrue(Vector2d.LiesBetween(Vector2d.One, a, b));

            Assert.IsFalse(Vector2d.LiesBetween(Vector2d.Up, a, b));
            Assert.IsFalse(Vector2d.LiesBetween(Vector2d.Right, a, b));
            Assert.IsFalse(Vector2d.LiesBetween(Vector2d.Down, a, b));
            Assert.IsFalse(Vector2d.LiesBetween(Vector2d.Left, a, b));
        }

        [Test]
        public void TestTimeBetweenMethod()
        {
            var a = Vector2d.Zero;
            var b = Vector2d.One;

            Assert.AreEqual(0d, Vector2d.TimeBetween(Vector2d.Zero, a, b));
            Assert.AreEqual(0.25d, Vector2d.TimeBetween(Vector2d.One * 0.25d, a, b));
            Assert.AreEqual(0.5d, Vector2d.TimeBetween(Vector2d.One * 0.5d, a, b));
            Assert.AreEqual(0.75d, Vector2d.TimeBetween(Vector2d.One * 0.75d, a, b));
            Assert.AreEqual(1d, Vector2d.TimeBetween(Vector2d.One, a, b));

            Assert.Throws<ArithmeticException>(() => { Vector2d.TimeBetween(Vector2d.Up, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector2d.TimeBetween(Vector2d.Right, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector2d.TimeBetween(Vector2d.Down, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector2d.TimeBetween(Vector2d.Left, a, b); });
        }

        [Test]
        public void TestClockwiseMethod()
        {
            var a = new Vector2d(0f, 0f);
            var b = new Vector2d(0f, 1f);
            var c = new Vector2d(1f, 1f);
            var d = new Vector2d(1f, 0f);

            var points = new [] { a, b, c, d };

            Assert.IsTrue(Vector2d.Clockwise(points));

            a = new Vector2d(-1f, -1f);
            b = new Vector2d(-1f, 1f);
            c = new Vector2d(1f, 1f);
            d = new Vector2d(1f, -1f);

            points = new[] { a, b, c, d };

            Assert.IsTrue(Vector2d.Clockwise(points));

            a = new Vector2d(0f, 0f);
            b = new Vector2d(1f, 0f);
            c = new Vector2d(1f, 1f);
            d = new Vector2d(0f, 1f);

            points = new[] { a, b, c, d };

            Assert.IsFalse(Vector2d.Clockwise(points));

            a = new Vector2d(-1f, -1f);
            b = new Vector2d(1f, -1f);
            c = new Vector2d(1f, 1f);
            d = new Vector2d(-1f, 1f);

            points = new[] { a, b, c, d };

            Assert.IsFalse(Vector2d.Clockwise(points));

            a = new Vector2d(0.8f, 2.5f);
            b = new Vector2d(0.8f, 0.8f);
            c = new Vector2d(1.5f, 0.8f);
            d = new Vector2d(1.5f, 2.5f);

            points = new[] { a, b, c, d };

            Assert.IsFalse(Vector2d.Clockwise(points));

            a = new Vector2d(0f, 0f);
            b = new Vector2d(0f, 1f);
            c = new Vector2d(1f, 1f);
            d = new Vector2d(1f, 0f);
            var e = new Vector2d(0f, 0f);
            
            points = new[] { a, b, c, d, e};

            Assert.IsTrue(Vector2d.Clockwise(points));
        }
    }
}
