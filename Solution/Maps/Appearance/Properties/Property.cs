using System;
using Maps.Extensions;

namespace Maps.Appearance.Properties
{
    /// <summary>
    /// Base class for all properties
    /// </summary>
    public abstract class Property
    {
        /// <summary>
        /// The key of the property
        /// </summary>
        public readonly string Key;

        /// <summary>
        /// Initializes a new instance of Property
        /// </summary>
        /// <param name="key">The key of the property</param>
        protected Property(string key)
        {
            if (key.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Value cannot be null or whitespace", 
                    nameof(key));
            }

            Key = key;
        }

        /// <summary>
        /// Accepts an IPropertyVisitor and calls it's concrete method
        /// </summary>
        /// <param name="visitor">The visitor</param>
        public abstract void Accept(IPropertyVisitor visitor);

        /// <summary>
        /// Accepts an IPropertyVisitor and calls it's concrete method
        /// </summary>
        /// <param name="visitor">The visitor</param>
        /// <param name="param">The additonal parameter</param>
        public abstract TResult Accept<TResult, T0>(IPropertyVisitor<TResult, 
            T0> visitor, T0 param);

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Target: {Key}";
        }

        /// <inheritdoc />
        public abstract override bool Equals(object obj);

        /// <inheritdoc />
        public abstract override int GetHashCode();
    }
}