using System;

namespace Maps.Appearance.Properties
{
    /// <summary>
    /// Represents a single floating point property
    /// </summary>
    public class SingleProperty : Property
    {
        /// <summary>
        /// The value of the property
        /// </summary>
        public readonly float Value;

        /// <summary>
        /// Initializes a new instance of SingleProperty
        /// </summary>
        /// <param name="key">The target of the property</param>
        /// <param name="value">The value of the property</param>
        public SingleProperty(string key, float value) : base(key)
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
            if (!(obj is SingleProperty))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (SingleProperty)obj;
            return Math.Abs(other.Value - Value) < Mathf.Epsilon &&
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