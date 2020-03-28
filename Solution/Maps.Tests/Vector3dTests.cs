using System;
using NUnit.Framework;

namespace Maps.Tests
{
    /// <summary>
    /// Series of tests for the Vector3d class, appearing in order of
    /// members in class
    /// </summary>
    [TestFixture]
    internal sealed class Vector3dTests
    {
        [Test]
        public void TestConstructor()
        {
            var a = new Vector3d(0d, 0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(0d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(0d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[2]);

            a = new Vector3d(1d, 1d, 1d);

            TestUtilities.AssertThatDoublesAreEqual(1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(1d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(1d, a[2]);

            a = new Vector3d(1d, 0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[2]);

            a = new Vector3d(0d, 1d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(0d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(0d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(1d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[2]);

            a = new Vector3d(0d, 0d, 1d);

            TestUtilities.AssertThatDoublesAreEqual(0d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(0d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(1d, a[2]);

            a = new Vector3d(-1d, -1d, -1d);

            TestUtilities.AssertThatDoublesAreEqual(-1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(-1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a[2]);

            a = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(-1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(-1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[2]);

            a = new Vector3d(0d, -1d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(0d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(0d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[2]);

            a = new Vector3d(0d, 0d, -1d);

            TestUtilities.AssertThatDoublesAreEqual(0d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(0d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a[2]);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(67.76451324678221d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(42.29832471921837d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(67.76451324678221d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(42.29832471921837d, a[2]);

            a = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(-67.76451324678221d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(-42.29832471921837d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(-67.76451324678221d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(-42.29832471921837d, a[2]);

            a = new Vector3d(double.MaxValue, double.MaxValue, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a.y);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a.z);

            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a[2]);

            a = new Vector3d(double.MinValue, double.MinValue, double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a.y);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a.z);

            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a[2]);

            a = new Vector3d(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.y);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.z);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a[2]);

            a = new Vector3d(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a.y);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a.z);

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a[2]);
        }

        [Test]
        public void TestSingleParameterConstructor()
        {
            var a = new Vector3d(0d);

            TestUtilities.AssertThatDoublesAreEqual(0d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(0d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(0d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(0d, a[2]);

            a = new Vector3d(1d);

            TestUtilities.AssertThatDoublesAreEqual(1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(1d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(1d, a[2]);

            a = new Vector3d(-1d);

            TestUtilities.AssertThatDoublesAreEqual(-1d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(-1d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(-1d, a[2]);

            a = new Vector3d(72.00231159357315d);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a[2]);

            a = new Vector3d(-72.00231159357315d);

            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a.x);
            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a.y);
            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a.z);

            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(-72.00231159357315d, a[2]);

            a = new Vector3d(double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a.y);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a.z);

            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(double.MaxValue, a[2]);

            a = new Vector3d(double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a.y);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a.z);

            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(double.MinValue, a[2]);

            a = new Vector3d(double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.y);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.z);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a[2]);

            a = new Vector3d(double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a.x);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a.y);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a.z);

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a[0]);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a[1]);
            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, a[2]);
        }

        [Test]
        public void TestIndexAccessor()
        {
            var a = Vector3d.Zero;

            Assert.Throws<IndexOutOfRangeException>(() => { var d = a[3]; });
            Assert.Throws<IndexOutOfRangeException>(() => { var d = a[-1]; });
        }

        [Test]
        public void TestHighProperty()
        {
            var a = Vector3d.Zero;
            var b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(a.High, b);

            a = Vector3d.One;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(a.High, b);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3f(72.00231159357315f, 67.76451324678221f, 42.29832471921837f);

            TestUtilities.AssertThatVector3d32sAreEqual(a.High, b);
        }

        [Test]
        public void TestLowProperty()
        {
            var a = Vector3d.Zero;
            var b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(a.Low, b);

            a = Vector3d.One;

            TestUtilities.AssertThatVector3d32sAreEqual(a.Low, Vector3f.Zero);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3f((float)(a.x - a.High.x),
                             (float)(a.y - a.High.y),
                             (float)(a.z - a.High.z));

            TestUtilities.AssertThatVector3d32sAreEqual(a.Low, b);
        }

        [Test]
        public void TestMinComponentProperty()
        {
            var a = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MinComponent);

            a = Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.MinComponent);

            a = Vector3d.Right;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MinComponent);

            a = Vector3d.Up;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MinComponent);

            a = Vector3d.Forward;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MinComponent);

            a = Vector3d.Left;

            TestUtilities.AssertThatDoublesAreEqual(-1d, a.MinComponent);

            a = Vector3d.Down;

            TestUtilities.AssertThatDoublesAreEqual(-1d, a.MinComponent);

            a = Vector3d.Back;

            TestUtilities.AssertThatDoublesAreEqual(-1d, a.MinComponent);
        }

        [Test]
        public void TestMaxComponentProperty()
        {
            var a = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MaxComponent);

            a = Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.MaxComponent);

            a = Vector3d.Right;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.MaxComponent);

