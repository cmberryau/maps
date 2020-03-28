using System;
using NUnit.Framework;

namespace Maps.Tests
{
    /// <summary>
    /// Series of tests for the Vector3f class, appearing in order of
    /// members in class
    /// </summary>
    [TestFixture]
    internal sealed class Vector3fTests
    {
        [Test]
        public void TestConstructor()
        {
            var a = new Vector3f(0f, 0f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(0f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(0f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[2]);

            a = new Vector3f(1f, 1f, 1f);

            TestUtilities.AssertThatSinglesAreEqual(1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(1f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(1f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(1f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(1f, a[2]);

            a = new Vector3f(1f, 0f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[2]);

            a = new Vector3f(0f, 1f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(0f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(1f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(0f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(1f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[2]);

            a = new Vector3f(0f, 0f, 1f);

            TestUtilities.AssertThatSinglesAreEqual(0f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(1f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(0f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(1f, a[2]);

            a = new Vector3f(-1f, -1f, -1f);

            TestUtilities.AssertThatSinglesAreEqual(-1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(-1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a[2]);

            a = new Vector3f(-1f, 0f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(-1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(-1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[2]);

            a = new Vector3f(0f, -1f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(0f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(0f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[2]);

            a = new Vector3f(0f, 0f, -1f);

            TestUtilities.AssertThatSinglesAreEqual(0f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(0f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a[2]);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(67.76451f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(42.29832f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(67.76451f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(42.29832f, a[2]);

            a = new Vector3f(-72.00231f, -67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(-67.76451f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(-42.29832f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(-67.76451f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(-42.29832f, a[2]);

            a = new Vector3f(float.MaxValue, float.MaxValue, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a.y);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a.z);

            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a[2]);

            a = new Vector3f(float.MinValue, float.MinValue, float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a.y);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a.z);

            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a[2]);

            a = new Vector3f(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.y);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.z);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a[2]);

            a = new Vector3f(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a.y);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a.z);

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a[2]);
        }

        [Test]
        public void TestSingleParameterConstructor()
        {
            var a = new Vector3f(0f);

            TestUtilities.AssertThatSinglesAreEqual(0f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(0f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(0f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(0f, a[2]);

            a = new Vector3f(1f);

            TestUtilities.AssertThatSinglesAreEqual(1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(1f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(1f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(1f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(1f, a[2]);

            a = new Vector3f(-1f);

            TestUtilities.AssertThatSinglesAreEqual(-1f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(-1f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(-1f, a[2]);

            a = new Vector3f(72.00231f);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a[2]);

            a = new Vector3f(-72.00231f);

            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a.x);
            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a.y);
            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a.z);

            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(-72.00231f, a[2]);

            a = new Vector3f(float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a.y);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a.z);

            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(float.MaxValue, a[2]);

            a = new Vector3f(float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a.y);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a.z);

            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(float.MinValue, a[2]);

            a = new Vector3f(float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.y);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.z);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a[2]);

            a = new Vector3f(float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a.x);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a.y);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a.z);

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a[0]);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a[1]);
            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, a[2]);
        }

        [Test]
        public void TestIndexAccessor()
        {
            var a = Vector3f.Zero;

            Assert.Throws<IndexOutOfRangeException>(() => { var d = a[3]; });
            Assert.Throws<IndexOutOfRangeException>(() => { var d = a[-1]; });
        }

        [Test]
        public void TestMinComponentProperty()
        {
            var a = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MinComponent);

            a = Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.MinComponent);

            a = Vector3f.Right;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MinComponent);

            a = Vector3f.Up;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MinComponent);

            a = Vector3f.Forward;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MinComponent);

            a = Vector3f.Left;

            TestUtilities.AssertThatSinglesAreEqual(-1f, a.MinComponent);

            a = Vector3f.Down;

            TestUtilities.AssertThatSinglesAreEqual(-1f, a.MinComponent);

            a = Vector3f.Back;

            TestUtilities.AssertThatSinglesAreEqual(-1f, a.MinComponent);
        }

        [Test]
        public void TestMaxComponentProperty()
        {
            var a = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MaxComponent);

            a = Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.MaxComponent);

            a = Vector3f.Right;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.MaxComponent);

            a = Vector3f.Up;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.MaxComponent);

            a = Vector3f.Forward;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.MaxComponent);

            a = Vector3f.Left;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MaxComponent);

            a = Vector3f.Down;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MaxComponent);

            a = Vector3f.Back;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.MaxComponent);
        }

        [Test]
        public void TestMagnitudeProperty()
        {
            var a = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(1.73205f, a.Magnitude, Mathf.EpsilonE6);

            a = -Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(1.73205f, a.Magnitude, Mathf.EpsilonE6);

            a = Vector3f.Right;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector3f.Left;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector3f.Up;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector3f.Down;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(107.5430f, a.Magnitude, Mathf.EpsilonE4);

            a = new Vector3f(72.00231f, 67.76451f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(98.87548f, a.Magnitude);

            a = new Vector3f(72.00231f, 0f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, a.Magnitude);

            a = new Vector3f(-72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(107.5430f, a.Magnitude, Mathf.EpsilonE4);

            a = new Vector3f(72.00231f, -67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(107.5430f, a.Magnitude, Mathf.EpsilonE4);

            a = new Vector3f(72.00231f, 67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(107.5430f, a.Magnitude, Mathf.EpsilonE4);

            a = new Vector3f(-72.00231f, -67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(107.5430f, a.Magnitude, Mathf.EpsilonE4);

            a = new Vector3f(72.00231f, -67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(107.5430f, a.Magnitude, Mathf.EpsilonE4);

            a = new Vector3f(-72.00231f, 67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(107.5430f, a.Magnitude, Mathf.EpsilonE4);

            a = new Vector3f(72.00231f, -67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(107.5430f, a.Magnitude, Mathf.EpsilonE4);

            a = new Vector3f(-72.00231f, -67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(107.5430f, a.Magnitude, Mathf.EpsilonE4);

            a = Vector3f.MaxValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = Vector3f.MinValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.MinValue, float.MinValue, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.MinValue, float.MaxValue, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.MaxValue, float.MaxValue, float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.MaxValue, float.MinValue, float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.MaxValue, float.MinValue, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.MinValue, float.MaxValue, float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.NegativeInfinity, float.NegativeInfinity, float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.NegativeInfinity, float.PositiveInfinity, float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.PositiveInfinity, float.PositiveInfinity, float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.PositiveInfinity, float.NegativeInfinity, float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.PositiveInfinity, float.NegativeInfinity, float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);

            a = new Vector3f(float.NegativeInfinity, float.PositiveInfinity, float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.Magnitude);
        }

        [Test]
        public void TestSqrMagnitudeProperty()
        {
            var a = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.SqrMagnitude);

            a = Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(3f, a.SqrMagnitude);

            a = -Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(3f, a.SqrMagnitude);

            a = Vector3f.Right;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.SqrMagnitude);

            a = Vector3f.Left;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.SqrMagnitude);

            a = Vector3f.Up;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.SqrMagnitude);

            a = Vector3f.Down;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.SqrMagnitude);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(11565.51f, a.SqrMagnitude);

            a = new Vector3f(72.00231f, 67.76451f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(9776.362f, a.SqrMagnitude, Mathf.EpsilonE3);

            a = new Vector3f(72.00231f, 0f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(5184.332f, a.SqrMagnitude, Mathf.EpsilonE3);

            a = new Vector3f(-72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(11565.51f, a.SqrMagnitude);

            a = new Vector3f(72.00231f, -67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(11565.51f, a.SqrMagnitude);

            a = new Vector3f(72.00231f, 67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(11565.51f, a.SqrMagnitude);

            a = new Vector3f(-72.00231f, -67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(11565.51f, a.SqrMagnitude);

            a = new Vector3f(72.00231f, -67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(11565.51f, a.SqrMagnitude);

            a = new Vector3f(-72.00231f, 67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(11565.51f, a.SqrMagnitude);

            a = new Vector3f(72.00231f, -67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(11565.51f, a.SqrMagnitude);

            a = new Vector3f(-72.00231f, -67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(11565.51f, a.SqrMagnitude);

            a = Vector3f.MaxValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = Vector3f.MinValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.MinValue, float.MinValue, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.MinValue, float.MaxValue, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.MaxValue, float.MaxValue, float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.MaxValue, float.MinValue, float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.MaxValue, float.MinValue, float.MaxValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.MinValue, float.MaxValue, float.MinValue);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.NegativeInfinity, float.NegativeInfinity, float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.NegativeInfinity, float.PositiveInfinity, float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.PositiveInfinity, float.PositiveInfinity, float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.PositiveInfinity, float.NegativeInfinity, float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.PositiveInfinity, float.NegativeInfinity, float.PositiveInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);

            a = new Vector3f(float.NegativeInfinity, float.PositiveInfinity, float.NegativeInfinity);

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, a.SqrMagnitude);
        }

        [Test]
        public void TestXySwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(a.x, a.y), a.xy);
        }

        [Test]
        public void TestXzSwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(a.x, a.z), a.xz);
        }

        [Test]
        public void TestYxSwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(a.y, a.x), a.yx);
        }

        [Test]
        public void TestYzSwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(a.y, a.z), a.yz);
        }

        [Test]
        public void TestZxSwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(a.z, a.x), a.zx);
        }

        [Test]
        public void TestZySwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector2d32sAreEqual(new Vector2f(a.z, a.y), a.zy);
        }

