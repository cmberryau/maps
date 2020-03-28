using System;
using System.Collections.Generic;
using Maps.Appearance.Properties;
using Maps.Extensions;
using Maps.Geographical.Places;
using Maps.Geographical.Projection;
using Maps.Geometry;
using Maps.Rendering;
using Maps.Appearance;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Responsible for holding place appearance information
    /// </summary>
    public class PlaceAppearance : FeatureAppearance
    {
        private readonly bool _label;
        private readonly LabelAppearance _labelAppearance;

        private readonly bool _icon;
        private readonly IconAppearance _iconAppearance;

        /// <summary>
        /// Initializes a new instance of PlaceAppearance
        /// </summary>
        /// <param name="properties">The properties of the appearance</param>
        /// <param name="projection">The projection for the place</param>
        public PlaceAppearance(IList<Property> properties, IProjection projection)
            : base(properties, projection)
        {
            var parser = new PlacePropertyParser(properties);
            parser.Parse();

            RenderableAppearances = new List<MeshAppearance>();
            UIElementAppearances = new List<UIRenderableAppearance>();

            _label = parser.Label;
            if (_label)
            {
                _labelAppearance = parser.LabelAppearance;
                UIElementAppearances.Add(_labelAppearance);
            }

            _icon = parser.Icon;
            if (_icon)
            {
                _iconAppearance = parser.IconApppearance;
                UIElementAppearances.Add(_iconAppearance);
            }
        }

        /// <summary>
        /// Evaluates the renderables for the given place
        /// </summary>
        /// <param name="place">The place to evaluate renderables for</param>
        /// <returns>The renderables for the given place</returns>
        public IList<Renderable> RenderablesFor(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            return RenderablesFor(place, Vector3d.Zero, 1d);
        }

        /// <summary>
        /// Evaluates the renderables for the given place
        /// </summary>
        /// <param name="place">The place to evaluate renderables for</param>
        /// <param name="anchor">The relative anchor point</param>
        /// <param name="scale">The scale</param>
        /// <returns>The renderables for the given place</returns>
        public IList<Renderable> RenderablesFor(Place place, Vector3d anchor,
            double scale)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            var renderables = new List<Renderable>();
            var projected = Projection.Forward(place.Coordinate);

            if (_label)
            {
                if (!place.Name.IsNullOrWhiteSpace())
                {
                    renderables.Add(new UIRenderable(Bounds3d.One, projected,
                        _labelAppearance, place.Name).Relative(anchor, scale));
                }
            }

            if (_icon)
            {
                renderables.Add(new UIRenderable(Bounds3d.One, projected, _iconAppearance,
                    place.Name, place.Icon).Relative(anchor, scale));
            }

            return renderables;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is PlaceAppearance))
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