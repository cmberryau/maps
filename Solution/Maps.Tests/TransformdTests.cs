using NUnit.Framework;

namespace Maps.Tests
{
    [TestFixture]
    internal sealed class TransformdTests
    {
        [Test]
        public void TestIdentity()
        {
            var t = Transformd.Identity;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, t.LocalPosition);
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Zero, t.Position);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, t.LocalScale);
            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.One, t.Scale);

            TestUtilities.AssertThatQuaternionsAreEqual(Quaterniond.Identity, t.Rotation);
            TestUtilities.AssertThatQuaternionsAreEqual(Quaterniond.Identity, t.LocalRotation);
        }
    }
}