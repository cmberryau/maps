using Maps.Geographical;

namespace Maps
{
    /// <summary>
    /// An interface for control of an object using coordinate parameters
    /// </summary>
    public interface ICoordinateControllable : ICoordinateQueryable
    {
        /// <summary>
        /// The current 3d coordinte of the object
        /// </summary>
        new Geodetic3d Coordinate
        {
            get;
            set;
        }
    }
}