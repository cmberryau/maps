using System;

namespace Maps.Appearance.Properties
{
    /// <summary>
    /// Represents a name property of an object
    /// </summary>
    public class NameProperty : StringProperty
    {
        private const string NameTarget = "name";

        /// <summary>
        /// Initializes a new NameProperty instance
        /// </summary>
        /// <param name="name">The name for the property</param>
        public NameProperty(string name) : base(NameTarget, name)
        {

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
    }
}