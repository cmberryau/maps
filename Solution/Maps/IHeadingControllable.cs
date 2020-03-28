namespace Maps
{
    /// <summary>
    /// An interface for objects which have a controllable heading
    /// </summary>
    public interface IHeadingControllable : IHeadingQueryable
    {
        /// <summary>
        /// The heading of the object in degrees
        /// </summary>
        new double Heading
        {
            get;
            set;
        }
    }
}