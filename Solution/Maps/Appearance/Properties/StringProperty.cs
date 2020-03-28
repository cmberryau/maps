using System;
using Maps.Extensions;

namespace Maps.Appearance.Properties
{
    /// <summary>
    /// Represents a string property of an object
    /// </summary>
    public class StringProperty : Property
    {
        /// <summary>
        /// The actual string content of the string property
        /// </summary>
        public readonly string Value;

        /// <summary>
        /// Initializes a new instance of StringProperty
        /// </summary>
        /// <param name="key">The target of the property</param>
        /// <param name="value">The content of the property</param>
        public StringProperty(string key, string value) : base(key)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Cannot be null or whitespace",
                    nameof(value));
            }

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
            return $"{base.ToString()}, Content:{Value}";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is StringProperty))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (StringProperty)obj;
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