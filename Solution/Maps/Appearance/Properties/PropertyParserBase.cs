using System;
using System.Collections.Generic;

namespace Maps.Appearance.Properties
{
    /// <summary>
    /// Parses properties
    /// </summary>
    public abstract class PropertyParserBase : IPropertyVisitor
    {
        /// <summary>
        /// The properties to parse
        /// </summary>
        protected readonly IList<Property> Properties;

        /// <summary>
        /// Initializes a new instance of PropertyParserBase
        /// </summary>
        /// <param name="properties">The properties to parse</param>
        protected PropertyParserBase(IList<Property> properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            Properties = properties;
        }

        /// <summary>
        /// Parses the list of properties given at construction
        /// </summary>
        public void Parse()
        {
            OnParseBegin();

            foreach (var property in Properties)
            {
                property.Accept(this);
            }

            OnParseComplete();
        }

        /// <summary>
        /// Called after property parsing is complete
        /// </summary>
        protected virtual void OnParseBegin()
        {

        }

        /// <summary>
        /// Called after property parsing is complete
        /// </summary>
        protected virtual void OnParseComplete()
        {
            
        }

        /// <inheritdoc />
        public virtual void Visit(Int32Property property)
        {
            
        }

        /// <inheritdoc />
        public virtual void Visit(SingleProperty property)
        {
            
        }

        /// <inheritdoc />
        public virtual void Visit(DoubleProperty property)
        {
            
        }

        /// <inheritdoc />
        public virtual void Visit(ColorProperty property)
        {
            
        }

        /// <inheritdoc />
        public virtual void Visit(NameProperty property)
        {
            
        }

        /// <inheritdoc />
        public virtual void Visit(StringProperty property)
        {
            
        }

        /// <inheritdoc />
        public virtual void Visit(BoolProperty property)
        {
            
        }
    }
}