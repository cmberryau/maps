using Maps.Geographical;

namespace Maps
{
    /// <summary>
    /// An interface for querying an object of it's coordinate
    /// </summary>
    public interface ICoordinateQueryable
    {
        /// <summary>
        /// The current 3d coordinte of the object
        /// </summary>
        Geodetic3d Coordinate
        {
            get;
        }
    }
}