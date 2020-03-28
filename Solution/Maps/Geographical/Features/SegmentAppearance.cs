using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Appearance.Properties;
using Maps.Extensions;
using Maps.Geographical.Projection;
using Maps.Geometry;
using Maps.Geometry.Tessellation;
using Maps.Rendering;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Responsible for holding segment appearance information
    /// </summary>
    public class SegmentAppearance : FeatureAppearance
    {
        private readonly MeshAppearance _mainAppearance;
        private readonly ILineTessellator _mainTessellator;
        private readonly bool _outline;
        private readonly MeshAppearance _outlineAppearance;
        private readonly ILineTessellator _outlineTessellator;
        private readonly bool _label;
        private readonly LabelAppearance _labelAppearance;

        /// <summary>
        /// Initializes a new instance of SegmentAppearance
        /// </summary>
        /// <param name="properties">The properties of the segment appearance</param>
        /// <param name="projection">The projection of the segment appearance</param>
        public SegmentAppearance(IList<Property> properties, IProjection projection)
            : base(properties, projection)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var parser = new SegmentPropertyParser(properties);
            parser.Parse();

            _mainAppearance = parser.MainAppearance;
            _mainTessellator = parser.MainTessellator;
            RenderableAppearances = new List<MeshAppearance>
            {
                _mainAppearance
            };

            _outline = parser.Outline;
            if (_outline)
            {
                _outlineAppearance = parser.OutlineAppearance;
                _outlineTessellator = parser.OutlineTessellator;
                RenderableAppearances.Add(_outlineAppearance);
            }

            UIElementAppearances = new List<UIRenderableAppearance>();
            _label = parser.Label;
            if (_label)
            {
                _labelAppearance = parser.LabelAppearance;
                UIElementAppearances.Add(_labelAppearance);
            }
        }

        /// <summary>
        /// Evaluates the renderables for the given segment
        /// </summary>
        /// <param name="segment">The segment to evaluate renderables for</param>
        /// <returns>The renderables for the given segment</returns>
        public IList<Renderable> RenderablesFor(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            return RenderablesFor(segment, Vector3d.Zero, 1d);
        }

        /// <summary>
        /// Evaluates the renderables for the given segment
        /// </summary>
        /// <param name="segment">The segment to evaluate renderables for</param>
        /// <param name="anchor">The relative anchor point</param>
        /// <param name="scale">The scale</param>
        /// <returns>The renderables for the given segment</returns>
        public IList<Renderable> RenderablesFor(Segment segment, Vector3d anchor,
            double scale)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            var renderables = new List<Renderable>();
            var projected = Projection.Forward(segment.LineStrip);
            var projectedLineStrip = new LineStrip2d(projected.xy());

            var mesh = _mainTessellator.Tessellate(projected);
            renderables.Add(new MeshRenderable(mesh.Bounds, mesh, 
                _mainAppearance).Relative(anchor, scale));

            if (_label && !segment.Name.IsNullOrWhiteSpace())
            {
                if (_labelAppearance.MinimumSegmentLength)
                {
                    if (segment.LineStrip.Length >= 
                        _labelAppearance.MinimumSegmentLengthValue)
                    {
                        renderables.Add(new UIRenderable(mesh.Bounds, new Vector3d(
                                projectedLineStrip.PointAlongAt(0.5d)), _labelAppearance,
                            segment.Name).Relative(anchor, scale));
                    }
                }
                else
                {
                    renderables.Add(new UIRenderable(mesh.Bounds, new Vector3d(
                        projectedLineStrip.PointAlongAt(0.5d)), _labelAppearance,
                        segment.Name).Relative(anchor, scale));
                }
            }

            if (_outline)
            {
                mesh = _outlineTessellator.Tessellate(projected);
                renderables.Add(new MeshRenderable(mesh.Bounds, mesh, 
                    _outlineAppearance).Relative(anchor, scale));
            }

            return renderables;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is SegmentAppearance))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return GetHashCode().Equals(obj.GetHashCode());
        }
    }
}