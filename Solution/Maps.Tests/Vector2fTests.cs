using System;
using NUnit.Framework;

namespace Maps.Tests
{
    /// <summary>
    /// Series of tests for the Vector2f class, appearing in order of
    /// members in class
    /// </summary>
    [TestFixture]
    internal sealed class Vector2fTests
    {
        [Test]
        public void TestConstructor()
        {
            var a = new Vector2f(0f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(0f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(0f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[1]);

            a = new Vector2f(1f, 1f);

            TestUtilities.AssertThatSinglesAreEqual(1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(1f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(1f, a[1]);

            a = new Vector2f(1f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[1]);

            a = new Vector2f(0f, 1f);

            TestUtilities.AssertThatSinglesAreEqual(0f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(1f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(0f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(1f, a[1]);

            a = new Vector2f(-1f, -1f);

            TestUtilities.AssertThatSinglesAreEqual(-1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(-1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a[1]);

            a = new Vector2f(72.00231f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(67.76451f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(67.76451f, a[1]);

            a = new Vector2f(-72.00231f, -67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(-67.76451f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(-67.76451f, a[1]);

            a = new Vector2f(float.MaxValue, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a.y);

            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a[1]);

            a = new Vector2f(float.MinValue, float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a.y);

            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a[1]);

            a = new Vector2f(float.PositiveInfinity, float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.y);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a[1]);

            a = new Vector2f(float.NegativeInfinity, float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a.y);

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a[1]);
        }

        [Test]
        public void TestSingleParameterConstructor()
        {
            var a = new Vector2f(0f);

            TestUtilities.AssertThatSinglesAreEqual(0f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(0f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[1]);

            a = new Vector2f(1f);

            TestUtilities.AssertThatSinglesAreEqual(1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(1f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(1f, a[1]);

            a = new Vector2f(-1f);

            TestUtilities.AssertThatSinglesAreEqual(-1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(-1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a[1]);

            a = new Vector2f(72.00231f);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a[1]);

            a = new Vector2f(-72.00231f);

            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a.y);

            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a[1]);

            a = new Vector2f(float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a.y);

            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a[1]);

            a = new Vector2f(float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a.y);

            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a[1]);

            a = new Vector2f(float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.y);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a[1]);

            a = new Vector2f(float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a.y);

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a[1]);
        }

        [Test]
        public void TestIndexAccessor()
        {
            var a = Vector2f.Zero;

            Assert.Throws<IndexOutOfRangeException>(() => { var f = a[2]; });
            Assert.Throws<IndexOutOfRangeException>(() => { var f = a[-1]; });
        }

        [Test]
        public void TestMinComponentProperty()
        {
            var a = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MinComponent);

            a = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.MinComponent);

            a = Vector2f.Right;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MinComponent);

            a = Vector2f.Up;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MinComponent);

            a = Vector2f.Left;

            TestUtilities.AssertThatSinglesAreEqual(-1f, a.MinComponent);

            a = Vector2f.Down;

            TestUtilities.AssertThatSinglesAreEqual(-1f, a.MinComponent);
        }

        [Test]
        public void TestMaxComponentProperty()
        {
            var a = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MaxComponent);

            a = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.MaxComponent);

            a = Vector2f.Right;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.MaxComponent);

