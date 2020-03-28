using System;
using System.Collections.Generic;
using Maps.Appearance.Properties;
using Maps.Extensions;
using Maps.Geographical.Projection;
using Maps.Geometry.Tessellation;
using Maps.Rendering;
using Maps.Appearance;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Responsible for holding area appearance information
    /// </summary>
    public class AreaAppearance : FeatureAppearance
    {
        private readonly MeshAppearance _mainAppearance;
        private readonly IPolygonTessellator _mainTessellator;
        private readonly bool _label;
        private readonly LabelAppearance _labelAppearance;

        /// <summary>
        /// Initializes a new instance of AreaAppearance
        /// </summary>
        /// <param name="properties">The properties of the segment appearance</param>
        /// <param name="projection">The projection of the segment appearance</param>
        public AreaAppearance(IList<Property> properties, IProjection projection)
            : base(properties, projection)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var parser = new AreaPropertyParser(properties);
            parser.Parse();

            _mainAppearance = parser.MainAppearance;
            _mainTessellator = parser.MainTessellator;
            RenderableAppearances = new List<MeshAppearance>
            {
                _mainAppearance
            };

            UIElementAppearances = new List<UIRenderableAppearance>();
            _label = parser.Label;
            if (_label)
            {
                _labelAppearance = parser.LabelAppearance;
                UIElementAppearances.Add(_labelAppearance);
            }
        }

        /// <summary>
        /// Evaluates the renderables for the given area
        /// </summary>
        /// <param name="area">The area to evaluate renderables for</param>
        /// <returns>The renderables for the given area</returns>
        public IList<Renderable> RenderablesFor(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            return RenderablesFor(area, Vector3d.Zero, 1d);
        }

        /// <summary>
        /// Evaluates the renderables for the given area
        /// </summary>
        /// <param name="area">The area to evaluate renderables for</param>
        /// <param name="anchor">The relative anchor point</param>
        /// <param name="scale">The scale</param>
        /// <returns>The renderables for the given area</returns>
        public IList<Renderable> RenderablesFor(Area area, Vector3d anchor,
            double scale)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            var renderables = new List<Renderable>();
            var projected = Projection.Forward(area.Polygon);

            var mesh = _mainTessellator.Tessellate(projected);
            renderables.Add(new MeshRenderable(mesh.Bounds, mesh,
                _mainAppearance).Relative(anchor, scale));

            if (_label && !area.Name.IsNullOrWhiteSpace())
            {
                renderables.Add(new UIRenderable(mesh.Bounds, mesh.Bounds.Centre,
                    _labelAppearance, area.Name).Relative(anchor, scale));
            }

            return renderables;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is AreaAppearance))
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