using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Extensions;
using Maps.Geographical.Places;
using Maps.Geometry.Simplification;
using Maps.IO;
using Maps.IO.Features;
using Maps.Rendering;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Represents a static geographical feature
    /// </summary>
    public abstract class Feature
    {
        /// <summary>
        /// The unique guid of the feature
        /// </summary>
        /// <remarks>
        /// If multiple Feature instances have the same guid, they represent the same 
        /// real-world feature but may represent different parts
        /// </remarks>
        public readonly Guid Guid;

        /// <summary>
        /// The name of the feature
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Empty feature list, to prevent creating empty lists constantly
        /// </summary>
        protected static readonly IList<Feature> EmptyFeatures = new Feature[0];

        private const string EmptyName = "";

        /// <summary>
        /// Initializes a new instance of Feature
        /// </summary>
        /// <param name="guid">The guid of the feature</param>
        /// <param name="name">The name of the feature</param>
        protected Feature(Guid guid, string name)
        {
            if (name.IsNullOrWhiteSpace())
            {
                Name = EmptyName;
            }
            else
            {
                Name = name;
            }

            Guid = guid;
        }

        /// <summary>
        /// Returns the renderable feature for the feature
        /// </summary>
        /// <param name="appearance">The map appearance to use</param>
        public abstract IList<Renderable> RenderablesFor(IMapAppearance appearance);

        /// <summary>
        /// Returns the renderable feature for the feature
        /// </summary>
        /// <param name="appearance">The map appearance to use</param>
        /// <param name="anchor">A relative anchor point</param>
        /// <param name="scale">The scale</param>
        public abstract IList<Renderable> RenderablesFor(IMapAppearance appearance, Vector3d anchor, double scale);

        /// <summary>
        /// Evaluates if the feature is connected to the place
        /// </summary>
        /// <param name="place">The place to evaluate against</param>
        /// <returns>True if connected, false otherwise</returns>
        public abstract bool ConnectedTo(Place place);

        /// <summary>
        /// Evaluates the connections the given segment has with this feature
        /// </summary>
        /// <param name="segment">The segment to evaluate</param>
        /// <returns>Boolean at each of the segments's coordinate indices indicating 
        /// a connection on true, false otherwise</returns>
        public abstract IList<bool> ConnectionsFrom(Segment segment);

        /// <summary>
        /// Evaluates the connections the given area has with this feature
        /// </summary>
        /// <param name="area">The area to evaluate against</param>
        /// <returns>Boolean at each of the area's coordinate indices indicating 
        /// a connection on true, false otherwise</returns>
        public abstract IList<IList<bool>> ConnectionsFrom(Area area);

        /// <summary>
        /// Accepts a visitor
        /// </summary>
        /// <param name="visitor">The visitor to accept</param>
        public abstract void Accept(IFeatureVisitor visitor);

        /// <summary>
        /// Accepts a visitor
        /// </summary>
        /// <param name="visitor">The visitor to accept</param>
        /// <typeparam name="TResult">The return type</typeparam>
        public abstract TResult Accept<TResult>(IFeatureVisitor<TResult> visitor);

        /// <summary>
        /// Accepts a visitor
        /// </summary>
        /// <param name="visitor">The visitor to accept</param>
        /// <param name="param">The first parameter</param>
        /// <typeparam name="TResult">The return type</typeparam>
        /// <typeparam name="T0">The first parameter type</typeparam>
        public abstract TResult Accept<TResult, T0>(IFeatureVisitor<TResult, T0> visitor, T0 param);

        /// <summary>
        /// Accepts a visitor
        /// </summary>
        /// <param name="visitor">The visitor to accept</param>
        /// <param name="param">The first parameter</param>
        /// <param name="param2">The second parameter</param>
        /// <typeparam name="TResult">The return type</typeparam>
        /// <typeparam name="T0">The first parameter type</typeparam>
        /// <typeparam name="T1">The second parameter type</typeparam>
        public abstract TResult Accept<TResult, T0, T1>(IFeatureVisitor<TResult, T0, T1> visitor, T0 param, T1 param2);

        /// <summary>
        /// Returns the feature, relative to the given coordinate
        /// </summary>
        /// <param name="coordinate">The coordinate to become relative to</param>
        public abstract Feature Relative(Geodetic2d coordinate);

        /// <summary>
        /// Returns the feature, reversing relativity to a given coordinate
        /// </summary>
        /// <param name="coordinate">The coordinate to reverse relativity to</param>
        public abstract Feature Absolute(Geodetic2d coordinate);

        /// <summary>
        /// Converts to the Binary representation of the Feature
        /// </summary>
        /// <param name="sideData">The side data target</param>
        public abstract BinaryFeature ToBinary(ISideData sideData = null);

        /// <inheritdoc />
        public abstract override bool Equals(object obj);

        /// <inheritdoc />
        public abstract override int GetHashCode();

        /// <summary>
        /// Returns the clipped Feature to the given box
        /// </summary>
        /// <param name="box">The box to clip to</param>
        public abstract IList<Feature> ClipTo(GeodeticBox2d box);

        /// <summary>
        /// Simplifies the feature
        /// </summary>
        /// <param name="simplifier">The simplifier to use</param>
        /// <param name="neighbours">The neighbours to consider in simplification</param>
        /// <param name="preservedEdges">The edges to preserve in simplification</param>
        /// <returns>The simplified feature</returns>
        public abstract Feature Simplify(IGeodeticSimplifier2d simplifier, 
            IList<Feature> neighbours, GeodeticPolygon2d preservedEdges);

        /// <summary>
        /// Evaluates if the host coordinate connects to the subject coordinates
        /// </summary>
        /// <param name="host">The host coordinate</param>
        /// <param name="subject">The subject coordinates</param>
        /// <returns>True if connected</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="subject"/> 
        /// is null</exception>
        protected static bool ResolveConnection(Geodetic2d host, 
            IReadOnlyList<Geodetic2d> subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            var subjectCount = subject.Count;

            for (var i = 0; i < subjectCount; ++i)
            {
                if (host == subject[i])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Evaluates the indices at which the host coordinate list connects to the 
        /// subject coordinate
        /// </summary>
        /// <param name="host">The host coordinate list</param>
        /// <param name="subject">The subject coordinate</param>
        /// <returns>A list of booleans, marked true at a connection</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> 
        /// is null</exception>
        protected static IList<bool> ResolveConnections(IReadOnlyList<Geodetic2d> host, 
            Geodetic2d subject)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            var hostCount = host.Count;
            var result = new bool[hostCount];

            for (var i = 0; i < hostCount; ++i)
            {
                if (host[i] == subject)
                {
                    result[i] = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Evaluates the indices at which the host coordinate list connects to the 
        /// subject coordinate list
        /// </summary>
        /// <param name="host">The host coordinate list</param>
        /// <param name="subject">The subject coordinate list</param>
        /// <returns>A list of booleans, marked true at a connection</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or 
        /// <paramref name="subject"/> is null
        /// null</exception>
        protected static IList<bool> ResolveConnections(IReadOnlyList<Geodetic2d> host, 
            IReadOnlyList<Geodetic2d> subject)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            var hostCount = host.Count;
            var result = new bool[hostCount];
            var subjectCount = subject.Count;
            var self = host.Equals(subject);

            for (var i = 0; i < hostCount; ++i)
            {
                var a = host[i];

                for (var j = 0; j < subjectCount; ++j)
                {
                    if (!(self && i == j) && a == subject[j])
                    {
                        result[i] = true;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
