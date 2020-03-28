using System;

namespace Maps.Appearance.Properties
{
    /// <summary>
    /// Represents a color property
    /// </summary>
    public class ColorProperty : Property
    {
        /// <summary>
        /// The color of the color property
        /// </summary>
        public readonly Colorf Value;

        /// <summary>
        /// Initializes a new instance of ColorProperty
        /// </summary>
        /// <param name="key">The target of the property</param>
        /// <param name="value">The color of the property</param>
        public ColorProperty(string key, Colorf value) : base(key)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override void Accept(IPropertyVisitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            visitor.Visit(this);
        }

        /// <inheritdoc />
        public override TResult Accept<TResult, T0>(IPropertyVisitor<TResult, 
            T0> visitor, T0 param)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            return visitor.Visit(this, param);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{base.ToString()}, Color: {Value}";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is ColorProperty))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (ColorProperty) obj;
            return other.Value.Equals(Value) && other.Key.Equals(Key);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Value.GetHashCode();
                hash = (hash * 397) ^ Key.GetHashCode();
                return hash;
            }
        }
    }
}