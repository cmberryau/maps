using System;
using Maps.Geographical;
using Maps.Geometry;
using NUnit.Framework;

namespace Maps.Tests.Geometry
{
    [TestFixture]
    internal sealed class EllipsoidTests
    {
        [Test]
        public void TestWgs84Property()
        {
            var a = Ellipsoid.Wgs84;

            TestUtilities.AssertThatDoublesAreEqual(Mathd.RMajor, a.Radii.x);
            TestUtilities.AssertThatDoublesAreEqual(Mathd.RMajor, a.Radii.y);
            TestUtilities.AssertThatDoublesAreEqual(Mathd.RMinor, a.Radii.z);
        }

        [Test]
        public void TestNormalisedWgs84Property()
        {
            var a = Ellipsoid.NormalisedWgs84;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Radii.x);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.Radii.y);
            TestUtilities.AssertThatDoublesAreEqual(Mathd.RMinor / Mathd.RMajor, a.Radii.z);
        }

        [Test]
        public void TestUnitSphereProperty()
        {
            var a = Ellipsoid.UnitSphere;

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Radii.x);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.Radii.y);
            TestUtilities.AssertThatDoublesAreEqual(1d, a.Radii.z);
        }

        [Test]
        public void TestRadiiProperty()
        {
            var a = new Ellipsoid(new Vector3d(1d, 2d, 3d));

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Radii.x);
            TestUtilities.AssertThatDoublesAreEqual(2d, a.Radii.y);
            TestUtilities.AssertThatDoublesAreEqual(3d, a.Radii.z);
        }

        [Test]
        public void TestRadiiSquaredProperty()
        {
            var a = new Ellipsoid(new Vector3d(1d, 2d, 3d));

            TestUtilities.AssertThatDoublesAreEqual(Math.Pow(a.Radii.x, 2d), a.RadiiSquared.x);
            TestUtilities.AssertThatDoublesAreEqual(Math.Pow(a.Radii.y, 2d), a.RadiiSquared.y);
            TestUtilities.AssertThatDoublesAreEqual(Math.Pow(a.Radii.z, 2d), a.RadiiSquared.z);
        }

        [Test]
        public void TestRadiiToTheFourthProperty()
        {
            var a = new Ellipsoid(new Vector3d(1d, 2d, 3d));

            TestUtilities.AssertThatDoublesAreEqual(Math.Pow(a.Radii.x, 4d), a.RadiiToTheFourth.x);
            TestUtilities.AssertThatDoublesAreEqual(Math.Pow(a.Radii.y, 4d), a.RadiiToTheFourth.y);
            TestUtilities.AssertThatDoublesAreEqual(Math.Pow(a.Radii.z, 4d), a.RadiiToTheFourth.z);
        }

        [Test]
        public void TestOneOverRadiiSquaredProperty()
        {
            var a = new Ellipsoid(new Vector3d(1d, 2d, 3d));

            TestUtilities.AssertThatDoublesAreEqual(1d / Math.Pow(a.Radii.x, 2d),
                                                    a.OneOverRadiiSquared.x);
            TestUtilities.AssertThatDoublesAreEqual(1d / Math.Pow(a.Radii.y, 2d),
                                                    a.OneOverRadiiSquared.y);
            TestUtilities.AssertThatDoublesAreEqual(1d / Math.Pow(a.Radii.z, 2d),
                                                    a.OneOverRadiiSquared.z);
        }

        [Test]
        public void TestConstructor()
        {
            var a = new Ellipsoid(new Vector3d(1d, 2d, 3d));

            TestUtilities.AssertThatDoublesAreEqual(1d, a.Radii.x);
            TestUtilities.AssertThatDoublesAreEqual(2d, a.Radii.y);
            TestUtilities.AssertThatDoublesAreEqual(3d, a.Radii.z);
        }

        [Test]
        public void TestGeodeticSurfaceNormalMethod()
        {
            // sanity check with an unit sphere and limits first
            var a = Ellipsoid.UnitSphere;
            GeodeticSurfaceNormalTests(a);

            a = Ellipsoid.Wgs84;
            GeodeticSurfaceNormalTests(a);

            a = Ellipsoid.NormalisedWgs84;
            GeodeticSurfaceNormalTests(a);
        }

        private static void GeodeticSurfaceNormalTests(Ellipsoid a)
        {
            var position = new Vector3d(0d, 2d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up,
                a.GeodeticSurfaceNormal(position));

            // keep in mind Z up of WGS84, where Vector3d.Up is Y up - use Vector3d.xzy
            var coordinate = Geodetic2d.NorthPole;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate),
                Mathd.EpsilonE16);

            var coordinate3d = new Geodetic3d(coordinate, 100d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate3d),
                Mathd.EpsilonE16);

            position = new Vector3d(0d, -2d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Down,
                a.GeodeticSurfaceNormal(position));

            coordinate = Geodetic2d.SouthPole;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Down.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate),
                Mathd.EpsilonE16);

            coordinate3d = new Geodetic3d(coordinate, 100d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Down.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate3d),
                Mathd.EpsilonE16);

            position = new Vector3d(0d, 0d, 2d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Forward,
                a.GeodeticSurfaceNormal(position));

            coordinate = Geodetic2d.Meridian90;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Forward.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate),
                Mathd.EpsilonE16);

            coordinate3d = new Geodetic3d(coordinate, 100d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Forward.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate3d),
                Mathd.EpsilonE16);

            position = new Vector3d(0d, 0d, -2d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Back,
                a.GeodeticSurfaceNormal(position));

            coordinate = Geodetic2d.MeridianNegative90;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Back.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate),
                Mathd.EpsilonE16);

            coordinate3d = new Geodetic3d(coordinate, 100d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Back.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate3d),
                Mathd.EpsilonE16);

            position = new Vector3d(2d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right,
                a.GeodeticSurfaceNormal(position));

            coordinate = Geodetic2d.Meridian;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate));

            coordinate3d = new Geodetic3d(coordinate, 100d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate3d));

            position = new Vector3d(-2d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left,
                a.GeodeticSurfaceNormal(position));

            coordinate = Geodetic2d.Meridian180;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate),
                Mathd.EpsilonE15);

            coordinate3d = new Geodetic3d(coordinate, 100d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left.xzy,
                Ellipsoid.GeodeticSurfaceNormal(coordinate3d),
                Mathd.EpsilonE15);

            // non limit cases
            position = Vector3d.One;
            var norm = Vector3d.ComponentMultiply(position, a.OneOverRadiiSquared).Normalised;

            TestUtilities.AssertThatVector3dsAreEqual(norm, a.GeodeticSurfaceNormal(position));

            coordinate = new Geodetic2d(45d, 45d);
            var cosLatitude = Math.Cos(coordinate.Latitude * Mathd.Deg2Rad);
            norm = new Vector3d(cosLatitude * Math.Cos(coordinate.Longitude * Mathd.Deg2Rad),
                cosLatitude * Math.Sin(coordinate.Longitude * Mathd.Deg2Rad),
                Math.Sin(coordinate.Latitude * Mathd.Deg2Rad));

            TestUtilities.AssertThatVector3dsAreEqual(norm, Ellipsoid.GeodeticSurfaceNormal(coordinate));

            coordinate3d = new Geodetic3d(coordinate, 100d);

            TestUtilities.AssertThatVector3dsAreEqual(norm, Ellipsoid.GeodeticSurfaceNormal(coordinate3d));

            position = -Vector3d.One;
            norm = Vector3d.ComponentMultiply(position, a.OneOverRadiiSquared).Normalised;

            TestUtilities.AssertThatVector3dsAreEqual(norm, a.GeodeticSurfaceNormal(position));

            coordinate = new Geodetic2d(-45d, 270d);
            cosLatitude = Math.Cos(coordinate.Latitude * Mathd.Deg2Rad);
            norm = new Vector3d(cosLatitude * Math.Cos(coordinate.Longitude * Mathd.Deg2Rad),
                cosLatitude * Math.Sin(coordinate.Longitude * Mathd.Deg2Rad),
                Math.Sin(coordinate.Latitude * Mathd.Deg2Rad));

            TestUtilities.AssertThatVector3dsAreEqual(norm, Ellipsoid.GeodeticSurfaceNormal(coordinate));

            coordinate3d = new Geodetic3d(coordinate, 100d);

            TestUtilities.AssertThatVector3dsAreEqual(norm, Ellipsoid.GeodeticSurfaceNormal(coordinate3d));
        }

        [Test]
        public void TestScaleToGeocentricSurfaceMethod()
        {
            // sanity check with an unit sphere and limits first
            var a = Ellipsoid.UnitSphere;
            var position = new Vector3d(0d, 0d, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, a.Radii.z),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(0d, 0d, -1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, -a.Radii.z),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(0d, 1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, a.Radii.y, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(0d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, -a.Radii.y, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.Radii.x, 0d, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeocentricSurface(position));

            // non limit cases
            position = Vector3d.One;
            var geocentricSurfacePoint = 1d / Math.Sqrt(Vector3d.Dot(
                Vector3d.Pow(position, 2d), a.OneOverRadiiSquared)) * position;

            TestUtilities.AssertThatVector3dsAreEqual(geocentricSurfacePoint,
                a.ScaleToGeocentricSurface(position));

            position = -Vector3d.One;
            geocentricSurfacePoint = 1d / Math.Sqrt(Vector3d.Dot(
                Vector3d.Pow(position, 2d), a.OneOverRadiiSquared)) * position;

            TestUtilities.AssertThatVector3dsAreEqual(geocentricSurfacePoint,
                a.ScaleToGeocentricSurface(position));

            // at limits, should return radii
            a = Ellipsoid.Wgs84;
            position = new Vector3d(0d, 0d, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, a.Radii.z), 
                              a.ScaleToGeocentricSurface(position), Mathd.EpsilonE9);

            position = new Vector3d(0d, 0d, -1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, -a.Radii.z),
                              a.ScaleToGeocentricSurface(position), Mathd.EpsilonE9);

            position = new Vector3d(0d, 1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, a.Radii.y, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(0d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, -a.Radii.y, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.Radii.x, 0d, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeocentricSurface(position));

            // non limit cases
            position = Vector3d.One;
            geocentricSurfacePoint = 1d / Math.Sqrt(Vector3d.Dot(
                Vector3d.Pow(position, 2d), a.OneOverRadiiSquared)) * position;

            TestUtilities.AssertThatVector3dsAreEqual(geocentricSurfacePoint,
                a.ScaleToGeocentricSurface(position));

            position = -Vector3d.One;
            geocentricSurfacePoint = 1d / Math.Sqrt(Vector3d.Dot(
                Vector3d.Pow(position, 2d), a.OneOverRadiiSquared)) * position;

            TestUtilities.AssertThatVector3dsAreEqual(geocentricSurfacePoint,
                a.ScaleToGeocentricSurface(position));

            a = Ellipsoid.NormalisedWgs84;
            position = new Vector3d(0d, 0d, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, a.Radii.z),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(0d, 0d, -1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, -a.Radii.z),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(0d, 1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, a.Radii.y, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(0d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, -a.Radii.y, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.Radii.x, 0d, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeocentricSurface(position));

            position = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeocentricSurface(position));

            // non limit cases
            position = Vector3d.One;
            geocentricSurfacePoint = 1d / Math.Sqrt(Vector3d.Dot(
                Vector3d.Pow(position, 2d), a.OneOverRadiiSquared)) * position;

            TestUtilities.AssertThatVector3dsAreEqual(geocentricSurfacePoint, 
                a.ScaleToGeocentricSurface(position));

            position = -Vector3d.One;
            geocentricSurfacePoint = 1d / Math.Sqrt(Vector3d.Dot(
                Vector3d.Pow(position, 2d), a.OneOverRadiiSquared)) * position;

            TestUtilities.AssertThatVector3dsAreEqual(geocentricSurfacePoint,
                a.ScaleToGeocentricSurface(position));
        }

        [Test]
        public void TestScaleToGeodeticSurfaceMethod()
        {
            // sanity check with an unit sphere and limits first
            var a = Ellipsoid.UnitSphere;
            var position = new Vector3d(0d, 0d, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, a.Radii.z),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(0d, 0d, -1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, -a.Radii.z),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(0d, 1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, a.Radii.y, 0d),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(0d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, -a.Radii.y, 0d),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.Radii.x, 0d, 0d),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeodeticSurface(position));

            // non limit cases
            position = Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.ComponentMultiply(
                Vector3d.One.Normalised, a.Radii), a.ScaleToGeodeticSurface(position));

            position = -Vector3d.One;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.ComponentMultiply(
                -Vector3d.One.Normalised, a.Radii), a.ScaleToGeodeticSurface(position));

            // at limits, should return radii
            a = Ellipsoid.Wgs84;
            position = new Vector3d(0d, 0d, 100000d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, a.Radii.z),
                              a.ScaleToGeodeticSurface(position), Mathd.EpsilonE7);

            position = new Vector3d(0d, 0d, -100000d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, -a.Radii.z),
                              a.ScaleToGeodeticSurface(position), Mathd.EpsilonE7);

            position = new Vector3d(0d, 100000d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, a.Radii.y, 0d),
                              a.ScaleToGeodeticSurface(position), Mathd.EpsilonE7);

            position = new Vector3d(0d, -100000d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, -a.Radii.y, 0d),
                              a.ScaleToGeodeticSurface(position), Mathd.EpsilonE7);

            position = new Vector3d(100000d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.Radii.x, 0d, 0d),
                              a.ScaleToGeodeticSurface(position), Mathd.EpsilonE7);

            position = new Vector3d(-100000d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeodeticSurface(position), Mathd.EpsilonE7);

            position = new Vector3d(-100000d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeodeticSurface(position), Mathd.EpsilonE7);

            a = Ellipsoid.NormalisedWgs84;
            position = new Vector3d(0d, 0d, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, a.Radii.z),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(0d, 0d, -1d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, 0d, -a.Radii.z),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(0d, 1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, a.Radii.y, 0d),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(0d, -1d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(0d, -a.Radii.y, 0d),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(a.Radii.x, 0d, 0d),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeodeticSurface(position));

            position = new Vector3d(-1d, 0d, 0d);

            TestUtilities.AssertThatVector3dsAreEqual(new Vector3d(-a.Radii.x, 0d, 0d),
                              a.ScaleToGeodeticSurface(position));
        }

        [Test]
        public void TestWgs84Position3dMethod()
        {
            // sanity check with an unit sphere and limits first

            // overload for 2d coordinates
            var a = Ellipsoid.UnitSphere;
            var coordinate = Geodetic2d.Meridian;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right.xzy,
                a.Wgs84Position3d(coordinate));

            coordinate = Geodetic2d.Meridian90;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Forward.xzy,
                a.Wgs84Position3d(coordinate), Mathd.EpsilonE16);

            coordinate = Geodetic2d.Meridian180;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left.xzy,
                a.Wgs84Position3d(coordinate), Mathd.EpsilonE15);

            coordinate = Geodetic2d.MeridianNegative90;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Back.xzy,
                a.Wgs84Position3d(coordinate), Mathd.EpsilonE16);

            coordinate = Geodetic2d.NorthPole;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up.xzy,
                a.Wgs84Position3d(coordinate), Mathd.EpsilonE16);

            coordinate = Geodetic2d.SouthPole;

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Down.xzy,
                a.Wgs84Position3d(coordinate), Mathd.EpsilonE16);

            // overload for 3d coordinates
            a = Ellipsoid.UnitSphere;
            var coordinate3d = new Geodetic3d(Geodetic2d.Meridian, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Right.xzy * 2d,
                a.Wgs84Position3d(coordinate3d));

            coordinate3d = new Geodetic3d(Geodetic2d.Meridian90, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Forward.xzy * 2d,
                a.Wgs84Position3d(coordinate3d), Mathd.EpsilonE15);

            coordinate3d = new Geodetic3d(Geodetic2d.Meridian180, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Left.xzy * 2d,
                a.Wgs84Position3d(coordinate3d), Mathd.EpsilonE15);

            coordinate3d = new Geodetic3d(Geodetic2d.MeridianNegative90, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Back.xzy * 2d,
                a.Wgs84Position3d(coordinate3d), Mathd.EpsilonE15);

            coordinate3d = new Geodetic3d(Geodetic2d.NorthPole, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Up.xzy * 2d,
                a.Wgs84Position3d(coordinate3d), Mathd.EpsilonE15);

            coordinate3d = new Geodetic3d(Geodetic2d.SouthPole, 1d);

            TestUtilities.AssertThatVector3dsAreEqual(Vector3d.Down.xzy * 2d,
                a.Wgs84Position3d(coordinate3d), Mathd.EpsilonE15);
        }

        [Test]
        public void TestWgs84Coordinate2dMethod()
        {
            // sanity check with an unit sphere and limits first
            var a = Ellipsoid.UnitSphere;
            var pos = Vector3d.Right.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.Meridian, 
                                                        a.Wgs84Coordinate2d(pos));

            pos = Vector3d.Forward.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.Meridian90,
                                                        a.Wgs84Coordinate2d(pos));

            pos = Vector3d.Left.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.Meridian180,
                                                        a.Wgs84Coordinate2d(pos));

            pos = Vector3d.Back.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.MeridianNegative90,
                                                        a.Wgs84Coordinate2d(pos));

            pos = Vector3d.Up.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.NorthPole,
                                                        a.Wgs84Coordinate2d(pos));

            pos = Vector3d.Down.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.SouthPole,
                                                        a.Wgs84Coordinate2d(pos));

            // at limits, should behave same as unit sphere
            a = Ellipsoid.Wgs84;
            pos = Vector3d.Right.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.Meridian,
                                                        a.Wgs84Coordinate2d(pos));

            pos = Vector3d.Forward.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.Meridian90,
                                                        a.Wgs84Coordinate2d(pos));

            pos = Vector3d.Left.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.Meridian180,
                                                        a.Wgs84Coordinate2d(pos));

            pos = Vector3d.Back.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.MeridianNegative90,
                                                        a.Wgs84Coordinate2d(pos));

            pos = Vector3d.Up.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.NorthPole,
                                                        a.Wgs84Coordinate2d(pos));

            pos = Vector3d.Down.xzy;

            TestUtilities.AssertThatGeodetic2dsAreEqual(Geodetic2d.SouthPole,
                                                        a.Wgs84Coordinate2d(pos));
        }

        [Test]
        public void TestWgs84Coordinate3dMethod()
        {
            // sanity check with an unit sphere and limits first
            var a = Ellipsoid.UnitSphere;
            var pos = Vector3d.Right.xzy;
            var expectedGeodetic3d = new Geodetic3d(Geodetic2d.Meridian, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                1d);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = Vector3d.Forward.xzy;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.Meridian90, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                1d);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d, 
                a.Wgs84Coordinate3d(pos));

            pos = Vector3d.Left.xzy;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.Meridian180, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                1d);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = Vector3d.Back.xzy;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.MeridianNegative90, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                1d);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = Vector3d.Up.xzy;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.NorthPole, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                1d);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = Vector3d.Down.xzy;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.SouthPole, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                1d);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            // wgs84 ellipsoid

            a = Ellipsoid.Wgs84;
            pos = Vector3d.Right.xzy * Mathd.RMajor;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.Meridian, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                Mathd.RMajor);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos), Mathd.EpsilonE9);

            pos = Vector3d.Forward.xzy * Mathd.RMajor;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.Meridian90, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                Mathd.RMajor);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos), Mathd.EpsilonE9);

            pos = Vector3d.Left.xzy * Mathd.RMajor;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.Meridian180, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                Mathd.RMajor);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos), Mathd.EpsilonE9);

            pos = Vector3d.Back.xzy * Mathd.RMajor;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.MeridianNegative90, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                Mathd.RMajor);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos), Mathd.EpsilonE9);

            pos = Vector3d.Up.xzy * Mathd.RMinor;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.NorthPole, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                Mathd.RMinor);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = Vector3d.Down.xzy * Mathd.RMinor;
            expectedGeodetic3d = new Geodetic3d(Geodetic2d.SouthPole, 0d);

            // should lie exactly on surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));

            pos = pos * 2d;
            expectedGeodetic3d = new Geodetic3d(expectedGeodetic3d.Latitude,
                                                expectedGeodetic3d.Longitude,
                                                Mathd.RMinor);

            // should be 1 unit above surface
            TestUtilities.AssertThatGeodetic3dsAreEqual(expectedGeodetic3d,
                a.Wgs84Coordinate3d(pos));
        }

        [Test]
        public void TestCurveBetweenMethod()
        {
            // sanity check with an unit sphere and limits first
            var ellipsoid = Ellipsoid.UnitSphere;

            // top to bottom, 180 degrees
            var positionA = Vector3d.Up.xzy;
            var positionB = Vector3d.Right.xzy;

            // granularity of 10, 9 divisions, 10 output points
            var curve = ellipsoid.CurveBetween(positionA, positionB, 10);

            Assert.AreEqual(10, curve.Length);

            // no position should be below xz plane
            // no position should be behind xy plane
            // all positions should be on yz plane
            foreach (var position in curve)
            {
                Assert.IsTrue(position.xzy.y >= 0d);
                Assert.IsTrue(position.xzy.z >= 0d);
                Assert.IsTrue(position.xzy.x <= 1d);
            }

            // early exit checks

            // tiny or no angle between a and b
            positionA = Vector3d.Up.xzy;
            positionB = Vector3d.Up.xzy;

            curve = ellipsoid.CurveBetween(positionA, positionB, 10);

            Assert.AreEqual(1, curve.Length);

            // angle between is equal or smaller than granularity
            positionA = Vector3d.Up.xzy;
            positionB = Vector3d.Up.Rotate(Vector3d.Forward, 10d).xzy;

            curve = ellipsoid.CurveBetween(positionA, positionB, 10);

            Assert.AreEqual(2, curve.Length);

            // tiny granularity check
            positionA = Vector3d.Up.xzy;
            positionB = Vector3d.Right.xzy;

            curve = ellipsoid.CurveBetween(positionA, positionB, Mathd.EpsilonE5);

            // anti-parallel fail check

            // top to bottom, 180 degreess
            positionA = Vector3d.Up.xzy;
            positionB = Vector3d.Down.xzy;

            // granularity of 10, 9 divisions, 10 output points
            Assert.Throws<NotSupportedException>(() => curve = ellipsoid.CurveBetween(positionA, positionB, 10));

            var coordinateA = Geodetic2d.NorthPole;
            var coordinateB = Geodetic2d.Meridian;

            // granularity of 10, 9 divisions, 10 output points
            curve = ellipsoid.CurveBetween(coordinateA, coordinateB, 10);

            Assert.AreEqual(10, curve.Length);

            // no position should be below xz plane
            // no position should be behind xy plane
            // all positions should be on yz plane
            foreach (var position in curve)
            {
                Assert.IsTrue(position.xzy.y >= 0d);
                Assert.IsTrue(position.xzy.z >= 0d);
                Assert.IsTrue(position.xzy.x <= 1d);
            }

            // todo: check why geodetic3d coordinates with height > 0 are failing
            var coordinate3dA = new Geodetic3d(Geodetic2d.NorthPole, 0d);
            var coordinate3dB = new Geodetic3d(Geodetic2d.Meridian, 0d);

            // granularity of 10, 9 divisions, 10 output points
            curve = ellipsoid.CurveBetween(coordinate3dA, coordinate3dB, 10);

            Assert.AreEqual(10, curve.Length);

            // no position should be below xz plane
            // no position should be behind xy plane
            // all positions should be on yz plane
            foreach (var position in curve)
            {
                Assert.IsTrue(position.xzy.y >= 0d);
                Assert.IsTrue(position.xzy.z >= 0d);
                Assert.IsTrue(position.xzy.x <= 1d);
            }
        }
    }
}