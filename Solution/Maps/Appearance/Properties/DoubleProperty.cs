using System;

namespace Maps.Appearance.Properties
{
    /// <summary>
    /// Represents a double floating point property
    /// </summary>
    public class DoubleProperty : Property
    {
        /// <summary>
        /// The value of the double property
        /// </summary>
        public readonly double Value;

        /// <summary>
        /// Initializes a new instance of DoubleProperty
        /// </summary>
        /// <param name="key">The target of the property</param>
        /// <param name="value">The value of the property</param>
        public DoubleProperty(string key, double value) : base(key)
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
            return $"{base.ToString()}, Value: {Value}";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is DoubleProperty))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (DoubleProperty)obj;
            return Math.Abs(other.Value - Value) < Mathd.Epsilon && 
                other.Key.Equals(Key);
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