        [Test]
        public void TestXyzSwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(a.x, a.y, a.z), a.xyz);
        }

        [Test]
        public void TestXzySwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(a.x, a.z, a.y), a.xzy);
        }

        [Test]
        public void TestYxzSwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(a.y, a.x, a.z), a.yxz);
        }

        [Test]
        public void TestYzxSwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(a.y, a.z, a.x), a.yzx);
        }

        [Test]
        public void TestZyxSwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(a.z, a.y, a.x), a.zyx);
        }

        [Test]
        public void TestZxySwizzleProperty()
        {
            var a = new Vector3f(1f, -1f, 0f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(a.z, a.x, a.y), a.zxy);
        }

        [Test]
        public void TestNormalisedProperty()
        {
            var a = Vector3f.Zero;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector3f.One;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = Vector3f.Right;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector3f.Up;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = -Vector3f.One;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = Vector3f.Left;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector3f.Down;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = new Vector3f(-72.00231f, -67.76451f, -42.29832f);

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            // vectors of infinite length are zero length when normalised

            a = Vector3f.MinValue;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector3f.MaxValue;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector3f.PositiveInfinity;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector3f.NegativeInfinity;

            a = a.Normalised;

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);
        }

        [Test]
        public void TestUnaryNegationOperator()
        {
            var a = Vector3f.Zero;
            var aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.One;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = -Vector3f.One;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.Right;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.Left;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.Up;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.Down;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.Forward;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.Back;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = new Vector3f(-72.00231f, -67.76451f, -42.29832f);
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.MaxValue;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.MinValue;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.PositiveInfinity;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);

            a = Vector3f.NegativeInfinity;
            aNegative = -a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-a.x, -a.y, -a.z), aNegative);
        }

        [Test]
        public void TestSubtrationOperator()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;
            var c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.One;
            b = Vector3f.One;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.Zero;
            b = Vector3f.One;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, c);

            a = Vector3f.One;
            b = Vector3f.Zero;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            a = Vector3f.One;
            b = Vector3f.Up;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(1f, 0f, 1f),
                                                      c);

            a = Vector3f.Zero;
            b = Vector3f.Right;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Left, c);

            a = Vector3f.Zero;
            b = Vector3f.Up;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Down, c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = Vector3f.Zero;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                                   c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.Zero;
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-72.00231f,
                                                                   -67.76451f,
                                                                   -42.29832f),
                                                                   c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(0.00231f, 0.76451f, 0.29832f);
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72f, 67f, 42f), c);

            a = Vector3f.MaxValue;
            b = Vector3f.MaxValue;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.MinValue;
            b = Vector3f.MinValue;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.MaxValue;
            b = Vector3f.MinValue;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, c);

            a = Vector3f.MinValue;
            b = Vector3f.MaxValue;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);

            a = Vector3f.PositiveInfinity;
            b = Vector3f.PositiveInfinity;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.NegativeInfinity;
            b = Vector3f.NegativeInfinity;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.PositiveInfinity;
            b = Vector3f.NegativeInfinity;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, c);

            a = Vector3f.NegativeInfinity;
            b = Vector3f.PositiveInfinity;
            c = a - b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);
        }

        [Test]
        public void TestAdditionOperator()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;
            var c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.One;
            b = Vector3f.One;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(2f, 2f, 2f), c);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(2f, 2f, 2f), c);

            a = Vector3f.Zero;
            b = Vector3f.One;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            a = Vector3f.One;
            b = Vector3f.Zero;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            a = Vector3f.One;
            b = Vector3f.Up;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(1f, 2f, 1f), c);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(1f, 2f, 1f), c);

            a = Vector3f.Zero;
            b = Vector3f.Right;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Right, c);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Right, c);

            a = Vector3f.Zero;
            b = Vector3f.Up;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Up, c);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Up, c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = Vector3f.Zero;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                                   c);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                                   c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(144.0046f,
                                                                   135.529f,
                                                                   84.59664f),
                                                                   c,
                                                                   Mathf.EpsilonE4);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(144.0046f,
                                                                   135.529f,
                                                                   84.59664f),
                                                                   c,
                                                                   Mathf.EpsilonE4);

            a = Vector3f.Zero;
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                                   c);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                                   c);

            a = new Vector3f(72f, 67f, 42f);
            b = new Vector3f(0.00231159357315f, 0.76451324678221f, 0.29832471921837f);
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                                   c,
                                                                   Mathf.EpsilonE5);

            c = b + a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                                   c,
                                                                   Mathf.EpsilonE5);

            a = Vector3f.MaxValue;
            b = Vector3f.MaxValue;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, c);

            a = Vector3f.MinValue;
            b = Vector3f.MinValue;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);

            a = Vector3f.MaxValue;
            b = Vector3f.MinValue;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.MinValue;
            b = Vector3f.MaxValue;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.PositiveInfinity;
            b = Vector3f.PositiveInfinity;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.NegativeInfinity;
            b = Vector3f.NegativeInfinity;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.PositiveInfinity;
            b = Vector3f.NegativeInfinity;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, c);

            a = Vector3f.NegativeInfinity;
            b = Vector3f.PositiveInfinity;
            c = a + b;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);
        }

        [Test]
        public void TestMultiplicationOperator()
        {
            var a = Vector3f.Zero;
            var c = a * 0f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            c = 0f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.One;
            c = a * 1f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            c = 1f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            a = Vector3f.Zero;
            c = a * 1f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            c = 1f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.One;
            c = a * 0f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            c = 0f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = Vector3f.Up;
            c = a * 1f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Up, c);

            c = 1f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Up, c);

            a = Vector3f.Right;
            c = a * 1f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Right, c);

            c = 1f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Right, c);

            a = Vector3f.One;
            c = a * -1f;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, c);

            c = -1f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, c);

            a = Vector3f.One;
            c = a * 2f;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(2f, 2f, 2f), c);

            c = 2f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(2f, 2f, 2f), c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a * 0f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            c = 0f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a * 1f;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                                   c);

            c = 1f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                                   c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a * 2f;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(144.0046f,
                                                                   135.529f,
                                                                   84.59664f),
                                                                   c,
                                                                   Mathf.EpsilonE4);

            c = 2f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(144.0046f,
                                                                   135.529f,
                                                                   84.59664f),
                                                                   c,
                                                                   Mathf.EpsilonE4);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a * -1f;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-72.00231f,
                                                                   -67.76451f,
                                                                   -42.29832f),
                                                                   c);

            c = -1f * a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-72.00231f,
                                                                   -67.76451f,
                                                                   -42.29832f),
                                                                   c);

            a = Vector3f.MaxValue;
            c = a * float.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, c);

            c = float.MaxValue * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, c);

            a = Vector3f.MinValue;
            c = a * float.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, c);

            c = float.MinValue * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, c);

            a = Vector3f.MaxValue;
            c = a * float.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);

            c = float.MinValue * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);

            a = Vector3f.MinValue;
            c = a * float.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);

            c = float.MaxValue * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);

            a = Vector3f.PositiveInfinity;
            c = a * float.PositiveInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            c = float.PositiveInfinity * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.NegativeInfinity;
            c = a * float.NegativeInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            c = float.NegativeInfinity * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.PositiveInfinity;
            c = a * float.NegativeInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);

            c = float.NegativeInfinity * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);

            a = Vector3f.NegativeInfinity;
            c = a * float.PositiveInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);

            c = float.PositiveInfinity * a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);
        }

        [Test]
        public void TestDivisionOperator()
        {
            var a = Vector3f.Zero;
            var c = a / 0f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.One;
            c = a / 1f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            c = 1f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            a = Vector3f.Zero;
            c = a / 1f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            c = 1f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.One;
            c = a / 0f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            c = 0f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.Up;
            c = a / 1f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Up, c);

            c = 1f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.Right;
            c = a / 1f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Right, c);

            c = 1f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.One;
            c = a / -1f;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, c);

            c = -1f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.One;
            c = a / 2f;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(0.5f, 0.5f, 0.5f), c);

            c = 2f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(2f, 2f, 2f), c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a / 0f;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            c = 0f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a / 1f;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                                   c);

            c = 1f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(1f / 72.00231f,
                                                                   1f / 67.76451f,
                                                                   1f / 42.29832f),
                                                                   c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a / 2f;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f / 2f,
                                                                   67.76451f / 2f,
                                                                   42.29832f / 2f),
                                                                   c);

            c = 2f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(2f / 72.00231f,
                                                                   2f / 67.76451f,
                                                                   2f / 42.29832f),
                                                                   c);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            c = a / -1f;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-72.00231f,
                                                                   -67.76451f,
                                                                   -42.29832f),
                                                                   c);

            c = -1f / a;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-1f / 72.00231f,
                                                                   -1f / 67.76451f,
                                                                   -1f / 42.29832f),
                                                                   c);

            a = Vector3f.MaxValue;
            c = a / float.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            a = Vector3f.MinValue;
            c = a / float.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, c);

            a = Vector3f.MaxValue;
            c = a / float.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, c);

            a = Vector3f.MinValue;
            c = a / float.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, c);

            a = Vector3f.PositiveInfinity;
            c = a / float.PositiveInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.NegativeInfinity;
            c = a / float.NegativeInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NaN, c);

            a = Vector3f.PositiveInfinity;
            c = a / float.NegativeInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, c);

            a = Vector3f.NegativeInfinity;
            c = a / float.PositiveInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, c);
        }

        [Test]
        public void TestEqualityOperator()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.One;
            b = Vector3f.One;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.Right;
            b = Vector3f.Right;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.Up;
            b = Vector3f.Up;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = -Vector3f.One;
            b = -Vector3f.One;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.Left;
            b = Vector3f.Left;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.Down;
            b = Vector3f.Down;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector3f(-72.00231f, -67.76451f, -42.29832f);
            b = new Vector3f(-72.00231f, -67.76451f, -42.29832f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector3f(-72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(-72.00231f, 67.76451f, 42.29832f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector3f(72.00231f, -67.76451f, 42.29832f);
            b = new Vector3f(72.00231f, -67.76451f, 42.29832f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.PositiveInfinity;
            b = Vector3f.PositiveInfinity;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.NegativeInfinity;
            b = Vector3f.NegativeInfinity;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.MaxValue;
            b = Vector3f.MaxValue;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.MinValue;
            b = Vector3f.MinValue;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [Test]
        public void TestInequalityOperator()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.One;
            b = Vector3f.One;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = Vector3f.Right;
            b = Vector3f.Up;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector3f.Up;
            b = Vector3f.Right;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = new Vector3f(-1f, 1f, -1f);
            b = new Vector3f(1f, -1f, 1f);

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector3f.Left;
            b = Vector3f.Down;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = Vector3f.Down;
            b = Vector3f.Left;

            Assert.IsTrue(a != b);
            Assert.IsFalse(a == b);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            a = new Vector3f(-72.00231f, -67.76451f, -42.29832f);
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = new Vector3f(-72.00231f, 67.76451f, -42.29832f);
            b = new Vector3f(72.00231f, -67.76451f, 42.29832f);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = new Vector3f(72.00231f, -67.76451f, 42.29832f);
            b = new Vector3f(-72.00231f, 67.76451f, -42.29832f);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector3f.PositiveInfinity;
            b = Vector3f.NegativeInfinity;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector3f.NegativeInfinity;
            b = Vector3f.PositiveInfinity;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector3f.MaxValue;
            b = Vector3f.MinValue;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            a = Vector3f.MinValue;
            b = Vector3f.MaxValue;

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);
        }

        [Test]
        public void TestOneProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(1f, 1f, 1f), Vector3f.One);
        }

        [Test]
        public void TestZeroProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(0f, 0f, 0f), Vector3f.Zero);
        }

        [Test]
        public void TestUpProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(0f, 1f, 0f), Vector3f.Up);
        }

        [Test]
        public void TestDownProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(0f, -1f, 0f), Vector3f.Down);
        }

        [Test]
        public void TestRightProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(1f, 0f, 0f), Vector3f.Right);
        }

        [Test]
        public void TestLeftProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-1f, 0f, 0f), Vector3f.Left);
        }

        [Test]
        public void TestForwardProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(0f, 0f, 1f), Vector3f.Forward);
        }

        [Test]
        public void TestBackProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(0f, 0f, -1f), Vector3f.Back);
        }

        [Test]
        public void TestMaxValueProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(float.MaxValue,
                                                                   float.MaxValue,
                                                                   float.MaxValue),
                                                      Vector3f.MaxValue);
        }

        [Test]
        public void TestMinValueProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(float.MinValue,
                                                                   float.MinValue,
                                                                   float.MinValue),
                                                      Vector3f.MinValue);
        }

        [Test]
        public void TestPositiveInfinityProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(float.PositiveInfinity,
                                                                   float.PositiveInfinity,
                                                                   float.PositiveInfinity),
                                                      Vector3f.PositiveInfinity);
        }

        [Test]
        public void TestNegativeInfinityProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(float.NegativeInfinity,
                                                                   float.NegativeInfinity,
                                                                   float.NegativeInfinity),
                                                      Vector3f.NegativeInfinity);
        }

        [Test]
        public void TestNaNProperty()
        {
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(float.NaN,
                                                                   float.NaN,
                                                                   float.NaN),
                                                      Vector3f.NaN);
        }

        [Test]
        public void TestIsNaNMethod()
        {
            var a = Vector3f.One;

            Assert.IsFalse(Vector3f.IsNaN(a));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            Assert.IsFalse(Vector3f.IsNaN(a));

            a = Vector3f.NaN;

            Assert.IsTrue(Vector3f.IsNaN(a));
        }

        [Test]
        public void TestComponentMultiplyMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.ComponentMultiply(a, b));

            a = Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.ComponentMultiply(a, b));

            a = Vector3f.Zero;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.ComponentMultiply(a, b));

            a = Vector3f.One;
            b = -Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, Vector3f.ComponentMultiply(a, b));

            a = Vector3f.One;
            b = new Vector3f(-1f, 1f, -1f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-1f, 1f, -1f),
                                                      Vector3f.ComponentMultiply(a, b));

            a = Vector3f.One;
            b = new Vector3f(1f, -1f, 1f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(1f, -1f, 1f),
                                                      Vector3f.ComponentMultiply(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(-1f, 1f, -1f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-72.00231f,
                                                                   67.76451f,
                                                                   -42.29832f),
                                                      Vector3f.ComponentMultiply(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(1f, -1f, 1f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   -67.76451f,
                                                                   42.29832f),
                                                      Vector3f.ComponentMultiply(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(2f, 2f, 2f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(144.0046f,
                                                                   135.529f,
                                                                   84.59664f),
                                                      Vector3f.ComponentMultiply(a, b),
                                                      Mathf.EpsilonE4);
        }

        [Test]
        public void TestPowMethod()
        {
            var a = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Pow(a, 0f));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Pow(a, 1f));

            a = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Pow(a, 0f));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Pow(a, 1f));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Pow(a, 2f));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Pow(a, 3f));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Pow(a, 0f));
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                      Vector3f.Pow(a, 1f));

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(5184.332f,
                                                                   4592.029f,
                                                                   1789.148f),
                                                      Vector3f.Pow(a, 2f),
                                                      Mathf.EpsilonE3);
        }

        [Test]
        public void TestDistanceMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector3f.Distance(a, b));

            a = Vector3f.Zero;
            b = Vector3f.Up;

            TestUtilities.AssertThatSinglesAreEqual(1f, Vector3f.Distance(a, b));

            a = Vector3f.Zero;
            b = Vector3f.Right;

            TestUtilities.AssertThatSinglesAreEqual(1f, Vector3f.Distance(a, b));

            a = Vector3f.Zero;
            b = Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(Mathf.Sqrt(3f), Vector3f.Distance(a, b));

            a = Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector3f.Distance(a, b));

            a = Vector3f.Left;
            b = Vector3f.Right;

            TestUtilities.AssertThatSinglesAreEqual(2f, Vector3f.Distance(a, b));

            a = Vector3f.Down;
            b = Vector3f.Up;

            TestUtilities.AssertThatSinglesAreEqual(2f, Vector3f.Distance(a, b));

            a = Vector3f.Forward;
            b = Vector3f.Back;

            TestUtilities.AssertThatSinglesAreEqual(2f, Vector3f.Distance(a, b));

            a = -Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(Mathf.Sqrt(3f) * 2f, Vector3f.Distance(a, b));

            a = new Vector3f(72.00231f, 0f, 0f);
            b = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, Vector3f.Distance(a, b));

            a = Vector3f.Zero;
            b = new Vector3f(72.00231f, 0f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(72.00231f, Vector3f.Distance(a, b));

            a = new Vector3f(0f, 67.76451f, 0f);
            b = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(67.76451f, Vector3f.Distance(a, b));

            a = Vector3f.Zero;
            b = new Vector3f(0f, 67.76451f, 0f);

            TestUtilities.AssertThatSinglesAreEqual(67.76451f, Vector3f.Distance(a, b));

            a = Vector3f.Zero;
            b = new Vector3f(0f, 0f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(42.29832f, Vector3f.Distance(a, b));

            a = new Vector3f(0f, 0f, 42.29832f);
            b = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(42.29832f, Vector3f.Distance(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(107.543f,
                                                    Vector3f.Distance(a, b),
                                                    Mathf.EpsilonE4);

            a = Vector3f.Zero;
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(107.543f,
                                                    Vector3f.Distance(a, b),
                                                    Mathf.EpsilonE4);

            a = Vector3f.PositiveInfinity;
            b = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector3f.Distance(a, b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector3f.Distance(a, b));

            a = Vector3f.PositiveInfinity;
            b = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector3f.Distance(a, b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector3f.Distance(a, b));

            a = Vector3f.MaxValue;
            b = Vector3f.MaxValue;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector3f.Distance(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.MinValue;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector3f.Distance(a, b));

            a = Vector3f.MaxValue;
            b = Vector3f.MinValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector3f.Distance(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.MaxValue;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector3f.Distance(a, b));
        }

        [Test]
        public void TestComponentDistanceMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.ComponentDistance(a, b));

            a = Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.ComponentDistance(a, b));

            a = Vector3f.One;
            b = -Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(2f, 2f, 2f),
                                                      Vector3f.ComponentDistance(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                      Vector3f.ComponentDistance(a, b));

            a = Vector3f.Zero;
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(72.00231f,
                                                                   67.76451f,
                                                                   42.29832f),
                                                      Vector3f.ComponentDistance(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(-72.00231f, -67.76451f, -42.29832f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(144.0046f,
                                                                   135.529f,
                                                                   84.59664f),
                                                      Vector3f.ComponentDistance(a, b),
                                                      Mathf.EpsilonE4);
        }

        [Test]
        public void TestMidpointMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(0.5f, 0.5f, 0.5f),
                                                      Vector3f.Midpoint(a, b));

            a = Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Midpoint(a, b));

            a = Vector3f.One;
            b = -Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Midpoint(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(36.00115f,
                                                                   33.88225f,
                                                                   21.14916f),
                                                      Vector3f.Midpoint(a, b),
                                                      Mathf.EpsilonE5);

            a = Vector3f.Zero;
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(36.00115f,
                                                                   33.88225f,
                                                                   21.14916f),
                                                      Vector3f.Midpoint(a, b),
                                                      Mathf.EpsilonE5);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(-72.00231f, -67.76451f, -42.29832f);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Midpoint(a, b));
        }

        [Test]
        public void TestScaleMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Scale(a, b));

            a = Vector3f.Zero;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Scale(a, b));

            a = Vector3f.One;
            b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Scale(a, b));

            a = Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Scale(a, b));

            a = Vector3f.One;
            b = -Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, Vector3f.Scale(a, b));

            a = Vector3f.One;
            b = new Vector3f(2f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(2f), Vector3f.Scale(a, b));

            a = new Vector3f(2f);
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(2f), Vector3f.Scale(a, b));

            a = Vector3f.One;
            b = new Vector3f(-2f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-2f), Vector3f.Scale(a, b));

            a = new Vector3f(-2f);
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-2f), Vector3f.Scale(a, b));

            a = Vector3f.MaxValue;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.MaxValue, Vector3f.Scale(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.MinValue, Vector3f.Scale(a, b));

            a = Vector3f.MaxValue;
            b = Vector3f.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, Vector3f.Scale(a, b));

            a = Vector3f.MaxValue;
            b = Vector3f.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, Vector3f.Scale(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, Vector3f.Scale(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, Vector3f.Scale(a, b));
        }

        [Test]
        public void TestMaxMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Max(a, b));

            a = Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Max(a, b));

            a = -Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Max(a, b));

            a = Vector3f.One;
            b = -Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Max(a, b));

            a = Vector3f.Zero;
            b = -Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Max(a, b));

            a = -Vector3f.One;
            b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Max(a, b));

            a = Vector3f.One;
            b = Vector3f.One * 2;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One * 2, Vector3f.Max(a, b));

            a = Vector3f.One * 2;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One * 2, Vector3f.Max(a, b));

            a = Vector3f.Up;
            b = Vector3f.Right;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(1f, 1f, 0f),
                                                      Vector3f.Max(a, b));

            a = Vector3f.Right;
            b = Vector3f.Up;

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(1f, 1f, 0f),
                                                      Vector3f.Max(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(a, Vector3f.Max(a, b));

            a = Vector3f.Zero;
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatVector3d32sAreEqual(b, Vector3f.Max(a, b));

            a = Vector3f.PositiveInfinity;
            b = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, Vector3f.Max(a, b));

            a = Vector3f.PositiveInfinity;
            b = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, Vector3f.Max(a, b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, Vector3f.Max(a, b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, Vector3f.Max(a, b));

            a = Vector3f.MaxValue;
            b = Vector3f.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.MaxValue, Vector3f.Max(a, b));

            a = Vector3f.MaxValue;
            b = Vector3f.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.MaxValue, Vector3f.Max(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.MaxValue, Vector3f.Max(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.MinValue, Vector3f.Max(a, b));
        }

        [Test]
        public void TestMinMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Min(a, b));

            a = Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Min(a, b));

            a = -Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, Vector3f.Min(a, b));

            a = Vector3f.One;
            b = -Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, Vector3f.Min(a, b));

            a = Vector3f.Zero;
            b = -Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, Vector3f.Min(a, b));

            a = -Vector3f.One;
            b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(-Vector3f.One, Vector3f.Min(a, b));

            a = Vector3f.One;
            b = Vector3f.One * 2;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Min(a, b));

            a = Vector3f.One * 2;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Min(a, b));

            a = Vector3f.Up;
            b = Vector3f.Right;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Min(a, b));

            a = Vector3f.Right;
            b = Vector3f.Up;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Min(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Min(a, b));

            a = Vector3f.Zero;
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Min(a, b));

            a = Vector3f.PositiveInfinity;
            b = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity, Vector3f.Min(a, b));

            a = Vector3f.PositiveInfinity;
            b = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, Vector3f.Min(a, b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, Vector3f.Min(a, b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity, Vector3f.Min(a, b));

            a = Vector3f.MaxValue;
            b = Vector3f.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.MaxValue, Vector3f.Min(a, b));

            a = Vector3f.MaxValue;
            b = Vector3f.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.MinValue, Vector3f.Min(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.MinValue, Vector3f.Min(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.MinValue, Vector3f.Min(a, b));
        }

        [Test]
        public void TestToStringMethod()
        {
            var a = Vector3f.Zero;
            Assert.AreEqual(@"[0]f,[0]f,[0]f", a.ToString());

            a = Vector3f.One;
            Assert.AreEqual(@"[1]f,[1]f,[1]f", a.ToString());

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            Assert.AreEqual(@"[72.00231]f,[67.76451]f,[42.29832]f", a.ToString());

            a = Vector3f.PositiveInfinity;
            Assert.AreEqual(@"[Infinity]f,[Infinity]f,[Infinity]f", a.ToString());

            a = Vector3f.NegativeInfinity;
            Assert.AreEqual(@"[-Infinity]f,[-Infinity]f,[-Infinity]f", a.ToString());
        }

        [Test]
        public void TestEqualsMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            Assert.IsTrue(a.Equals(b));

            a = Vector3f.One;
            b = Vector3f.One;

            Assert.IsTrue(a.Equals(b));

            a = Vector3f.Right;
            b = Vector3f.Right;

            Assert.IsTrue(a.Equals(b));

            a = Vector3f.Up;
            b = Vector3f.Up;

            Assert.IsTrue(a.Equals(b));

            a = -Vector3f.One;
            b = -Vector3f.One;

            Assert.IsTrue(a.Equals(b));

            a = Vector3f.Left;
            b = Vector3f.Left;

            Assert.IsTrue(a.Equals(b));

            a = Vector3f.Down;
            b = Vector3f.Down;

            Assert.IsTrue(a.Equals(b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            Assert.IsTrue(a.Equals(b));

            // test rounding up
            b = new Vector3f(72.002309f, 67.764509f, 42.298319f);

            Assert.IsTrue(a.Equals(b));

            // test rounding down
            b = new Vector3f(72.002311f, 67.764511f, 42.298321f);

            Assert.IsTrue(a.Equals(b));

            a = Vector3f.PositiveInfinity;
            b = Vector3f.PositiveInfinity;

            Assert.IsTrue(a.Equals(b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.NegativeInfinity;

            Assert.IsTrue(a.Equals(b));

            a = Vector3f.MaxValue;
            b = Vector3f.MaxValue;

            Assert.IsTrue(a.Equals(b));

            a = Vector3f.MinValue;
            b = Vector3f.MinValue;

            Assert.IsTrue(a.Equals(b));

            var c = "I am not a Vector3d32";

            Assert.IsFalse(a.Equals(c));
        }

        [Test]
        public void TestGetHashCodeMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3f.One;
            b = Vector3f.One;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3f.Right;
            b = Vector3f.Right;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3f.Up;
            b = Vector3f.Up;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = -Vector3f.One;
            b = -Vector3f.One;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3f.Left;
            b = Vector3f.Left;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3f.Down;
            b = Vector3f.Down;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            // test rounding up
            b = new Vector3f(72.002309f, 67.764509f, 42.298319f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            // test rounding down
            b = new Vector3f(72.002311f, 67.764511f, 42.298321f);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3f.PositiveInfinity;
            b = Vector3f.PositiveInfinity;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3f.NegativeInfinity;
            b = Vector3f.NegativeInfinity;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3f.MaxValue;
            b = Vector3f.MaxValue;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());

            a = Vector3f.MinValue;
            b = Vector3f.MinValue;

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void TestDotMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector3f.Dot(a, b));

            a = Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(3f, Vector3f.Dot(a, b));

            a = Vector3f.Zero;
            b = Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector3f.Dot(a, b));

            a = Vector3f.One;
            b = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(0f, Vector3f.Dot(a, b));

            a = Vector3f.One;
            b = -Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(-3f, Vector3f.Dot(a, b));

            a = -Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatSinglesAreEqual(-3f, Vector3f.Dot(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(-72.00231f, 67.76451f, -42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(-2381.451f,
                                                    Vector3f.Dot(a, b),
                                                    Mathf.EpsilonE2);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(72.00231f, -67.76451f, 42.29832f);

            TestUtilities.AssertThatSinglesAreEqual(2381.451f,
                                                    Vector3f.Dot(a, b),
                                                    Mathf.EpsilonE2);

            a = Vector3f.PositiveInfinity;
            b = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector3f.Dot(a, b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.PositiveInfinity, Vector3f.Dot(a, b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, Vector3f.Dot(a, b));

            a = Vector3f.PositiveInfinity;
            b = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatSinglesAreEqual(float.NegativeInfinity, Vector3f.Dot(a, b));
        }

        [Test]
        public void TestCrossMethod()
        {
            // 1 or more zero sized vectors produce zero sized cross product
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));

            // parallel vectors produce zero sided cross product
            a = Vector3f.One;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));

            // 1 or more zero sized vectors produce zero sized cross product
            a = Vector3f.Zero;
            b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(b, a));

            // parallel vectors produce zero sided cross product
            a = Vector3f.One;
            b = -Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(b, a));

            // perpendicular vectors produce full sized cross product
            a = Vector3f.Right;
            b = Vector3f.Up;

            // cross product is not communicative
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Forward, Vector3f.Cross(a, b));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Back, Vector3f.Cross(b, a));

            // parallel vectors produce zero sided cross product
            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(-72.00231f, -67.76451f, -42.29832f);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, a));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(72.00231f, -67.76451f, 42.29832f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(5732.650f,
                                                                   0f,
                                                                   -9758.403f),
                                                      Vector3f.Cross(a, b));

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);
            b = new Vector3f(-72.00231f, 67.76451f, -42.29832f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(-5732.650f,
                                                                   0f,
                                                                   9758.403f),
                                                      Vector3f.Cross(a, b));

            a = Vector3f.PositiveInfinity;
            b = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));

            a = Vector3f.NegativeInfinity;
            b = Vector3f.PositiveInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));

            a = Vector3f.PositiveInfinity;
            b = Vector3f.NegativeInfinity;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));

            a = new Vector3f(float.PositiveInfinity, 0f, 0f);
            b = new Vector3f(0f, float.PositiveInfinity, 0f);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.PositiveInfinity,
                                                      Vector3f.Cross(a, b));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.NegativeInfinity,
                                                      Vector3f.Cross(b, a));

            a = Vector3f.MaxValue;
            b = Vector3f.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.MinValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));

            a = Vector3f.MinValue;
            b = Vector3f.MaxValue;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(a, b));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Cross(b, a));

            a = new Vector3f(float.MaxValue, 0f, 0f);
            b = new Vector3f(0f, float.MaxValue, 0f);

            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(0f,
                                                                   0f,
                                                                   float.PositiveInfinity),
                                                      Vector3f.Cross(a, b));
            TestUtilities.AssertThatVector3d32sAreEqual(new Vector3f(0f,
                                                                   0f,
                                                                   float.NegativeInfinity),
                                                      Vector3f.Cross(b, a));
        }

        [Test]
        public void TestRotateMethod()
        {
            var a = Vector3f.Right;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Up,
                                                      a.Rotate(Vector3f.Forward, 90f),
                                                      Mathf.EpsilonE7);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Left,
                                                      a.Rotate(Vector3f.Forward, 180f),
                                                      Mathf.EpsilonE7);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Down,
                                                      a.Rotate(Vector3f.Forward, 270f),
                                                      Mathf.EpsilonE7);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Right,
                                                      a.Rotate(Vector3f.Forward, 360f),
                                                      Mathf.EpsilonE6);

            a = Vector3f.Forward;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Right,
                                                      a.Rotate(Vector3f.Up, 90f),
                                                      Mathf.EpsilonE7);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Back,
                                                      a.Rotate(Vector3f.Up, 180f),
                                                      Mathf.EpsilonE7);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Left,
                                                      a.Rotate(Vector3f.Up, 270f),
                                                      Mathf.EpsilonE7);

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Forward,
                                                      a.Rotate(Vector3f.Up, 360f),
                                                      Mathf.EpsilonE6);
        }

        [Test]
        public void TestAngleMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(90f, Vector3f.Angle(a, b));

            a = Vector3f.Up;
            b = Vector3f.Zero;

            TestUtilities.AssertThatSinglesAreEqual(90f, Vector3f.Angle(a, b));

            a = Vector3f.Up;
            b = Vector3f.Right;

            TestUtilities.AssertThatSinglesAreEqual(90f, Vector3f.Angle(a, b));

            a = Vector3f.Up;
            b = Vector3f.Left;

            TestUtilities.AssertThatSinglesAreEqual(90f, Vector3f.Angle(a, b));

            a = Vector3f.Up;
            b = Vector3f.Down;

            TestUtilities.AssertThatSinglesAreEqual(180f, Vector3f.Angle(a, b));
        }

        [Test]
        public void TestNormaliseMethod()
        {
            var a = Vector3f.Zero;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector3f.One;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = Vector3f.Right;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector3f.Up;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = -Vector3f.One;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = Vector3f.Left;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = Vector3f.Down;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude);

            a = new Vector3f(72.00231f, 67.76451f, 42.29832f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = new Vector3f(-72.00231f, -67.76451f, -42.29832f);

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);

            a = Vector3f.MaxValue;

            // vectors of infinite length are zero length when normalised

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector3f.MinValue;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector3f.PositiveInfinity;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);

            a = Vector3f.NegativeInfinity;

            a.Normalise();

            TestUtilities.AssertThatSinglesAreEqual(0f, a.Magnitude);
        }

        [Test]
        public void TestNormalizeMethod()
        {
            // we know normalize just expands to normalise
            var a = Vector3f.One;

            a.Normalize();

            TestUtilities.AssertThatSinglesAreEqual(1f, a.Magnitude, Mathf.EpsilonE7);
        }

        [Test]
        public void TestLerpMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.One;

            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.Zero, Vector3f.Lerp(a, b, 0f));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One * 0.25f, Vector3f.Lerp(a, b, 0.25f));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One * 0.5f, Vector3f.Lerp(a, b, 0.5f));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One * 0.75f, Vector3f.Lerp(a, b, 0.75f));
            TestUtilities.AssertThatVector3d32sAreEqual(Vector3f.One, Vector3f.Lerp(a, b, 1f));
        }

        [Test]
        public void TestLiesBetweenMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.One;

            Assert.IsTrue(Vector3f.LiesBetween(Vector3f.Zero, a, b));
            Assert.IsTrue(Vector3f.LiesBetween(Vector3f.One * 0.25f, a, b));
            Assert.IsTrue(Vector3f.LiesBetween(Vector3f.One * 0.5f, a, b));
            Assert.IsTrue(Vector3f.LiesBetween(Vector3f.One * 0.75f, a, b));
            Assert.IsTrue(Vector3f.LiesBetween(Vector3f.One, a, b));

            Assert.IsFalse(Vector3f.LiesBetween(Vector3f.Up, a, b));
            Assert.IsFalse(Vector3f.LiesBetween(Vector3f.Right, a, b));
            Assert.IsFalse(Vector3f.LiesBetween(Vector3f.Down, a, b));
            Assert.IsFalse(Vector3f.LiesBetween(Vector3f.Left, a, b));
        }

        [Test]
        public void TestTimeBetweenMethod()
        {
            var a = Vector3f.Zero;
            var b = Vector3f.One;

            Assert.AreEqual(0f, Vector3f.TimeBetween(Vector3f.Zero, a, b));
            Assert.AreEqual(0.25f, Vector3f.TimeBetween(Vector3f.One * 0.25f, a, b));
            Assert.AreEqual(0.5f, Vector3f.TimeBetween(Vector3f.One * 0.5f, a, b));
            Assert.AreEqual(0.75f, Vector3f.TimeBetween(Vector3f.One * 0.75f, a, b));
            Assert.AreEqual(1f, Vector3f.TimeBetween(Vector3f.One, a, b));

            Assert.Throws<ArithmeticException>(() => { Vector3f.TimeBetween(Vector3f.Up, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector3f.TimeBetween(Vector3f.Right, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector3f.TimeBetween(Vector3f.Down, a, b); });
            Assert.Throws<ArithmeticException>(() => { Vector3f.TimeBetween(Vector3f.Left, a, b); });
        }
    }
}