            a = Vector3d.Up;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.MaxComponent);

            a = Vector3d.Forward;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.MaxComponent);

            a = Vector3d.Left;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MaxComponent);

            a = Vector3d.Down;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MaxComponent);

            a = Vector3d.Back;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.MaxComponent);
        }

        [Test]
        public void TestMagnitudeProperty()
        {
            var a = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(1.73205080756888d, a.Magnitude, Mathd.EpsilonE14);

            a = -Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(1.73205080756888d, a.Magnitude, Mathd.EpsilonE14);

            a = Vector3d.Right;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Left;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Up;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Down;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(107.543063023348d, a.Magnitude, Mathd.EpsilonE12);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(98.8754880159451d, a.Magnitude, Mathd.EpsilonE13);

            a = new Vector3d(72.00231159357315d, 0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, a.Magnitude);

            a = new Vector3d(-72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(107.543063023348d, a.Magnitude, Mathd.EpsilonE12);

            a = new Vector3d(72.00231159357315d, -67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(107.543063023348d, a.Magnitude, Mathd.EpsilonE12);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(107.543063023348d, a.Magnitude, Mathd.EpsilonE12);

            a = new Vector3d(-72.00231159357315d, -67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(107.543063023348d, a.Magnitude, Mathd.EpsilonE12);

            a = new Vector3d(72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(107.543063023348d, a.Magnitude, Mathd.EpsilonE12);

            a = new Vector3d(-72.00231159357315d, 67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(107.543063023348d, a.Magnitude, Mathd.EpsilonE12);

            a = new Vector3d(72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(107.543063023348d, a.Magnitude, Mathd.EpsilonE12);

            a = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(107.543063023348d, a.Magnitude, Mathd.EpsilonE12);

            a = Vector3d.MaxValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = Vector3d.MinValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.MinValue, double.MinValue, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.MinValue, double.MaxValue, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.MaxValue, double.MaxValue, double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.MaxValue, double.MinValue, double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.MaxValue, double.MinValue, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.MinValue, double.MaxValue, double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.NegativeInfinity, double.NegativeInfinity, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.NegativeInfinity, double.PositiveInfinity, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.PositiveInfinity, double.PositiveInfinity, double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.PositiveInfinity, double.NegativeInfinity, double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);

            a = new Vector3d(double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.Magnitude);
        }

        [Test]
        public void TestSqrMagnitudeProperty()
        {
            var a = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.SqrMagnitude);

            a = Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(3d, a.SqrMagnitude);

            a = -Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(3d, a.SqrMagnitude);

            a = Vector3d.Right;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.SqrMagnitude);

            a = Vector3d.Left;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.SqrMagnitude);

            a = Vector3d.Up;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.SqrMagnitude);

            a = Vector3d.Down;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.SqrMagnitude);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(11565.5104044437d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(9776.36213039132d, a.SqrMagnitude);

            a = new Vector3d(72.00231159357315d, 0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(5184.33287481799d, a.SqrMagnitude, Mathd.EpsilonE11);

            a = new Vector3d(-72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(11565.5104044437d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector3d(72.00231159357315d, -67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(11565.5104044437d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(11565.5104044437d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector3d(-72.00231159357315d, -67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(11565.5104044437d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector3d(72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(11565.5104044437d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector3d(-72.00231159357315d, 67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(11565.5104044437d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector3d(72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(11565.5104044437d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(11565.5104044437d, a.SqrMagnitude, Mathd.EpsilonE10);

            a = Vector3d.MaxValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = Vector3d.MinValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.MinValue, double.MinValue, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.MinValue, double.MaxValue, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.MaxValue, double.MaxValue, double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.MaxValue, double.MinValue, double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.MaxValue, double.MinValue, double.MaxValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.MinValue, double.MaxValue, double.MinValue);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.NegativeInfinity, double.NegativeInfinity, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.NegativeInfinity, double.PositiveInfinity, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.PositiveInfinity, double.PositiveInfinity, double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.PositiveInfinity, double.NegativeInfinity, double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3d(double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity);

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, a.SqrMagnitude);
        }

        [Test]
        public void TestXySwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(a.x, a.y), a.xy);
        }

        [Test]
        public void TestXzSwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(a.x, a.z), a.xz);
        }

        [Test]
        public void TestYxSwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(a.y, a.x), a.yx);
        }

        [Test]
        public void TestYzSwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(a.y, a.z), a.yz);
        }

        [Test]
        public void TestZxSwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(a.z, a.x), a.zx);
        }

        [Test]
        public void TestZySwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(a.z, a.y), a.zy);
        }

        [Test]
        public void TestXyzSwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.x, a.y, a.z), a.xyz);
        }

        [Test]
        public void TestXzySwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.x, a.z, a.y), a.xzy);
        }

        [Test]
        public void TestYxzSwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.y, a.x, a.z), a.yxz);
        }

        [Test]
        public void TestYzxSwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.y, a.z, a.x), a.yzx);
        }

        [Test]
        public void TestZyxSwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.z, a.y, a.x), a.zyx);
        }

        [Test]
        public void TestZxySwizzleProperty()
        {
            var a = new Vector3d(1d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.z, a.x, a.y), a.zxy);
        }

        [Test]
        public void TestNormalisedProperty()
        {
            var a = Vector3d.Zero;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector3d.One;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Right;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Up;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = -Vector3d.One;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Left;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Down;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            // vectors of infinite length are zero length when normalised

            a = Vector3d.MinValue;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector3d.MaxValue;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector3d.PositiveInfinity;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector3d.NegativeInfinity;

            a = a.Normalised;

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);
        }

        [Test]
        public void TestUnaryNegationOperator()
        {
            var a = Vector3d.Zero;
            var aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.One;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = -Vector3d.One;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.Right;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.Left;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.Up;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.Down;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.Forward;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.Back;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.MaxValue;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.MinValue;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.PositiveInfinity;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);

            a = Vector3d.NegativeInfinity;
            aNegative = -a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.x, -a.y, -a.z), aNegative);
        }

        [Test]
        public void TestSubtrationOperator()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;
            var c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.One;
            b = Vector3d.One;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.Zero;
            b = Vector3d.One;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, c);

            a = Vector3d.One;
            b = Vector3d.Zero;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            a = Vector3d.One;
            b = Vector3d.Up;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(1d, 0d, 1d), c);

            a = Vector3d.Zero;
            b = Vector3d.Right;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left, c);

            a = Vector3d.Zero;
            b = Vector3d.Up;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Down, c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = Vector3d.Zero;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d, 
                                                                   42.29832471921837d), 
                                                                   c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.Zero;
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d); 
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-72.00231159357315d,
                                                                   -67.76451324678221d,
                                                                   -42.29832471921837d), 
                                                                   c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(0.00231159357315d, 0.76451324678221d, 0.29832471921837d);
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72d, 67d, 42d), c);

            a = Vector3d.MaxValue;
            b = Vector3d.MaxValue;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.MinValue;
            b = Vector3d.MinValue;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.MaxValue;
            b = Vector3d.MinValue;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, c);

            a = Vector3d.MinValue;
            b = Vector3d.MaxValue;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);

            a = Vector3d.PositiveInfinity;
            b = Vector3d.PositiveInfinity;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.NegativeInfinity;
            b = Vector3d.NegativeInfinity;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.PositiveInfinity;
            b = Vector3d.NegativeInfinity;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, c);

            a = Vector3d.NegativeInfinity;
            b = Vector3d.PositiveInfinity;
            c = a - b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);
        }

        [Test]
        public void TestAdditionOperator()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;
            var c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.One;
            b = Vector3d.One;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(2d, 2d, 2d), c);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(2d, 2d, 2d), c);

            a = Vector3d.Zero;
            b = Vector3d.One;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            a = Vector3d.One;
            b = Vector3d.Zero;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            a = Vector3d.One;
            b = Vector3d.Up;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(1d, 2d, 1d), c);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(1d, 2d, 1d), c);

            a = Vector3d.Zero;
            b = Vector3d.Right;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right, c);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right, c);

            a = Vector3d.Zero;
            b = Vector3d.Up;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up, c);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up, c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = Vector3d.Zero;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   42.29832471921837d),
                                                                   c);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   42.29832471921837d),
                                                                   c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(144.0046231871463d,
                                                                   135.5290264935644d,
                                                                   84.59664943843674d), 
                                                                   c,
                                                                   Mathd.EpsilonE13);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(144.0046231871463d,
                                                                   135.5290264935644d,
                                                                   84.59664943843674d),
                                                                   c,
                                                                   Mathd.EpsilonE13);

            a = Vector3d.Zero;
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   42.29832471921837d), 
                                                                   c);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   42.29832471921837d),
                                                                   c);

            a = new Vector3d(72d, 67d, 42d);
            b = new Vector3d(0.00231159357315d, 0.76451324678221d, 0.29832471921837d);
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   42.29832471921837d), 
                                                                   c);

            c = b + a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   42.29832471921837d),
                                                                   c);

            a = Vector3d.MaxValue;
            b = Vector3d.MaxValue;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, c);

            a = Vector3d.MinValue;
            b = Vector3d.MinValue;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);

            a = Vector3d.MaxValue;
            b = Vector3d.MinValue;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.MinValue;
            b = Vector3d.MaxValue;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.PositiveInfinity;
            b = Vector3d.PositiveInfinity;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.NegativeInfinity;
            b = Vector3d.NegativeInfinity;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.PositiveInfinity;
            b = Vector3d.NegativeInfinity;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, c);

            a = Vector3d.NegativeInfinity;
            b = Vector3d.PositiveInfinity;
            c = a + b;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);
        }

        [Test]
        public void TestMultiplicationOperator()
        {
            var a = Vector3d.Zero;
            var c = a * 0d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            c = 0d * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.One;
            c = a * 1d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            c = 1d * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            a = Vector3d.Zero;
            c = a * 1d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            c = 1d * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.One;
            c = a * 0d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            c = 0d * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = Vector3d.Up;
            c = a * 1d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up, c);

            c = 1d * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up, c);

            a = Vector3d.Right;
            c = a * 1d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right, c);

            c = 1d * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right, c);

            a = Vector3d.One;
            c = a * -1d;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, c);

            c = -1d * a;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, c);

            a = Vector3d.One;
            c = a * 2d;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(2d, 2d, 2d), c);

            c = 2d * a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(2d, 2d, 2d), c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a * 0d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            c = 0d * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a * 1d;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   42.29832471921837d),
                                                                   c);

            c = 1d * a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   42.29832471921837d),
                                                                   c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a * 2d;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(144.0046231871463d,
                                                                   135.5290264935644d,
                                                                   84.59664943843674d),
                                                                   c,
                                                                   Mathd.EpsilonE13);

            c = 2d * a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(144.0046231871463d,
                                                                   135.5290264935644d,
                                                                   84.59664943843674d),
                                                                   c,
                                                                   Mathd.EpsilonE13);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a * -1d;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-72.00231159357315d,
                                                                   -67.76451324678221d,
                                                                   -42.29832471921837d),
                                                                   c);

            c = -1d * a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-72.00231159357315d,
                                                                   -67.76451324678221d,
                                                                   -42.29832471921837d),
                                                                   c);

            a = Vector3d.MaxValue;
            c = a * double.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, c);

            c = double.MaxValue * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, c);

            a = Vector3d.MinValue;
            c = a * double.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, c);

            c = double.MinValue * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, c);

            a = Vector3d.MaxValue;
            c = a * double.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);

            c = double.MinValue * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);

            a = Vector3d.MinValue;
            c = a * double.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);

            c = double.MaxValue * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);

            a = Vector3d.PositiveInfinity;
            c = a * double.PositiveInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            c = double.PositiveInfinity * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.NegativeInfinity;
            c = a * double.NegativeInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            c = double.NegativeInfinity * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.PositiveInfinity;
            c = a * double.NegativeInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);

            c = double.NegativeInfinity * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);

            a = Vector3d.NegativeInfinity;
            c = a * double.PositiveInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);

            c = double.PositiveInfinity * a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);
        }

        [Test]
        public void TestDivisionOperator()
        {
            var a = Vector3d.Zero;
            var c = a / 0d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.One;
            c = a / 1d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            c = 1d / a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            a = Vector3d.Zero;
            c = a / 1d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            c = 1d / a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.One;
            c = a / 0d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            c = 0d / a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.Up;
            c = a / 1d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up, c);

            c = 1d / a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.Right;
            c = a / 1d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right, c);

            c = 1d / a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.One;
            c = a / -1d;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, c);

            c = -1d / a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.One;
            c = a / 2d;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0.5d, 0.5d, 0.5d), c);

            c = 2d / a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(2d, 2d, 2d), c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a / 0d;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            c = 0d / a;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a / 1d;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   42.29832471921837d),
                                                                   c);

            c = 1d / a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(1d / 72.00231159357315d,
                                                                   1d / 67.76451324678221d,
                                                                   1d / 42.29832471921837d),
                                                                   c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a / 2d;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d / 2d,
                                                                   67.76451324678221d / 2d,
                                                                   42.29832471921837d / 2d),
                                                                   c);

            c = 2d / a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(2d / 72.00231159357315d,
                                                                   2d / 67.76451324678221d,
                                                                   2d / 42.29832471921837d),
                                                                   c);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            c = a / -1d;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-72.00231159357315d,
                                                                   -67.76451324678221d,
                                                                   -42.29832471921837d),
                                                                   c);

            c = -1d / a;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-1d / 72.00231159357315d,
                                                                   -1d / 67.76451324678221d,
                                                                   -1d / 42.29832471921837d),
                                                                   c);

            a = Vector3d.MaxValue;
            c = a / double.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            a = Vector3d.MinValue;
            c = a / double.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, c);

            a = Vector3d.MaxValue;
            c = a / double.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, c);

            a = Vector3d.MinValue;
            c = a / double.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, c);

            a = Vector3d.PositiveInfinity;
            c = a / double.PositiveInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.NegativeInfinity;
            c = a / double.NegativeInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NaN, c);

            a = Vector3d.PositiveInfinity;
            c = a / double.NegativeInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, c);

            a = Vector3d.NegativeInfinity;
            c = a / double.PositiveInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, c);
        }

        [Test]
        public void TestEqualityOperator()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.One;
            b = Vector3d.One;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.Right;
            b = Vector3d.Right;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.Up;
            b = Vector3d.Up;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = -Vector3d.One;
            b = -Vector3d.One;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.Left;
            b = Vector3d.Left;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.Down;
            b = Vector3d.Down;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);
            b = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector3d(-72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(-72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector3d(72.00231159357315d, -67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(72.00231159357315d, -67.76451324678221d, 42.29832471921837d);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.PositiveInfinity;
            b = Vector3d.PositiveInfinity;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.NegativeInfinity;
            b = Vector3d.NegativeInfinity;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.MaxValue;
            b = Vector3d.MaxValue;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.MinValue;
            b = Vector3d.MinValue;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [Test]
        public void TestInequalityOperator()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.One;
            b = Vector3d.One;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3d.Right;
            b = Vector3d.Up;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector3d.Up;
            b = Vector3d.Right;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = new Vector3d(-1d, 1d, -1d);
            b = new Vector3d(1d, -1d, 1d);

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector3d.Left;
            b = Vector3d.Down;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector3d.Down;
            b = Vector3d.Left;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = new Vector3d(-72.00231159357315d, 67.76451324678221d, -42.29832471921837d);
            b = new Vector3d(72.00231159357315d, -67.76451324678221d, 42.29832471921837d);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = new Vector3d(72.00231159357315d, -67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(-72.00231159357315d, 67.76451324678221d, -42.29832471921837d);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector3d.PositiveInfinity;
            b = Vector3d.NegativeInfinity;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector3d.NegativeInfinity;
            b = Vector3d.PositiveInfinity;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector3d.MaxValue;
            b = Vector3d.MinValue;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector3d.MinValue;
            b = Vector3d.MaxValue;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);
        }

        [Test]
        public void TestOneProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(1d, 1d, 1d), Vector3d.One);
        }

        [Test]
        public void TestZeroProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, 0d), Vector3d.Zero);
        }

        [Test]
        public void TestUpProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 1d, 0d), Vector3d.Up);
        }

        [Test]
        public void TestDownProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, -1d, 0d), Vector3d.Down);
        }

        [Test]
        public void TestRightProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(1d, 0d, 0d), Vector3d.Right);
        }

        [Test]
        public void TestLeftProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-1d, 0d, 0d), Vector3d.Left);
        }

        [Test]
        public void TestForwardProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, 1d), Vector3d.Forward);
        }

        [Test]
        public void TestBackProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, -1d), Vector3d.Back);
        }

        [Test]
        public void TestMaxValueProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(double.MaxValue,
                                                                   double.MaxValue,
                                                                   double.MaxValue),
                                                      Vector3d.MaxValue);
        }

        [Test]
        public void TestMinValueProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(double.MinValue,
                                                                   double.MinValue,
                                                                   double.MinValue),
                                                      Vector3d.MinValue);
        }

        [Test]
        public void TestPositiveInfinityProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(double.PositiveInfinity,
                                                                   double.PositiveInfinity,
                                                                   double.PositiveInfinity),
                                                      Vector3d.PositiveInfinity);
        }

        [Test]
        public void TestNegativeInfinityProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(double.NegativeInfinity,
                                                                   double.NegativeInfinity,
                                                                   double.NegativeInfinity),
                                                      Vector3d.NegativeInfinity);
        }

        [Test]
        public void TestNaNProperty()
        {
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(double.NaN,
                                                                   double.NaN,
                                                                   double.NaN),
                                                      Vector3d.NaN);
        }

        [Test]
        public void TestIsNaNMethod()
        {
            var a = Vector3d.One;

            Assert.IsFalse(Vector3d.IsNaN(a));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            Assert.IsFalse(Vector3d.IsNaN(a));

            a = Vector3d.NaN;

            Assert.IsTrue(Vector3d.IsNaN(a));
        }

        [Test]
        public void TestComponentMultiplyMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.ComponentMultiply(a, b));

            a = Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.ComponentMultiply(a, b));

            a = Vector3d.Zero;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.ComponentMultiply(a, b));

            a = Vector3d.One;
            b = -Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, Vector3d.ComponentMultiply(a, b));

            a = Vector3d.One;
            b = new Vector3d(-1d, 1d, -1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-1d, 1d, -1d), 
                                                      Vector3d.ComponentMultiply(a, b));

            a = Vector3d.One;
            b = new Vector3d(1d, -1d, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(1d, -1d, 1d), 
                                                      Vector3d.ComponentMultiply(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(-1d, 1d, -1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   -42.29832471921837d), 
                                                      Vector3d.ComponentMultiply(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(1d, -1d, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   -67.76451324678221d,
                                                                   42.29832471921837d),
                                                      Vector3d.ComponentMultiply(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(2d, 2d, 2d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(144.0046231871463d,
                                                                   135.5290264935644d,
                                                                   84.59664943843674d), 
                                                      Vector3d.ComponentMultiply(a, b), 
                                                      Mathd.EpsilonE13);
        }

        [Test]
        public void TestPowMethod()
        {
            var a = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Pow(a, 0d));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Pow(a, 1d));

            a = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Pow(a, 0d));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Pow(a, 1d));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Pow(a, 2d));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Pow(a, 3d));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Pow(a, 0d));
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d, 
                                                                   67.76451324678221d, 
                                                                   42.29832471921837d), 
                                                      Vector3d.Pow(a, 1d));

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(5184.33287481799d, 
                                                                   4592.02925557332d, 
                                                                   1789.14827405243d), 
                                                      Vector3d.Pow(a, 2d), 
                                                      Mathd.EpsilonE10);
        }

        [Test]
        public void TestDistanceMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector3d.Distance(a, b));

            a = Vector3d.Zero;
            b = Vector3d.Up;

            TestUtilities.AssertThatDoublesAreEqual(1d, Vector3d.Distance(a, b));

            a = Vector3d.Zero;
            b = Vector3d.Right;

            TestUtilities.AssertThatDoublesAreEqual(1d, Vector3d.Distance(a, b));

            a = Vector3d.Zero;
            b = Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(System.Math.Sqrt(3d), Vector3d.Distance(a, b));

            a = Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector3d.Distance(a, b));

            a = Vector3d.Left;
            b = Vector3d.Right;

            TestUtilities.AssertThatDoublesAreEqual(2d, Vector3d.Distance(a, b));

            a = Vector3d.Down;
            b = Vector3d.Up;

            TestUtilities.AssertThatDoublesAreEqual(2d, Vector3d.Distance(a, b));

            a = Vector3d.Forward;
            b = Vector3d.Back;

            TestUtilities.AssertThatDoublesAreEqual(2d, Vector3d.Distance(a, b));

            a = -Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(System.Math.Sqrt(3d) * 2d, Vector3d.Distance(a, b));

            a = new Vector3d(72.00231159357315d, 0d, 0d);
            b = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, Vector3d.Distance(a, b));

            a = Vector3d.Zero;
            b = new Vector3d(72.00231159357315d, 0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(72.00231159357315d, Vector3d.Distance(a, b));

            a = new Vector3d(0d, 67.76451324678221d, 0d);
            b = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(67.76451324678221d, Vector3d.Distance(a, b));

            a = Vector3d.Zero;
            b = new Vector3d(0d, 67.76451324678221d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(67.76451324678221d, Vector3d.Distance(a, b));

            a = Vector3d.Zero;
            b = new Vector3d(0d, 0d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(42.29832471921837d, Vector3d.Distance(a, b));

            a = new Vector3d(0d, 0d, 42.29832471921837d);
            b = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(42.29832471921837d, Vector3d.Distance(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(107.543063023347, 
                                                    Vector3d.Distance(a, b), 
                                                    Mathd.EpsilonE12);

            a = Vector3d.Zero;
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(107.543063023347, 
                                                    Vector3d.Distance(a, b), 
                                                    Mathd.EpsilonE12);

            a = Vector3d.PositiveInfinity;
            b = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector3d.Distance(a, b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector3d.Distance(a, b));

            a = Vector3d.PositiveInfinity;
            b = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector3d.Distance(a, b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector3d.Distance(a, b));

            a = Vector3d.MaxValue;
            b = Vector3d.MaxValue;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector3d.Distance(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.MinValue;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector3d.Distance(a, b));

            a = Vector3d.MaxValue;
            b = Vector3d.MinValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector3d.Distance(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.MaxValue;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector3d.Distance(a, b));
        }

        [Test]
        public void TestComponentDistanceMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.ComponentDistance(a, b));

            a = Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.ComponentDistance(a, b));

            a = Vector3d.One;
            b = -Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(2d, 2d, 2d), 
                                                      Vector3d.ComponentDistance(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d, 
                                                                   67.76451324678221d,
                                                                   42.29832471921837d), 
                                                      Vector3d.ComponentDistance(a, b));

            a = Vector3d.Zero;
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(72.00231159357315d,
                                                                   67.76451324678221d,
                                                                   42.29832471921837d),
                                                      Vector3d.ComponentDistance(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(144.0046231871463d,
                                                                   135.52902649356442d,
                                                                   84.59664943843674d),
                                                      Vector3d.ComponentDistance(a, b));
        }

        [Test]
        public void TestMidpointMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0.5d, 0.5d, 0.5d),
                                                      Vector3d.Midpoint(a, b));

            a = Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Midpoint(a, b));

            a = Vector3d.One;
            b = -Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Midpoint(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(36.001155796786575d,
                                                                   33.882256623391105d,
                                                                   21.149162359609185d),
                                                      Vector3d.Midpoint(a, b));

            a = Vector3d.Zero;
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(36.001155796786575d,
                                                                   33.882256623391105d,
                                                                   21.149162359609185d), 
                                                      Vector3d.Midpoint(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Midpoint(a, b));
        }

        [Test]
        public void TestScaleMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Scale(a, b));

            a = Vector3d.Zero;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Scale(a, b));

            a = Vector3d.One;
            b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Scale(a, b));

            a = Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Scale(a, b));

            a = Vector3d.One;
            b = -Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, Vector3d.Scale(a, b));

            a = Vector3d.One;
            b = new Vector3d(2d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(2d), Vector3d.Scale(a, b));

            a = new Vector3d(2d);
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(2d), Vector3d.Scale(a, b));

            a = Vector3d.One;
            b = new Vector3d(-2d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-2d), Vector3d.Scale(a, b));

            a = new Vector3d(-2d);
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-2d), Vector3d.Scale(a, b));

            a = Vector3d.MaxValue;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.MaxValue, Vector3d.Scale(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.MinValue, Vector3d.Scale(a, b));

            a = Vector3d.MaxValue;
            b = Vector3d.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, Vector3d.Scale(a, b));

            a = Vector3d.MaxValue;
            b = Vector3d.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, Vector3d.Scale(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, Vector3d.Scale(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, Vector3d.Scale(a, b));
        }

        [Test]
        public void TestMaxMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Max(a, b));

            a = Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Max(a, b));

            a = -Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Max(a, b));

            a = Vector3d.One;
            b = -Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Max(a, b));

            a = Vector3d.Zero;
            b = -Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Max(a, b));

            a = -Vector3d.One;
            b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Max(a, b));

            a = Vector3d.One;
            b = Vector3d.One * 2;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One * 2, Vector3d.Max(a, b));

            a = Vector3d.One * 2;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One * 2, Vector3d.Max(a, b));

            a = Vector3d.Up;
            b = Vector3d.Right;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(1d, 1d, 0d), 
                                                      Vector3d.Max(a, b));

            a = Vector3d.Right;
            b = Vector3d.Up;

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(1d, 1d, 0d), 
                                                      Vector3d.Max(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(a, Vector3d.Max(a, b));

            a = Vector3d.Zero;
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatVector3dsAreEqual(b, Vector3d.Max(a, b));

            a = Vector3d.PositiveInfinity;
            b = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, Vector3d.Max(a, b));

            a = Vector3d.PositiveInfinity;
            b = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, Vector3d.Max(a, b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, Vector3d.Max(a, b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, Vector3d.Max(a, b));

            a = Vector3d.MaxValue;
            b = Vector3d.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.MaxValue, Vector3d.Max(a, b));

            a = Vector3d.MaxValue;
            b = Vector3d.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.MaxValue, Vector3d.Max(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.MaxValue, Vector3d.Max(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.MinValue, Vector3d.Max(a, b));
        }

        [Test]
        public void TestMinMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Min(a, b));

            a = Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Min(a, b));

            a = -Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, Vector3d.Min(a, b));

            a = Vector3d.One;
            b = -Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, Vector3d.Min(a, b));

            a = Vector3d.Zero;
            b = -Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, Vector3d.Min(a, b));

            a = -Vector3d.One;
            b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(-Vector3d.One, Vector3d.Min(a, b));

            a = Vector3d.One;
            b = Vector3d.One * 2;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Min(a, b));

            a = Vector3d.One * 2;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Min(a, b));

            a = Vector3d.Up;
            b = Vector3d.Right;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Min(a, b));

            a = Vector3d.Right;
            b = Vector3d.Up;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Min(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Min(a, b));

            a = Vector3d.Zero;
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Min(a, b));

            a = Vector3d.PositiveInfinity;
            b = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, Vector3d.Min(a, b));

            a = Vector3d.PositiveInfinity;
            b = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, Vector3d.Min(a, b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, Vector3d.Min(a, b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, Vector3d.Min(a, b));

            a = Vector3d.MaxValue;
            b = Vector3d.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.MaxValue, Vector3d.Min(a, b));

            a = Vector3d.MaxValue;
            b = Vector3d.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.MinValue, Vector3d.Min(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.MinValue, Vector3d.Min(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.MinValue, Vector3d.Min(a, b));
        }

        [Test]
        public void TestToStringMethod()
        {
            var a = Vector3d.Zero;
            Assert.AreEqual(@"[0]d,[0]d,[0]d", a.ToString());

            a = Vector3d.One;
            Assert.AreEqual(@"[1]d,[1]d,[1]d", a.ToString());

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.2983247192184d);
            Assert.AreEqual(@"[72.0023115935732]d,[67.7645132467822]d,[42.2983247192184]d", a.ToString());

            a = Vector3d.PositiveInfinity;
            Assert.AreEqual(@"[Infinity]d,[Infinity]d,[Infinity]d", a.ToString());

            a = Vector3d.NegativeInfinity;
            Assert.AreEqual(@"[-Infinity]d,[-Infinity]d,[-Infinity]d", a.ToString());
        }

        [Test]
        public void TestEqualsMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            Assert.IsTrue(a.Equals(b));

            a = Vector3d.One;
            b = Vector3d.One;

            Assert.IsTrue(a.Equals(b));

            a = Vector3d.Right;
            b = Vector3d.Right;

            Assert.IsTrue(a.Equals(b));

            a = Vector3d.Up;
            b = Vector3d.Up;

            Assert.IsTrue(a.Equals(b));

            a = -Vector3d.One;
            b = -Vector3d.One;

            Assert.IsTrue(a.Equals(b));

            a = Vector3d.Left;
            b = Vector3d.Left;

            Assert.IsTrue(a.Equals(b));

            a = Vector3d.Down;
            b = Vector3d.Down;

            Assert.IsTrue(a.Equals(b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            Assert.IsTrue(a.Equals(b));

            // test rounding up
            b = new Vector3d(72.002311593573149d, 67.764513246782209d, 42.298324719218369d);

            Assert.IsTrue(a.Equals(b));

            // test rounding down
            b = new Vector3d(72.002311593573151d, 67.764513246782211d, 42.298324719218371d);

            Assert.IsTrue(a.Equals(b));

            a = Vector3d.PositiveInfinity;
            b = Vector3d.PositiveInfinity;

            Assert.IsTrue(a.Equals(b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.NegativeInfinity;

            Assert.IsTrue(a.Equals(b));

            a = Vector3d.MaxValue;
            b = Vector3d.MaxValue;

            Assert.IsTrue(a.Equals(b));

            a = Vector3d.MinValue;
            b = Vector3d.MinValue;

            Assert.IsTrue(a.Equals(b));

            var c = "I am not a Vector3d";

            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(a.Equals(c));
        }

        [Test]
        public void TestGetHashCodeMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3d.One;
            b = Vector3d.One;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3d.Right;
            b = Vector3d.Right;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3d.Up;
            b = Vector3d.Up;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = -Vector3d.One;
            b = -Vector3d.One;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3d.Left;
            b = Vector3d.Left;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3d.Down;
            b = Vector3d.Down;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            // test rounding up
            b = new Vector3d(72.002311593573149d, 67.764513246782209d, 42.298324719218369d);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            // test rounding down
            b = new Vector3d(72.002311593573151d, 67.764513246782211d, 42.298324719218371d);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3d.PositiveInfinity;
            b = Vector3d.PositiveInfinity;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3d.NegativeInfinity;
            b = Vector3d.NegativeInfinity;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3d.MaxValue;
            b = Vector3d.MaxValue;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3d.MinValue;
            b = Vector3d.MinValue;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void TestDotMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector3d.Dot(a, b));

            a = Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(3d, Vector3d.Dot(a, b));

            a = Vector3d.Zero;
            b = Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector3d.Dot(a, b));

            a = Vector3d.One;
            b = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(0d, Vector3d.Dot(a, b));

            a = Vector3d.One;
            b = -Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(-3d, Vector3d.Dot(a, b));

            a = -Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatDoublesAreEqual(-3d, Vector3d.Dot(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(-72.00231159357315d, 67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(-2381.451893297116d,
                                                    Vector3d.Dot(a, b),
                                                    Mathd.EpsilonE12);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(72.00231159357315d, -67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatDoublesAreEqual(2381.451893297116d,
                                                    Vector3d.Dot(a, b),
                                                    Mathd.EpsilonE12);

            a = Vector3d.PositiveInfinity;
            b = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector3d.Dot(a, b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.PositiveInfinity, Vector3d.Dot(a, b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, Vector3d.Dot(a, b));

            a = Vector3d.PositiveInfinity;
            b = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatDoublesAreEqual(double.NegativeInfinity, Vector3d.Dot(a, b));
        }

        [Test]
        public void TestCrossMethod()
        {
            // 1 or more zero sized vectors produce zero sized cross product
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));

            // parallel vectors produce zero sided cross product
            a = Vector3d.One;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));

            // 1 or more zero sized vectors produce zero sized cross product
            a = Vector3d.Zero;
            b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(b, a));

            // parallel vectors produce zero sided cross product
            a = Vector3d.One;
            b = -Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(b, a));

            // perpendicular vectors produce full sized cross product
            a = Vector3d.Right;
            b = Vector3d.Up;

            // cross product is not communicative
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Forward, Vector3d.Cross(a, b));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Back, Vector3d.Cross(b, a));

            // parallel vectors produce zero sided cross product
            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, a));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(72.00231159357315d, -67.76451324678221d, 42.29832471921837d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(5732.650771504337d, 
                                                                   0d,
                                                                   -9758.403195563256d), 
                                                      Vector3d.Cross(a, b),
                                                      Mathd.EpsilonE11);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);
            b = new Vector3d(-72.00231159357315d, 67.76451324678221d, -42.29832471921837d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-5732.650771504337d, 
                                                                   0d,
                                                                   9758.403195563256d), 
                                                      Vector3d.Cross(a, b),
                                                      Mathd.EpsilonE11);

            a = Vector3d.PositiveInfinity;
            b = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));

            a = Vector3d.NegativeInfinity;
            b = Vector3d.PositiveInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));

            a = Vector3d.PositiveInfinity;
            b = Vector3d.NegativeInfinity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));

            a = new Vector3d(double.PositiveInfinity, 0d, 0d);
            b = new Vector3d(0d, double.PositiveInfinity, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.PositiveInfinity, 
                                                      Vector3d.Cross(a, b));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.NegativeInfinity, 
                                                      Vector3d.Cross(b, a));

            a = Vector3d.MaxValue;
            b = Vector3d.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.MinValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));

            a = Vector3d.MinValue;
            b = Vector3d.MaxValue;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(a, b));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Cross(b, a));

            a = new Vector3d(double.MaxValue, 0d, 0d);
            b = new Vector3d(0d, double.MaxValue, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 
                                                                   0d,
                                                                   double.PositiveInfinity), 
                                                      Vector3d.Cross(a, b));
            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d,
                                                                   0d,
                                                                   double.NegativeInfinity), 
                                                      Vector3d.Cross(b, a));
        }

        [Test]
        public void TestRotateMethod()
        {
            var a = Vector3d.Right;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up, 
                                                      a.Rotate(Vector3d.Forward, 90d),
                                                      Mathd.EpsilonE16);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left,
                                                      a.Rotate(Vector3d.Forward, 180d),
                                                      Mathd.EpsilonE15);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Down,
                                                      a.Rotate(Vector3d.Forward, 270d),
                                                      Mathd.EpsilonE15);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right,
                                                      a.Rotate(Vector3d.Forward, 360d),
                                                      Mathd.EpsilonE15);

            a = Vector3d.Forward;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right,
                                                      a.Rotate(Vector3d.Up, 90d),
                                                      Mathd.EpsilonE16);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Back,
                                                      a.Rotate(Vector3d.Up, 180d),
                                                      Mathd.EpsilonE15);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left,
                                                      a.Rotate(Vector3d.Up, 270d),
                                                      Mathd.EpsilonE15);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Forward,
                                                      a.Rotate(Vector3d.Up, 360d),
                                                      Mathd.EpsilonE15);
        }

        [Test]
        public void TestAngleMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(90d, Vector3d.Angle(a, b));

            a = Vector3d.Up;
            b = Vector3d.Zero;

            TestUtilities.AssertThatDoublesAreEqual(90d, Vector3d.Angle(a, b));

            a = Vector3d.Up;
            b = Vector3d.Right;

            TestUtilities.AssertThatDoublesAreEqual(90d, Vector3d.Angle(a, b));

            a = Vector3d.Up;
            b = Vector3d.Left;

            TestUtilities.AssertThatDoublesAreEqual(90d, Vector3d.Angle(a, b));

            a = Vector3d.Up;
            b = Vector3d.Down;

            TestUtilities.AssertThatDoublesAreEqual(180d, Vector3d.Angle(a, b));
        }

        [Test]
        public void TestNormaliseMethod()
        {
            var a = Vector3d.Zero;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector3d.One;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Right;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Up;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = -Vector3d.One;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Left;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = Vector3d.Down;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);

            a = new Vector3d(72.00231159357315d, 67.76451324678221d, 42.29832471921837d);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = new Vector3d(-72.00231159357315d, -67.76451324678221d, -42.29832471921837d);

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude, Mathd.EpsilonE15);

            a = Vector3d.MaxValue;

            // vectors of infinite length are zero length when normalised

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector3d.MinValue;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector3d.PositiveInfinity;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);

            a = Vector3d.NegativeInfinity;

            a.Normalise();

            TestUtilities.AssertThatDoublesAreEqual(0d, a.Magnitude);
        }

        [Test]
        public void TestNormalizeMethod()
        {
            // we know normalize just expands to normalise
            var a = Vector3d.One;

            a.Normalize();

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Magnitude);
        }

        [Test]
        public void TestLerpMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, Vector3d.Lerp(a, b, 0d));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One * 0.25d, Vector3d.Lerp(a, b, 0.25d));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One * 0.5d, Vector3d.Lerp(a, b, 0.5d));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One * 0.75d, Vector3d.Lerp(a, b, 0.75d));
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, Vector3d.Lerp(a, b, 1d));
        }

        [Test]
        public void TestLiesBetweenMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.One;

            Assert.IsTrue(Vector3d.LiesBetween(Vector3d.Zero, a, b));
            Assert.IsTrue(Vector3d.LiesBetween(Vector3d.One * 0.25d, a, b));
            Assert.IsTrue(Vector3d.LiesBetween(Vector3d.One * 0.5d, a, b));
            Assert.IsTrue(Vector3d.LiesBetween(Vector3d.One * 0.75d, a, b));
            Assert.IsTrue(Vector3d.LiesBetween(Vector3d.One, a, b));

            Assert.IsFalse(Vector3d.LiesBetween(Vector3d.Up, a, b));
            Assert.IsFalse(Vector3d.LiesBetween(Vector3d.Right, a, b));
            Assert.IsFalse(Vector3d.LiesBetween(Vector3d.Down, a, b));
            Assert.IsFalse(Vector3d.LiesBetween(Vector3d.Left, a, b));
        }

        [Test]
        public void TestTimeBetweenMethod()
        {
            var a = Vector3d.Zero;
            var b = Vector3d.One;

            Assert.AreEqual(0d, Vector3d.TimeBetween(Vector3d.Zero, a, b));
            Assert.AreEqual(0.25d, Vector3d.TimeBetween(Vector3d.One * 0.25d, a, b));
            Assert.AreEqual(0.5d, Vector3d.TimeBetween(Vector3d.One * 0.5d, a, b));
            Assert.AreEqual(0.75d, Vector3d.TimeBetween(Vector3d.One * 0.75d, a, b));
            Assert.AreEqual(1d, Vector3d.TimeBetween(Vector3d.One, a, b));

            Assert.Throws<ArithmeticException>(() => { Vector3d.TimeBetween(Vector3d.Up, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector3d.TimeBetween(Vector3d.Right, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector3d.TimeBetween(Vector3d.Down, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector3d.TimeBetween(Vector3d.Left, a, b); });
        }
    }
}