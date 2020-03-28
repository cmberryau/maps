namespace Maps.Appearance.Properties
{
    /// <summary>
    /// Interface for concrete property access
    /// </summary>
    public interface IPropertyVisitor
    {
        /// <summary>
        /// Visits an Int32Property
        /// </summary>
        /// <param name="property">The property to visit</param>
        void Visit(Int32Property property);

        /// <summary>
        /// Visits a SingleProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        void Visit(SingleProperty property);

        /// <summary>
        /// Visits a DoubleProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        void Visit(DoubleProperty property);

        /// <summary>
        /// Visits a ColorProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        void Visit(ColorProperty property);

        /// <summary>
        /// Visits a NameProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        void Visit(NameProperty property);

        /// <summary>
        /// Visits a StringProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        void Visit(StringProperty property);

        /// <summary>
        /// Visits a BoolProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        void Visit(BoolProperty property);
    }

    /// <summary>
    /// Interface for concrete property access
    /// </summary>
    public interface IPropertyVisitor<out TResult, in T0>
    {
        /// <summary>
        /// Visits an Int32Property
        /// </summary>
        /// <param name="property">The property to visit</param>
        /// <param name="param">The additional parameter</param>
        TResult Visit(Int32Property property, T0 param);

        /// <summary>
        /// Visits a SingleProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        /// <param name="param">The additional parameter</param>
        TResult Visit(SingleProperty property, T0 param);

        /// <summary>
        /// Visits a DoubleProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        /// <param name="param">The additional parameter</param>
        TResult Visit(DoubleProperty property, T0 param);

        /// <summary>
        /// Visits a ColorProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        /// <param name="param">The additional parameter</param>
        TResult Visit(ColorProperty property, T0 param);

        /// <summary>
        /// Visits a NameProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        /// <param name="param">The additional parameter</param>
        TResult Visit(NameProperty property, T0 param);

        /// <summary>
        /// Visits a StringProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        /// <param name="param">The additional parameter</param>
        TResult Visit(StringProperty property, T0 param);

        /// <summary>
        /// Visits a BoolProperty
        /// </summary>
        /// <param name="property">The property to visit</param>
        /// <param name="param">The additional parameter</param>
        TResult Visit(BoolProperty property, T0 param);
    }
}