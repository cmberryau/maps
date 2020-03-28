using System;
using Maps.Geographical;
using Maps.Geographical.Features;
using NUnit.Framework;

namespace Maps.Tests.Geographical.Features
{
    /// <summary>
    /// An series of minimum tests for IFeatureSource implementers
    /// </summary>
    [TestFixture]
    public abstract class FeatureSourceTests
    {
        /// <summary>
        /// Creates a IFeatureSource with known reference data
        /// </summary>
        protected abstract IFeatureSource CreateReferenceSource();

        /// <summary>
        /// Creates an empty IFeatureSource
        /// </summary>
        protected abstract IFeatureSource CreateEmptySource();

        /// <summary>
        /// Tests the constructor for the IFeatureSource implementer
        /// </summary>
        [Test]
        public abstract void TestConstructor();

        /// <summary>
        /// Tests the constructor for the IFeatureSource implementer
        /// </summary>
        [Test]
        public abstract void TestConstructorInvalidParameters();

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features given
        /// a coordinate box
        /// </summary>
        [Test]
        public void TestBoxGetMethod()
        {
            using (var source = CreateReferenceSource())
            {
                var features = source.Get(TestUtilities.BigIngolstadtBox);

                Assert.IsNotNull(features);
                Assert.IsNotEmpty(features);

                for (var i = 0; i < features.Count; i++)
                {
                    Assert.IsNotNull(features[i]);
                }
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features given
        /// a coordinate box when given invalid parameters
        /// </summary>
        [Test]
        public void TestBoxGetMethodInvalidParameters()
        {
            using (var source = CreateReferenceSource())
            {
                GeodeticBox2d box = null;
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var features = source.Get(box);
                });
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features 
        /// asyncronously given a coordinate box
        /// </summary>
        [Test]
        public void TestBoxGetAsyncMethod()
        {
            using (var source = CreateReferenceSource())
            {
                var featuresAsync = source.GetAsync(TestUtilities.BigIngolstadtBox);

                featuresAsync.Wait();

                var features = featuresAsync.Result;

                Assert.IsNotNull(features);
                Assert.IsNotEmpty(features);

                for (var i = 0; i < features.Count; i++)
                {
                    Assert.IsNotNull(features[i]);
                }
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features 
        /// asyncronously given a coordinate box when given invalid params
        /// </summary>
        [Test]
        public void TestBoxGetAsyncMethodInvalidParameters()
        {
            using (var source = CreateReferenceSource())
            {
                GeodeticBox2d box = null;
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var features = source.GetAsync(box);
                });
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features given
        /// a coordinate box when empty
        /// </summary>
        [Test]
        public void TestBoxGetMethodEmptySource()
        {
            using (var source = CreateEmptySource())
            {
                var features = source.Get(TestUtilities.BigIngolstadtBox);

                Assert.IsNotNull(features);
                Assert.IsEmpty(features);
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features 
        /// asyncronously given a coordinate box when empty
        /// </summary>
        [Test]
        public void TestBoxGetAsyncMethodEmptySource()
        {
            using (var source = CreateEmptySource())
            {
                var features = source.GetAsync(TestUtilities.BigIngolstadtBox);

                features.Wait();

                Assert.IsNotNull(features.Result);
                Assert.IsEmpty(features.Result);
            }
        }
    }
}