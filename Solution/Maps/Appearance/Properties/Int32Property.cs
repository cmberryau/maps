using System;

namespace Maps.Appearance.Properties
{
    /// <summary>
    /// Represents a 32bit integer property
    /// </summary>
    public class Int32Property : Property
    {
        /// <summary>
        /// The value of the property
        /// </summary>
        public readonly int Value;

        /// <summary>
        /// Initializes a new instance of Int32Property
        /// </summary>
        /// <param name="key">The target of the property</param>
        /// <param name="value">The value of the property</param>
        public Int32Property(string key, int value) : base(key)
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
            if (!(obj is Int32Property))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (Int32Property)obj;
            return other.Value == Value && other.Key.Equals(Key);
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