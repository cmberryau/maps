using System;
using System.Collections.Generic;
using System.Drawing;
using Maps.Data.Geographical.Features;
using Maps.Geographical.Features;
using Maps.IO;
using Maps.Tests.Geographical.Features;
using NUnit.Framework;

namespace Maps.Data.Tests.Geographical.Features
{
    /// <summary>
    /// Series of tests for the MapsFeatureSource class
    /// </summary>
    [TestFixture]
    internal abstract class MapsFeatureSourceTests : TiledFeatureSourceTests
    {
        /// <summary>
        /// Creates a Feature data connection to data in an unknown state
        /// </summary>
        protected abstract IDbConnection<long, byte[]> FeatureConnection();

        /// <summary>
        /// Creates a Feature data connection to reference data
        /// </summary>
        protected abstract IDbConnection<long, byte[]> ReferenceFeatureConnection();

        /// <summary>
        /// Creates a string data connection to reference data
        /// </summary>
        protected abstract IDbConnection<long, string> StringConnection();

        /// <summary>
        /// Creates a string data connection to reference data
        /// </summary>
        protected abstract IDbConnection<long, Bitmap> ImageConnection();

        /// <summary>
        /// Creates a string data connection to reference data
        /// </summary>
        protected abstract IDbConnection<long, string> ReferenceStringConnection();

        /// <summary>
        /// Creates a image data connection to reference data
        /// </summary>
        protected abstract IDbConnection<long, Bitmap> ReferenceImageConnection();

        /// <inheritdoc/>
        protected override ITiledFeatureSource CreateReferenceTiledSource()
        {
            var featuresConnection = ReferenceFeatureConnection();

            var sideData = new SideData(new List<ITable>
            {
                new DbTable<string>(ReferenceStringConnection()),
                new DbTable<Bitmap>(ReferenceImageConnection())
            });

            return new MapsFeatureSource(featuresConnection.Reader(),
                featuresConnection.MetaReader(), sideData);
        }

        /// <inheritdoc/>
        protected override ITiledFeatureSource CreateEmptyTiledSource()
        {
            var featuresConnection = FeatureConnection();
            featuresConnection.Clear();

            var stringConnection = StringConnection();
            stringConnection.Clear();

            var imageConnection = ImageConnection();
            imageConnection.Clear();

            var sideData = new SideData(new List<ITable>
            {
                new DbTable<string>(stringConnection),
                new DbTable<Bitmap>(imageConnection)
            });

            return new MapsFeatureSource(featuresConnection.Reader(),
                featuresConnection.MetaReader(), sideData);
        }

        [Test]
        public override void TestConstructor()
        {
            var featuresConnection = FeatureConnection();

            var sideData = new SideData(new List<ITable>
            {
                new DbTable<string>(StringConnection()),
                new DbTable<Bitmap>(ImageConnection())
            });

            Assert.DoesNotThrow(() =>
            {
                new MapsFeatureSource(featuresConnection.Reader(), 
                    featuresConnection.MetaReader(), sideData);
            });
        }

        [Test]
        public override void TestConstructorInvalidParameters()
        {
            var featuresConnection = FeatureConnection();

            var sideData = new SideData(new List<ITable>
            {
                new DbTable<string>(StringConnection()),
                new DbTable<Bitmap>(ImageConnection())
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new MapsFeatureSource(featuresConnection.Reader(), featuresConnection.MetaReader(), null);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new MapsFeatureSource(featuresConnection.Reader(), null, sideData);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new MapsFeatureSource(null, featuresConnection.MetaReader(), sideData);
            });
        }
    }
}