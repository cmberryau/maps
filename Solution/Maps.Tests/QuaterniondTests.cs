using NUnit.Framework;

namespace Maps.Tests
{
    [TestFixture]
    internal sealed class QuaterniondTests
    {
        [Test]
        public void TestIdentity()
        {
            var p = Vector3d.Zero;
            var q = Quaterniond.Identity;

            TestUtilities.AssertThatVector3dsAreEqual(p, p * q);

            p = Vector3d.Right;
            q = Quaterniond.Identity;

            TestUtilities.AssertThatVector3dsAreEqual(p, p * q);
        }

        [Test]
        public void TestAxisAngleRotation()
        {
            var p = Vector3d.Right;
            var q = Quaterniond.AxisAngle(Vector3d.Down, 90);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Forward, p * q, 
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Down, 180);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left, p * q, 
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Down, 270);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Back, p * q, 
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Down, 360);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right, p * q, 
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Down, -90);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Back, p * q, 
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Down, -180);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left, p * q, 
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Down, -270);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Forward, p * q, 
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Down, -360);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right, p * q, 
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Forward, 90);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up, p * q,
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Forward, 180);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left, p * q,
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Forward, 270);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Down, p * q,
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Forward, 360);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right, p * q,
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Forward, -90);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Down, p * q,
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Forward, -180);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left, p * q,
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Forward, -270);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up, p * q,
                Mathd.EpsilonE15);

            p = Vector3d.Right;
            q = Quaterniond.AxisAngle(Vector3d.Forward, -360);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right, p * q,
                Mathd.EpsilonE15);
        }

        [Test]
        public void AngleAxisToAxisAngleTest()
        {
            var theta = 90;
            var a = Vector3d.Down;
            var q = Quaterniond.AxisAngle(a, theta);

            Vector3d b;
            double phi;

            q.ToAxisAngle(out b, out phi);

            TestUtilities.AssertThatDoublesAreEqual(theta, phi);
            TestUtilities.AssertThatVector3dsAreEqual(a, b);
        }

        [Test]
        public void TestMatrixConstructor()
        {
            var mat = Matrix3d.Identity;
            var q = new Quaterniond(mat);
            TestUtilities.AssertThatQuaternionsAreEqual(Quaterniond.Identity, q);

            mat = new Matrix3d(-1, 0, 0, 0, 1, 0, 0, 0, -1);
            q = new Quaterniond(mat);
            TestUtilities.AssertThatQuaternionsAreEqual(Quaterniond.AxisAngle(
                Vector3d.Up, 180), q, Mathd.EpsilonE16);

            mat = new Matrix3d(-1, 0, 0, 0, 1, 0, 0, 0, -1);
            q = new Quaterniond(mat);
            TestUtilities.AssertThatQuaternionsAreEqual(Quaterniond.AxisAngle(
                Vector3d.Up, 180), q, Mathd.EpsilonE16);

            mat = new Matrix3d(1, 0, 0, 0, -1, 0, 0, 0, -1);
            q = new Quaterniond(mat);
            TestUtilities.AssertThatQuaternionsAreEqual(Quaterniond.AxisAngle(
                Vector3d.Right, 180), q, Mathd.EpsilonE16);

            mat = new Matrix3d(-1, 0, 0, 0, -1, 0, 0, 0, 1);
            q = new Quaterniond(mat);
            TestUtilities.AssertThatQuaternionsAreEqual(Quaterniond.AxisAngle(
                Vector3d.Forward, 180), q, Mathd.EpsilonE16);
        }

        [Test]
        public void TestEulerMethods()
        {
            var q = Quaterniond.Euler(0d, 0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(0d, q.EulerX);
            TestUtilities.AssertThatDoublesAreEqual(0d, q.EulerY);
            TestUtilities.AssertThatDoublesAreEqual(0d, q.EulerZ);

            q = Quaterniond.Euler(1d, 0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(1d, q.EulerX);
            TestUtilities.AssertThatDoublesAreEqual(0d, q.EulerY);
            TestUtilities.AssertThatDoublesAreEqual(0d, q.EulerZ);

            q = Quaterniond.Euler(0d, 1d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(0d, q.EulerX);
            TestUtilities.AssertThatDoublesAreEqual(1d, q.EulerY);
            TestUtilities.AssertThatDoublesAreEqual(0d, q.EulerZ);

            q = Quaterniond.Euler(0d, 0d, 1d);

            TestUtilities.AssertThatDoublesAreEqual(0d, q.EulerX);
            TestUtilities.AssertThatDoublesAreEqual(0d, q.EulerY);
            TestUtilities.AssertThatDoublesAreEqual(1d, q.EulerZ);

            q = Quaterniond.Euler(1d, 0d, 1d);

            TestUtilities.AssertThatDoublesAreEqual(1d, q.EulerX);
            TestUtilities.AssertThatDoublesAreEqual(0d, q.EulerY);
            TestUtilities.AssertThatDoublesAreEqual(1d, q.EulerZ);
        }
    }
}