            a = Vector2f.Up;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.MaxComponent);

            a = Vector2f.Left;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MaxComponent);

            a = Vector2f.Down;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MaxComponent);
        }

        [Test]
        public void TestMagnitudeProperty()
        {
            var a = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(1.414213f, a.Magnitude, Mathf.EpsilonE6);

            a = Vector2f.Right;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector2f.Up;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = -Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(1.414213f, a.Magnitude, Mathf.EpsilonE6);

            a = new Vector2f(-1f, 1f);

            TestUtilities.AssertThatSinglesAreEqual(1.414213f, a.Magnitude, Mathf.EpsilonE6);

            a = Vector2f.Left;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(1f, -1f);

            TestUtilities.AssertThatSinglesAreEqual(1.414213f, a.Magnitude, Mathf.EpsilonE6);

            a = Vector2f.Down;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(72.00231f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(98.87548f, a.Magnitude);

            a = new Vector2f(72.00231f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a.Magnitude);

            a = new Vector2f(0f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(67.76451f, a.Magnitude);

            a = new Vector2f(72.00231f, -67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(98.87548f, a.Magnitude);

            a = new Vector2f(-72.00231f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(98.87548f, a.Magnitude);

            a = new Vector2f(-72.00231f, -67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(98.87548f, a.Magnitude);

            a = Vector2f.MaxValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = Vector2f.MinValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector2f(float.MinValue, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector2f(float.MaxValue, float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector2f(float.PositiveInfinity, float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector2f(float.NegativeInfinity, float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);
        }

        [Test]
        public void TestSqrMagnitudeProperty()
        {
            var a = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.SqrMagnitude);

            a = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(2f, a.SqrMagnitude);

            a = Vector2f.Right;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.SqrMagnitude);

            a = Vector2f.Up;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.SqrMagnitude);

            a = -Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(2f, a.SqrMagnitude);

            a = new Vector2f(-1f, 1f);

            TestUtilities.AssertThatSinglesAreEqual(2f, a.SqrMagnitude);

            a = Vector2f.Left;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.SqrMagnitude);

            a = new Vector2f(1f, -1f);

            TestUtilities.AssertThatSinglesAreEqual(2f, a.SqrMagnitude);

            a = Vector2f.Down;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.SqrMagnitude);

            a = new Vector2f(72.00231f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(9776.362f, a.SqrMagnitude, Mathf.EpsilonE3);

            a = new Vector2f(72.00231f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(5184.333f, a.SqrMagnitude);

            a = new Vector2f(0f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(4592.029f, a.SqrMagnitude);

            a = new Vector2f(72.00231f, -67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(9776.362f, a.SqrMagnitude, Mathf.EpsilonE3);

            a = new Vector2f(-72.00231f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(9776.362f, a.SqrMagnitude, Mathf.EpsilonE3);

            a = new Vector2f(-72.00231f, -67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(9776.362f, a.SqrMagnitude, Mathf.EpsilonE3);

            a = new Vector2f(0f, -67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(4592.029f, a.SqrMagnitude);

            a = new Vector2f(-72.00231f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(5184.333f, a.SqrMagnitude);

            a = Vector2f.MaxValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = Vector2f.MinValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector2f(float.MinValue, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector2f(float.MaxValue, float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector2f(float.PositiveInfinity, float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector2f(float.NegativeInfinity, float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);
        }

        [Test]
        public void TestXySwizzleProperty()
        {
            var a = new Vector2f(1f, -1f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(a.x, a.y), a.xy);
        }

        [Test]
        public void TestYxSwizzleProperty()
        {
            var a = new Vector2f(1f, -1f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(a.y, a.x), a.yx);
        }

        [Test]
        public void TestNormalisedProperty()
        {
            var a = Vector2f.Zero;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector2f.One;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = Vector2f.Right;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector2f.Up;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = -Vector2f.One;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = Vector2f.Left;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector2f.Down;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(72.00231f, 67.76451f);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(72.00231f, 0f);

            a = a.Normalised; 

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(0f, 67.76451f);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(72.00231f, -67.76451f);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(-72.00231f, 67.76451f);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(-72.00231f, -67.76451f);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(0f, -67.76451f);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(-72.00231f, 0f);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector2f.MaxValue;

            // vectors of infinite length are zero length when normalised

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector2f.MinValue;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = new Vector2f(float.MinValue, float.MaxValue);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = new Vector2f(float.MaxValue, float.MinValue);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector2f.PositiveInfinity;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector2f.NegativeInfinity;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = new Vector2f(float.PositiveInfinity, float.NegativeInfinity);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = new Vector2f(float.NegativeInfinity, float.PositiveInfinity);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);
        }

        [Test]
        public void TestPerpendicularProperty()
        {
            var a = Vector2f.One;
            var b = new Vector2f(-1f, 1f);

            TestUtilities.AssertThatVector2d32sAreEqual(b, a.Perpendicular);

            a = b;
            b = -Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(b, a.Perpendicular);

            a = b;
            b = new Vector2f(1f, -1f);

            TestUtilities.AssertThatVector2d32sAreEqual(b, a.Perpendicular);

            a = b;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(b, a.Perpendicular);

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(-67.76451f, 72.00231f);

            TestUtilities.AssertThatVector2d32sAreEqual(b, a.Perpendicular);

            a = new Vector2f(72.00231f, -67.76451f);
            b = new Vector2f(67.76451f, 72.00231f);

            TestUtilities.AssertThatVector2d32sAreEqual(b, a.Perpendicular);
        }

        [Test]
        public void TestUnaryNegationOperator()
        {
            var a = Vector2f.Zero;
            var aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = Vector2f.Zero;

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = Vector2f.Right;

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = Vector2f.Up;

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = -Vector2f.One;

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(-1f, 1f);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = Vector2f.Left;

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(1f, -1f);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = Vector2f.Down;

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(72.00231f, 67.76451f);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(72.00231f, 0f);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(0f, 67.76451f);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(72.00231f, -67.76451f);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(-72.00231f, 67.76451f);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(-72.00231f, -67.76451f);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(0f, -67.76451f);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(-72.00231f, 0f);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = Vector2f.MaxValue;

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = Vector2f.MinValue;

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(float.MinValue, float.MaxValue);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(float.MaxValue, float.MinValue);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = Vector2f.PositiveInfinity;

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = Vector2f.NegativeInfinity;

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(float.PositiveInfinity, float.NegativeInfinity);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);

            a = new Vector2f(float.NegativeInfinity, float.PositiveInfinity);

            aNegative = -a;

            TestUtilities.AssertThatSinglesAreEqual(-a.x, aNegative.x);
            TestUtilities.AssertThatSinglesAreEqual(-a.y, aNegative.y);
        }

        [Test]
        public void TestSubtractionOperator()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;
            var c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.Zero;
            b = Vector2f.Zero;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.Zero;
            b = Vector2f.One;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, c);

            a = Vector2f.One;
            b = Vector2f.One;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.One;
            b = Vector2f.Right;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Up, c);

            a = Vector2f.One;
            b = Vector2f.Up;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Right, c);

            a = Vector2f.Zero;
            b = Vector2f.Right;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Left, c);

            a = Vector2f.Zero;
            b = Vector2f.Up;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Down, c);

            a = new Vector2f(72.00231f, 67.76451f);
            b = Vector2f.Zero;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72.00231f, 67.76451f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, 67.76451f);
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = new Vector2f(72.00231f, 67.76451f);
            b = Vector2f.One;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(71.00231f, 66.76451f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(10f, 10f);
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(62.00231f, 57.76451f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(0.00231f, 0.76451f);
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72f, 67f), c);

            // infinity - infinity = undetermined
            a = Vector2f.PositiveInfinity;
            b = Vector2f.PositiveInfinity;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            // -infinity - -infinity = undetermined
            a = Vector2f.NegativeInfinity;
            b = Vector2f.NegativeInfinity;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            // -infinity - infinity = -infinity
            a = Vector2f.NegativeInfinity;
            b = Vector2f.PositiveInfinity;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            // infinity - -infinity = infinity
            a = Vector2f.PositiveInfinity;
            b = Vector2f.NegativeInfinity;            
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            // max - max = 0
            a = Vector2f.MaxValue;
            b = Vector2f.MaxValue;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            // min - min = 0
            a = Vector2f.MinValue;
            b = Vector2f.MinValue;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            // max - min = infinity
            a = Vector2f.MaxValue;
            b = Vector2f.MinValue;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            // min - max = -infinity
            a = Vector2f.MinValue;
            b = Vector2f.MaxValue;
            c = a - b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);
        }

        [Test]
        public void TestAdditionOperator()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;
            var c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.One;
            b = Vector2f.One;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(2f, 2f), c);

            a = Vector2f.Zero;
            b = Vector2f.Zero;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.Zero;
            b = Vector2f.Zero;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.One;
            b = Vector2f.Right;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(2f, 1f), c);

            a = Vector2f.One;
            b = Vector2f.Up;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(1f, 2f), c);

            a = Vector2f.Zero;
            b = Vector2f.Right;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Right, c);

            a = Vector2f.Zero;
            b = Vector2f.Up;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Up, c);

            a = new Vector2f(72.00231f, 67.76451f);
            b = Vector2f.Zero;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72.00231f, 67.76451f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, 67.76451f);
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(144.00462f, 135.52902f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            b = Vector2f.One;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(73.00231f, 68.76451f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(10f, 10f);
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(82.00231f, 77.76451f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(0.00231f, 0.76451f);
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72.00462f, 68.52902f), c);

            // infinity + infinity = infinity
            a = Vector2f.PositiveInfinity;
            b = Vector2f.PositiveInfinity;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            // -infinity + -infinity = -infinity
            a = Vector2f.NegativeInfinity;
            b = Vector2f.NegativeInfinity;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            // -infinity + infinity = NaN
            a = Vector2f.NegativeInfinity;
            b = Vector2f.PositiveInfinity;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            // infinity + -infinity = infinity
            a = Vector2f.PositiveInfinity;
            b = Vector2f.NegativeInfinity;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            // max + max = infinity
            a = Vector2f.MaxValue;
            b = Vector2f.MaxValue;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            // min + min = -infinity
            a = Vector2f.MinValue;
            b = Vector2f.MinValue;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            // max + min = 0
            a = Vector2f.MaxValue;
            b = Vector2f.MinValue;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            // min + max = 0
            a = Vector2f.MinValue;
            b = Vector2f.MaxValue;
            c = a + b;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);
        }

        [Test]
        public void TestMultiplicationOperator()
        {
            var a = Vector2f.Zero;
            var c = a * 0f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            c = 0f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.Zero;
            c = a * 1f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            c = 1f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.Zero;
            c = a * -1f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            c = -1f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.Zero;
            c = a * 0f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            c = 0f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.Zero;
            c = a * 1f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            c = 1f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.One;
            c = a * -1f;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, c);

            c = -1f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, c);

            a = Vector2f.One;
            c = a * 2f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(2f, 2f), c);

            c = 2f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(2f, 2f), c);

            a = Vector2f.One;
            c = a * 0.5f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0.5f, 0.5f), c);

            c = 0.5f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0.5f, 0.5f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            c = a * 0f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            c = 0f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = new Vector2f(72.00231f, 67.76451f);
            c = a * 1f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72.00231f, 67.76451f), c);

            c = 1f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72.00231f, 67.76451f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            c = a * -1f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(-72.00231f, -67.76451f), c);

            c = -1f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(-72.00231f, -67.76451f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            c = a * 2f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(144.00462f, 135.52902f), c);

            c = 2f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(144.00462f, 135.52902f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            c = a * 0.5f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(36.001155f, 33.882255f), c);

            c = 0.5f * a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(36.001155f, 33.882255f), c);

            // infinity * infinity = infinity
            a = Vector2f.PositiveInfinity;
            c = a * float.PositiveInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            c = float.PositiveInfinity * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            // infinity * -infinity = -infinity
            a = Vector2f.PositiveInfinity;
            c = a * float.NegativeInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            c = float.NegativeInfinity * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            // -infinity * infinity = -infinity
            a = Vector2f.NegativeInfinity;
            c = a * float.PositiveInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            c = float.PositiveInfinity * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            // -infinity * -infinity = infinity
            a = Vector2f.NegativeInfinity;
            c = a * float.NegativeInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            c = float.NegativeInfinity * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            // max * max = infinity
            a = Vector2f.MaxValue;
            c = a * float.MaxValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            c = float.MaxValue * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            // max * min = -infinity
            a = Vector2f.MaxValue;
            c = a * float.MinValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            c = float.MinValue * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            // min * max = -infinity
            a = Vector2f.MinValue;
            c = a * float.MaxValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            c = float.MaxValue * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            // min * min = infinity
            a = Vector2f.MinValue;
            c = a * float.MinValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            c = float.MinValue * a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);
        }

        [Test]
        public void TestDivisionOperator()
        {
            var a = Vector2f.Zero;
            var c = a / 0f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            c = 0f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            a = Vector2f.Zero;
            c = a / 1f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            c = 1f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            a = Vector2f.Zero;
            c = a / -1f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            c = -1f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, c);

            a = Vector2f.Zero;
            c = a / 0f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            c = 0f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = Vector2f.One;
            c = a / 1f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, c);

            c = 1f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, c);

            a = Vector2f.One;
            c = a / -1f;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, c);

            c = -1f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, c);

            a = Vector2f.One;
            c = a / 2f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0.5f, 0.5f), c);

            c = 2f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(2f, 2f), c);

            a = Vector2f.One;
            c = a / 0.5f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(2f, 2f), c);

            c = 0.5f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0.5f, 0.5f), c);

            a = new Vector2f(72.00231f, 67.76451f);
            c = a / 0f;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, c);

            c = 0f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, c);

            a = new Vector2f(72.00231f, 67.76451f);
            c = a / 1f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72.00231f, 67.76451f), c);

            c = 1f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0.01388844f, 0.01475699f), c, Mathf.EpsilonE8);

            a = new Vector2f(72.00231f, 67.76451f);
            c = a / -1f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(-72.00231f, -67.76451f), c);

            c = -1f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(-0.01388844f, -0.01475699f), c, Mathf.EpsilonE8);

            a = new Vector2f(72.00231f, 67.76451f);
            c = a / 2f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(36.001155f, 33.882255f), c);

            c = 2f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0.0277768f, 0.0295139f), c, Mathf.EpsilonE7);

            a = new Vector2f(72.00231f, 67.76451f);
            c = a / 0.5f;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(144.00462f, 135.52902f), c);

            c = 0.5f / a;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0.0069442f, 0.0073784f), c, Mathf.EpsilonE7);

            // infinity / infinity = NaN
            a = Vector2f.PositiveInfinity;
            c = a / float.PositiveInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            c = float.PositiveInfinity / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            // infinity / -infinity = NaN
            a = Vector2f.PositiveInfinity;
            c = a / float.NegativeInfinity;
            c = float.NegativeInfinity / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            // -infinity / infinity = NaN
            a = Vector2f.NegativeInfinity;
            c = a / float.PositiveInfinity;
            c = float.PositiveInfinity / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            // -infinity / -infinity = NaN
            a = Vector2f.NegativeInfinity;
            c = a / float.NegativeInfinity;
            c = float.NegativeInfinity / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NaN, c);

            // max / max = 1
            a = Vector2f.MaxValue;
            c = a / float.MaxValue;
            c = float.MaxValue / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, c);

            // max / min = -1
            a = Vector2f.MaxValue;
            c = a / float.MinValue;
            c = float.MinValue / a;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, c);

            // min / max = -infinity
            a = Vector2f.MinValue;
            c = a / float.MaxValue;
            c = float.MaxValue / a;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, c);

            // min / min = infinity
            a = Vector2f.MinValue;
            c = a / float.MinValue;
            c = float.MinValue / a;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, c);
        }

        [Test]
        public void TestEqualityOperator()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;
            
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.Zero;
            b = Vector2f.Zero;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.Right;
            b = Vector2f.Right;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.Up;
            b = Vector2f.Up;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = -Vector2f.One;
            b = -Vector2f.One;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.Left;
            b = Vector2f.Left;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.Down;
            b = Vector2f.Down;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, 67.76451f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector2f(-72.00231f, -67.76451f);
            b = new Vector2f(-72.00231f, -67.76451f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector2f(-72.00231f, 67.76451f);
            b = new Vector2f(-72.00231f, 67.76451f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector2f(72.00231f, -67.76451f);
            b = new Vector2f(72.00231f, -67.76451f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.PositiveInfinity;
            b = Vector2f.PositiveInfinity;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.NegativeInfinity;
            b = Vector2f.NegativeInfinity;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.MaxValue;
            b = Vector2f.MaxValue;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.MinValue;
            b = Vector2f.MinValue;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [Test]
        public void TestInequalityOperator()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.Zero;
            b = Vector2f.Zero;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector2f.Right;
            b = Vector2f.Up;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector2f.Up;
            b = Vector2f.Right;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = new Vector2f(-1f, 1f);
            b = new Vector2f(1f, -1f);

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector2f.Left;
            b = Vector2f.Down;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector2f.Down;
            b = Vector2f.Left;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, 67.76451f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector2f(-72.00231f, -67.76451f);
            b = new Vector2f(72.00231f, 67.76451f);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = new Vector2f(-72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, -67.76451f);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = new Vector2f(72.00231f, -67.76451f);
            b = new Vector2f(-72.00231f, 67.76451f);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector2f.PositiveInfinity;
            b = Vector2f.NegativeInfinity;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector2f.NegativeInfinity;
            b = Vector2f.PositiveInfinity;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector2f.MaxValue;
            b = Vector2f.MinValue;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector2f.MinValue;
            b = Vector2f.MaxValue;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);
        }

        [Test]
        public void TestOneProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(1f, 1f), Vector2f.One);
        }

        [Test]
        public void TestZeroProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0f, 0f), Vector2f.Zero);
        }

        [Test]
        public void TestUpProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0f, 1f), Vector2f.Up);
        }

        [Test]
        public void TestDownProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0f, -1f), Vector2f.Down);
        }

        [Test]
        public void TestRightProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(1f, 0f), Vector2f.Right);
        }

        [Test]
        public void TestLeftProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(-1f, 0f), Vector2f.Left);
        }

        [Test]
        public void TestMaxValueProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(float.MaxValue, float.MaxValue), Vector2f.MaxValue);
        }

        [Test]
        public void TestMinValueProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(float.MinValue, float.MinValue), Vector2f.MinValue);
        }

        [Test]
        public void TestPositiveInfinityProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(float.PositiveInfinity, float.PositiveInfinity), Vector2f.PositiveInfinity);
        }

        [Test]
        public void TestNegativeInfinityProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(float.NegativeInfinity, float.NegativeInfinity), Vector2f.NegativeInfinity);
        }

        [Test]
        public void TestNaNProperty()
        {
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(float.NaN, float.NaN), Vector2f.NaN);
        }

        [Test]
        public void TestIsNaNMethod()
        {
            var a = Vector2f.One;

            Assert.IsFalse(Vector2f.IsNaN(a));

            a = new Vector2f(72.00231f, 67.76451f);

            Assert.IsFalse(Vector2f.IsNaN(a));

            a = Vector2f.NaN;

            Assert.IsTrue(Vector2f.IsNaN(a));
        }

        [Test]
        public void TestComponentMultiplyMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.ComponentMultiply(a, b));

            a = Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.ComponentMultiply(a, b));

            a = Vector2f.Zero;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.ComponentMultiply(a, b));

            a = Vector2f.One;
            b = -Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, Vector2f.ComponentMultiply(a, b));

            a = Vector2f.One;
            b = new Vector2f(-1f, 1f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(-1f, 1f), Vector2f.ComponentMultiply(a, b));

            a = Vector2f.One;
            b = new Vector2f(1f, -1f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(1f, -1f), Vector2f.ComponentMultiply(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(-1f, 1f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(-72.00231f, 67.76451f), Vector2f.ComponentMultiply(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(1f, -1f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72.00231f, -67.76451f), Vector2f.ComponentMultiply(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(2f, 2f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(144.00462f, 135.52902f), Vector2f.ComponentMultiply(a, b));
        }

        [Test]
        public void TestPowMethod()
        {
            var a = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Pow(a, 0f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Pow(a, 1f));

            a = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Pow(a, 0f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Pow(a, 1f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Pow(a, 2f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Pow(a, 3f));

            a = new Vector2f(72.00231f, 67.76451f);

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Pow(a, 0f));
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72.00231f, 67.76451f), Vector2f.Pow(a, 1f));
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(5184.333f, 4592.029f), Vector2f.Pow(a, 2f));
        }

        [Test]
        public void TestDistanceMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Distance(a, b));

            a = Vector2f.Zero;
            b = Vector2f.Up;

            TestUtilities.AssertThatSinglesAreEqual(1f, Vector2f.Distance(a, b));

            a = Vector2f.Zero;
            b = Vector2f.Right;

            TestUtilities.AssertThatSinglesAreEqual(1f, Vector2f.Distance(a, b));

            a = Vector2f.Zero;
            b = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(1.414213f, Vector2f.Distance(a, b), Mathf.EpsilonE6);

            a = Vector2f.Zero;
            b = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Distance(a, b));

            a = Vector2f.Left;
            b = Vector2f.Right;

            TestUtilities.AssertThatSinglesAreEqual(2f, Vector2f.Distance(a, b));

            a = Vector2f.Down;
            b = Vector2f.Up;

            TestUtilities.AssertThatSinglesAreEqual(2f, Vector2f.Distance(a, b));

            a = -Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(2.828427f, Vector2f.Distance(a, b));

            a = new Vector2f(72.00231f, 0f);
            b = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, Vector2f.Distance(a, b));

            a = Vector2f.Zero;
            b = new Vector2f(72.00231f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, Vector2f.Distance(a, b));

            a = new Vector2f(0f, 67.76451f);
            b = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(67.76451f, Vector2f.Distance(a, b));

            a = Vector2f.Zero;
            b = new Vector2f(0f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(67.76451f, Vector2f.Distance(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(98.87548f, Vector2f.Distance(a, b));

            a = Vector2f.Zero;
            b = new Vector2f(72.00231f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(98.87548f, Vector2f.Distance(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(67.76451f, Vector2f.Distance(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(0f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, Vector2f.Distance(a, b));
            
            a = Vector2f.PositiveInfinity;
            b = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Distance(a, b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Distance(a, b));

            a = Vector2f.PositiveInfinity;
            b = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector2f.Distance(a, b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector2f.Distance(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.MaxValue;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Distance(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.MinValue;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Distance(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.MinValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector2f.Distance(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.MaxValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector2f.Distance(a, b));
        }

        [Test]
        public void TestComponentDistanceMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.ComponentDistance(a, b));

            a = Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.ComponentDistance(a, b));

            a = Vector2f.One;
            b = -Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(2f, 2f), Vector2f.ComponentDistance(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72.00231f, 67.76451f), Vector2f.ComponentDistance(a, b));

            a = Vector2f.Zero;
            b = new Vector2f(72.00231f, 67.76451f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(72.00231f, 67.76451f), Vector2f.ComponentDistance(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(-72.00231f, -67.76451f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(144.00462f, 135.52902f), Vector2f.ComponentDistance(a, b));
        }

        [Test]
        public void TestMidpointMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(0.5f, 0.5f), Vector2f.Midpoint(a, b));

            a = Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Midpoint(a, b));

            a = Vector2f.One;
            b = -Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Midpoint(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(36.001155f, 33.882255f), Vector2f.Midpoint(a, b));

            a = Vector2f.Zero;
            b = new Vector2f(72.00231f, 67.76451f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(36.001155f, 33.882255f), Vector2f.Midpoint(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(-72.00231f, -67.76451f);

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Midpoint(a, b));
        }

        [Test]
        public void TestScaleMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Scale(a, b));

            a = Vector2f.Zero;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Scale(a, b));

            a = Vector2f.One;
            b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Scale(a, b));

            a = Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Scale(a, b));

            a = Vector2f.One;
            b = -Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, Vector2f.Scale(a, b));

            a = Vector2f.One;
            b = new Vector2f(2f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(2f), Vector2f.Scale(a, b));

            a = new Vector2f(2f);
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(2f), Vector2f.Scale(a, b));

            a = Vector2f.One;
            b = new Vector2f(-2f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(-2f), Vector2f.Scale(a, b));

            a = new Vector2f(-2f);
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(-2f), Vector2f.Scale(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.MaxValue, Vector2f.Scale(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.MinValue, Vector2f.Scale(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.MaxValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, Vector2f.Scale(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.MinValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, Vector2f.Scale(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.MaxValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, Vector2f.Scale(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.MinValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, Vector2f.Scale(a, b));
        }

        [Test]
        public void TestMaxMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Max(a, b));

            a = Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Max(a, b));

            a = -Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Max(a, b));

            a = Vector2f.One;
            b = -Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Max(a, b));

            a = Vector2f.Zero;
            b = -Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Max(a, b));

            a = -Vector2f.One;
            b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Max(a, b));

            a = Vector2f.One;
            b = Vector2f.One * 2;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One * 2, Vector2f.Max(a, b));

            a = Vector2f.One * 2;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One * 2, Vector2f.Max(a, b));

            a = Vector2f.Up;
            b = Vector2f.Right;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Max(a, b));

            a = Vector2f.Right;
            b = Vector2f.Up;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Max(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(a, Vector2f.Max(a, b));

            a = Vector2f.Zero;
            b = new Vector2f(72.00231f, 67.76451f);            

            TestUtilities.AssertThatVector2d32sAreEqual(b, Vector2f.Max(a, b));

            a = Vector2f.PositiveInfinity;
            b = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, Vector2f.Max(a, b));

            a = Vector2f.PositiveInfinity;
            b = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, Vector2f.Max(a, b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, Vector2f.Max(a, b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, Vector2f.Max(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.MaxValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.MaxValue, Vector2f.Max(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.MinValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.MaxValue, Vector2f.Max(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.MaxValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.MaxValue, Vector2f.Max(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.MinValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.MinValue, Vector2f.Max(a, b));
        }

        [Test]
        public void TestMinMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Min(a, b));

            a = Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Min(a, b));

            a = -Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, Vector2f.Min(a, b));

            a = Vector2f.One;
            b = -Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, Vector2f.Min(a, b));

            a = Vector2f.Zero;
            b = -Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, Vector2f.Min(a, b));

            a = -Vector2f.One;
            b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One, Vector2f.Min(a, b));

            a = Vector2f.One;
            b = Vector2f.One * 2;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Min(a, b));

            a = Vector2f.One * 2;
            b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Min(a, b));

            a = Vector2f.Up;
            b = Vector2f.Right;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Min(a, b));

            a = Vector2f.Right;
            b = Vector2f.Up;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Min(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = Vector2f.Zero;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Min(a, b));

            a = Vector2f.Zero;
            b = new Vector2f(72.00231f, 67.76451f);

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Min(a, b));

            a = Vector2f.PositiveInfinity;
            b = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.PositiveInfinity, Vector2f.Min(a, b));

            a = Vector2f.PositiveInfinity;
            b = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, Vector2f.Min(a, b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, Vector2f.Min(a, b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.NegativeInfinity, Vector2f.Min(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.MaxValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.MaxValue, Vector2f.Min(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.MinValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.MinValue, Vector2f.Min(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.MaxValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.MinValue, Vector2f.Min(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.MinValue;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.MinValue, Vector2f.Min(a, b));
        }

        [Test]
        public void TestToStringMethod()
        {
            var a = Vector2f.Zero;
            Assert.AreEqual(@"[0]f,[0]f", a.ToString());

            a = Vector2f.One;
            Assert.AreEqual(@"[1]f,[1]f", a.ToString());

            a = new Vector2f(72.00231f, 67.76451f);
            Assert.AreEqual(@"[72.00231]f,[67.76451]f", a.ToString());

            a = Vector2f.PositiveInfinity;
            Assert.AreEqual("[\u221E]f,[\u221E]f", a.ToString());

            a = Vector2f.NegativeInfinity;
            Assert.AreEqual("[-\u221E]f,[-\u221E]f", a.ToString());
        }

        [Test]
        public void TestEqualsMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;

            Assert.IsTrue(a.Equals(b));

            a = Vector2f.Zero;
            b = Vector2f.Zero;

            Assert.IsTrue(a.Equals(b));

            a = Vector2f.Right;
            b = Vector2f.Right;

            Assert.IsTrue(a.Equals(b));

            a = Vector2f.Up;
            b = Vector2f.Up;

            Assert.IsTrue(a.Equals(b));

            a = -Vector2f.One;
            b = -Vector2f.One;

            Assert.IsTrue(a.Equals(b));

            a = Vector2f.Left;
            b = Vector2f.Left;

            Assert.IsTrue(a.Equals(b));

            a = Vector2f.Down;
            b = Vector2f.Down;

            Assert.IsTrue(a.Equals(b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, 67.76451f);

            Assert.IsTrue(a.Equals(b));

            // test rounding up
            b = new Vector2f(72.002309f, 67.764509f);

            Assert.IsTrue(a.Equals(b));

            // test rounding down
            b = new Vector2f(72.002311f, 67.764511f);

            Assert.IsTrue(a.Equals(b));

            a = Vector2f.PositiveInfinity;
            b = Vector2f.PositiveInfinity;

            Assert.IsTrue(a.Equals(b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.NegativeInfinity;

            Assert.IsTrue(a.Equals(b));

            a = Vector2f.MaxValue;
            b = Vector2f.MaxValue;

            Assert.IsTrue(a.Equals(b));

            a = Vector2f.MinValue;
            b = Vector2f.MinValue;

            Assert.IsTrue(a.Equals(b));

            var c = "I am not a Vector2d32";

            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(a.Equals(c));
        }

        [Test]
        public void TestGetHashCodeMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2f.Zero;
            b = Vector2f.Zero;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2f.Right;
            b = Vector2f.Right;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2f.Up;
            b = Vector2f.Up;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = -Vector2f.One;
            b = -Vector2f.One;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2f.Left;
            b = Vector2f.Left;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2f.Down;
            b = Vector2f.Down;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, 67.76451f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            // test rounding up
            b = new Vector2f(72.002311f, 67.764511f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            // test rounding down
            b = new Vector2f(72.002316f, 67.764516f);

            Assert.IsFalse(a.Equals(b));
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());

            a = new Vector2f(-72.00231f, -67.76451f);
            b = new Vector2f(-72.00231f, -67.76451f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            b = new Vector2f(-72.002311f, -67.764511f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            b = new Vector2f(-72.002316f, -67.764516f);

            Assert.IsFalse(a.Equals(b));
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());

            a = new Vector2f(-72.00231f, 67.76451f);
            b = new Vector2f(-72.00231f, 67.76451f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            b = new Vector2f(-72.002311f, 67.764511f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            b = new Vector2f(-72.002316f, 67.764516f);

            Assert.IsFalse(a.Equals(b));
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());

            a = new Vector2f(72.00231f, -67.76451f);
            b = new Vector2f(72.00231f, -67.76451f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            b = new Vector2f(72.002311f, -67.764511f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            b = new Vector2f(72.002316f, -67.764516f);

            Assert.IsFalse(a.Equals(b));
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2f.PositiveInfinity;
            b = Vector2f.PositiveInfinity;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2f.NegativeInfinity;
            b = Vector2f.NegativeInfinity;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2f.MaxValue;
            b = Vector2f.MaxValue;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector2f.MinValue;
            b = Vector2f.MinValue;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());         
        }

        [Test]
        public void TestDotMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Dot(a, b));

            a = Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(2f, Vector2f.Dot(a, b));

            a = Vector2f.Zero;
            b = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Dot(a, b));

            a = Vector2f.One;
            b = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Dot(a, b));

            a = Vector2f.One;
            b = -Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(-2f, Vector2f.Dot(a, b));

            a = -Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(-2f, Vector2f.Dot(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(-72.00231f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(-592.3042f, Vector2f.Dot(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, -67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(592.30395f, Vector2f.Dot(a, b), Mathf.EpsilonE3);

            a = Vector2f.PositiveInfinity;
            b = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector2f.Dot(a, b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector2f.Dot(a, b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, Vector2f.Dot(a, b));

            a = Vector2f.PositiveInfinity;
            b = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, Vector2f.Dot(a, b));
        }

        [Test]
        public void TestCrossMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.Zero;
            b = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.One;
            b = Vector2f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.One;
            b = -Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = -Vector2f.One;
            b = Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.Right;
            b = Vector2f.Up;

            TestUtilities.AssertThatSinglesAreEqual(1f, Vector2f.Cross(a, b));

            a = Vector2f.Up;
            b = Vector2f.Right;

            TestUtilities.AssertThatSinglesAreEqual(-1f, Vector2f.Cross(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(-72.00231f, -67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(72.00231f, -67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(-9758.403f, Vector2f.Cross(a, b));

            a = new Vector2f(72.00231f, 67.76451f);
            b = new Vector2f(-72.00231f, 67.76451f);

            TestUtilities.AssertThatSinglesAreEqual(9758.403f, Vector2f.Cross(a, b));

            a = Vector2f.PositiveInfinity;
            b = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.NegativeInfinity;
            b = Vector2f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.PositiveInfinity;
            b = Vector2f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = new Vector2f(float.PositiveInfinity, 0f);
            b = new Vector2f(0f, float.PositiveInfinity);            

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector2f.Cross(a, b));

            a = new Vector2f(0f, float.PositiveInfinity);
            b = new Vector2f(float.PositiveInfinity, 0f);

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, Vector2f.Cross(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.MaxValue;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.MinValue;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.MinValue;
            b = Vector2f.MaxValue;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = Vector2f.MaxValue;
            b = Vector2f.MinValue;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Cross(a, b));

            a = new Vector2f(float.MaxValue, 0f);
            b = new Vector2f(0f, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector2f.Cross(a, b));

            a = new Vector2f(0f, float.MaxValue);
            b = new Vector2f(float.MaxValue, 0f);

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, Vector2f.Cross(a, b));
        }

        [Test]
        public void TestPolarAngleMethod()
        {
            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Zero.PolarAngle());
            TestUtilities.AssertThatSinglesAreEqual(0f, Vector2f.Right.PolarAngle());
            TestUtilities.AssertThatSinglesAreEqual(45f, Vector2f.One.PolarAngle());
            TestUtilities.AssertThatSinglesAreEqual(90f, Vector2f.Up.PolarAngle());
            TestUtilities.AssertThatSinglesAreEqual(180f, Vector2f.Left.PolarAngle());
            TestUtilities.AssertThatSinglesAreEqual(-135f, (-Vector2f.One).PolarAngle());
            TestUtilities.AssertThatSinglesAreEqual(-90f, Vector2f.Down.PolarAngle());
        }

        [Test]
        public void TestRotateMethod()
        {
            var a = Vector2f.Right;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Right, a.Rotate(0f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One.Normalised, a.Rotate(45f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Up, a.Rotate(90f), Mathf.EpsilonE7);
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(-1f, 1f).Normalised, a.Rotate(135f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Left, a.Rotate(180f), Mathf.EpsilonE7);
            TestUtilities.AssertThatVector2d32sAreEqual(-Vector2f.One.Normalised, a.Rotate(225f), Mathf.EpsilonE7);
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Down, a.Rotate(270f), Mathf.EpsilonE7);
            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(1f, -1f).Normalised, a.Rotate(315f), Mathf.EpsilonE6);
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Right, a.Rotate(360f), Mathf.EpsilonE6);
        }

        [Test]
        public void TestAngleMethod()
        {
            var a = Vector2f.One;
            var b = new Vector2f(-1f, 1f);

            TestUtilities.AssertThatSinglesAreEqual(90f, Vector2f.Angle(a, b));

            a = Vector2f.One;
            b = -Vector2f.One;

            TestUtilities.AssertThatSinglesAreEqual(180f, Vector2f.Angle(a, b), Mathf.EpsilonE1);

            a = Vector2f.One;
            b = new Vector2f(1f, -1f);

            TestUtilities.AssertThatSinglesAreEqual(90f, Vector2f.Angle(a, b));
        }

        [Test]
        public void TestNormaliseMethod()
        {
            var a = Vector2f.Zero;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector2f.One;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = Vector2f.Right;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector2f.Up;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = -Vector2f.One;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = new Vector2f(-1f, 1f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = Vector2f.Left;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(1f, -1f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = Vector2f.Down;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(72.00231f, 67.76451f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(72.00231f, 0f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(0f, 67.76451f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(72.00231f, -67.76451f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(-72.00231f, 67.76451f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(-72.00231f, -67.76451f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(0f, -67.76451f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector2f(-72.00231f, 0f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector2f.MaxValue;

            // vectors of infinite length are zero length when normalised

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector2f.MinValue;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = new Vector2f(float.MinValue, float.MaxValue);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = new Vector2f(float.MaxValue, float.MinValue);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector2f.PositiveInfinity;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector2f.NegativeInfinity;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = new Vector2f(float.PositiveInfinity, float.NegativeInfinity);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = new Vector2f(float.NegativeInfinity, float.PositiveInfinity);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);
        }

        [Test]
        public void TestNormalizeMethod()
        {
            // we know normalize just expands to normalise
            var vec = Vector2f.One;

            vec.Normalize();

            TestUtilities.AssertThatDoublesAreEqual(1f, vec.Magnitude, Mathf.EpsilonE7);
        }

        [Test]
        public void TestLerpMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.One;

            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.Zero, Vector2f.Lerp(a, b, 0f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One * 0.25f, Vector2f.Lerp(a, b, 0.25f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One * 0.5f, Vector2f.Lerp(a, b, 0.5f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One * 0.75f, Vector2f.Lerp(a, b, 0.75f));
            TestUtilities.AssertThatVector2d32sAreEqual(Vector2f.One, Vector2f.Lerp(a, b, 1f));
        }

        [Test]
        public void TestLiesBetweenMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.One;

            Assert.IsTrue(Vector2f.LiesBetween(Vector2f.Zero, a, b));
            Assert.IsTrue(Vector2f.LiesBetween(Vector2f.One * 0.25f, a, b));
            Assert.IsTrue(Vector2f.LiesBetween(Vector2f.One * 0.5f, a, b));
            Assert.IsTrue(Vector2f.LiesBetween(Vector2f.One * 0.75f, a, b));
            Assert.IsTrue(Vector2f.LiesBetween(Vector2f.One, a, b));

            Assert.IsFalse(Vector2f.LiesBetween(Vector2f.Up, a, b));
            Assert.IsFalse(Vector2f.LiesBetween(Vector2f.Right, a, b));
            Assert.IsFalse(Vector2f.LiesBetween(Vector2f.Down, a, b));
            Assert.IsFalse(Vector2f.LiesBetween(Vector2f.Left, a, b));
        }

        [Test]
        public void TestTimeBetweenMethod()
        {
            var a = Vector2f.Zero;
            var b = Vector2f.One;

            Assert.AreEqual(0f, Vector2f.TimeBetween(Vector2f.Zero, a, b));
            Assert.AreEqual(0.25f, Vector2f.TimeBetween(Vector2f.One * 0.25f, a, b));
            Assert.AreEqual(0.5f, Vector2f.TimeBetween(Vector2f.One * 0.5f, a, b));
            Assert.AreEqual(0.75f, Vector2f.TimeBetween(Vector2f.One * 0.75f, a, b));
            Assert.AreEqual(1f, Vector2f.TimeBetween(Vector2f.One, a, b));

            Assert.Throws<ArithmeticException>(() => { Vector2f.TimeBetween(Vector2f.Up, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector2f.TimeBetween(Vector2f.Right, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector2f.TimeBetween(Vector2f.Down, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector2f.TimeBetween(Vector2f.Left, a, b); });
        }
    }
}
