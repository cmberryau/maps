using System;

namespace Maps.Appearance.Properties
{
    /// <summary>
    /// Represents a boolean property
    /// </summary>
    public class BoolProperty : Property
    {
        /// <summary>
        /// The value of the bool property
        /// </summary>
        public readonly bool Value;

        /// <inheritdoc />
        public BoolProperty(string key, bool value) : base(key)
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
        public override bool Equals(object obj)
        {
            if (!(obj is BoolProperty))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (BoolProperty) obj;
            return other.Value == Value && other.Key == Key;
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