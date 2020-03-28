using Maps.Geographical.Places;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Visitor pattern interface for the concrete Feature classes
    /// </summary>
    public interface IFeatureVisitor
    {
        /// <summary>
        /// Visits a Place instance
        /// </summary>
        /// <param name="place">The place instance to visit</param>
        void Visit(Place place);

        /// <summary>
        /// Visits a Segment instance
        /// </summary>
        /// <param name="segment">The segment instance to visit</param>
        void Visit(Segment segment);

        /// <summary>
        /// Visits an Area instance
        /// </summary>
        /// <param name="area">The area instance to visit</param>
        void Visit(Area area);
    }


    /// <summary>
    /// Visitor pattern interface for the concrete Feature classes
    /// </summary>
    /// <typeparam name="TResult">The return type</typeparam>
    public interface IFeatureVisitor<out TResult>
    {
        /// <summary>
        /// Visits a Place instance
        /// </summary>
        /// <param name="place">The place instance to visit</param>
        TResult Visit(Place place);

        /// <summary>
        /// Visits a Segment instance
        /// </summary>
        /// <param name="segment">The segment instance to visit</param>
        TResult Visit(Segment segment);

        /// <summary>
        /// Visits an Area instance
        /// </summary>
        /// <param name="area">The area instance to visit</param>
        TResult Visit(Area area);
    }

    /// <summary>
    /// Visitor pattern interface for the concrete Feature classes
    /// </summary>
    /// <typeparam name="TResult">The return type</typeparam>
    /// <typeparam name="T0">The first parameter type</typeparam>
    public interface IFeatureVisitor<out TResult, in T0>
    {
        /// <summary>
        /// Visits a Place instance
        /// </summary>
        /// <param name="place">The place instance to visit</param>
        /// <param name="param">The first parameter</param>
        TResult Visit(Place place, T0 param);

        /// <summary>
        /// Visits a Segment instance
        /// </summary>
        /// <param name="segment">The segment instance to visit</param>
        /// <param name="param">The first parameter</param>
        TResult Visit(Segment segment, T0 param);

        /// <summary>
        /// Visits an Area instance
        /// </summary>
        /// <param name="area">The area instance to visit</param>
        /// <param name="param">The first parameter</param>
        TResult Visit(Area area, T0 param);
    }

    /// <summary>
    /// Visitor pattern interface for the concrete Feature classes
    /// </summary>
    /// <typeparam name="TResult">The return type</typeparam>
    /// <typeparam name="T0">The first parameter type</typeparam>
    /// <typeparam name="T1">The second parameter type</typeparam>
    public interface IFeatureVisitor<out TResult, in T0, in T1>
    {
        /// <summary>
        /// Visits a Place instance
        /// </summary>
        /// <param name="place">The place instance to visit</param>
        /// <param name="param">The first parameter</param>
        /// <param name="param2">The second parameter</param>
        TResult Visit(Place place, T0 param, T1 param2);

        /// <summary>
        /// Visits a Segment instance
        /// </summary>
        /// <param name="segment">The segment instance to visit</param>
        /// <param name="param">The first parameter</param>
        /// <param name="param2">The second parameter</param>
        TResult Visit(Segment segment, T0 param, T1 param2);

        /// <summary>
        /// Visits an Area instance
        /// </summary>
        /// <param name="area">The area instance to visit</param>
        /// <param name="param">The first parameter</param>
        /// <param name="param2">The second parameter</param>
        TResult Visit(Area area, T0 param, T1 param2);
    }
}