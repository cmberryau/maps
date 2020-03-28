namespace Maps
{
    /// <summary>
    /// Interface for objects which have a queryable heading
    /// </summary>
    public interface IHeadingQueryable
    {
        /// <summary>
        /// The heading of the object in degrees
        /// </summary>
        double Heading
        {
            get;
        }
    }
}