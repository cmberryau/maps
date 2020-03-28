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
    /// Series of tests for the MapFeatureTarget class
    /// </summary>
    internal abstract class MapsFeatureTargetTests : TiledFeatureTargetTests
    {
        /// <summary>
        /// Creates a temporary Feature data connection
        /// </summary>
        protected abstract IDbConnection<long, byte[]> FeatureConnection();

        /// <summary>
        /// Creates a temporary Feature data connection
        /// </summary>
        protected abstract IDbConnection<long, byte[]> ReferenceFeatureConnection();

        /// <summary>
        /// Creates a temporary string connection
        /// </summary>
        protected abstract IDbConnection<long, string> StringConnection();

        /// <summary>
        /// Creates a temporary string connection
        /// </summary>
        protected abstract IDbConnection<long, string> ReferenceStringConnection();

        /// <summary>
        /// Creates a temporary image connection
        /// </summary>
        protected abstract IDbConnection<long, Bitmap> ImageConnection();

        /// <summary>
        /// Creates a temporary image connection
        /// </summary>
        protected abstract IDbConnection<long, Bitmap> ReferenceImageConnection();

        /// <inheritdoc />
        protected override ITiledFeatureTarget CreateTarget()
        {
            var featuresConnection = FeatureConnection();
            featuresConnection.Clear();

            var stringConnection = StringConnection();
            stringConnection.Clear();

            var imageConnection = ImageConnection();
            imageConnection.Clear();

            var stringTable = new DbTable<string>(stringConnection);
            var imageTable = new DbTable<Bitmap>(imageConnection);
            var sideData = new SideData(new List<ITable>
            {
                stringTable, imageTable
            });

            return new MapsFeatureTarget(featuresConnection.Writer(), 
                featuresConnection.MetaWriter(), sideData);
        }

        /// <inheritdoc />
        protected override ITiledFeatureSource CreateSource()
        {
            var featuresConnection = FeatureConnection();
            var stringConnection = StringConnection();
            var imageConnection = ImageConnection();

            var stringTable = new DbTable<string>(stringConnection);
            var imageTable = new DbTable<Bitmap>(imageConnection);
            var sideData = new SideData(new List<ITable>
            {
                stringTable, imageTable
            });

            return new MapsFeatureSource(featuresConnection.Reader(),
                featuresConnection.MetaReader(), sideData);
        }

        /// <inheritdoc />
        protected override IFeatureProvider ReferenceFeatureProvider()
        {
            var featuresConnection = ReferenceFeatureConnection();
            var stringConnection = ReferenceStringConnection();
            var imageConnection = ReferenceImageConnection();

            var sideData = new SideData(new List<ITable>
            {
                new DbTable<string>(stringConnection),
                new DbTable<Bitmap>(imageConnection),
            });

            return new MapsFeatureProvider(featuresConnection, sideData);
        }

        /// <inheritdoc />
        [Test]
        public override void TestConstructor()
        {
            var featuresConnection = FeatureConnection();
            featuresConnection.Clear();

            var stringConnection = StringConnection();
            stringConnection.Clear();

            var imageConnection = ImageConnection();
            imageConnection.Clear();

            var stringTable = new DbTable<string>(stringConnection);
            var imageTable = new DbTable<Bitmap>(imageConnection);
            var sideData = new SideData(new List<ITable>
            {
                stringTable, imageTable
            });

            Assert.DoesNotThrow(() =>
            {
                new MapsFeatureTarget(featuresConnection.Writer(), 
                    featuresConnection.MetaWriter(), sideData);
            });
        }

        /// <inheritdoc />
        [Test]
        public override void TestConstructorInvalidParameters()
        {
            var featuresConnection = FeatureConnection();
            featuresConnection.Clear();

            var stringConnection = StringConnection();
            stringConnection.Clear();

            var imageConnection = ImageConnection();
            imageConnection.Clear();

            var stringTable = new DbTable<string>(stringConnection);
            var imageTable = new DbTable<Bitmap>(imageConnection);
            var sideData = new SideData(new List<ITable>
            {
                stringTable, imageTable
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new MapsFeatureTarget(featuresConnection.Writer(), 
                    featuresConnection.MetaWriter(), null);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new MapsFeatureTarget(featuresConnection.Writer(), null, sideData);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new MapsFeatureTarget(null, featuresConnection.MetaWriter(), sideData);
            });
        }
    }
}