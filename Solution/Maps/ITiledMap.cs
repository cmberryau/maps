using Maps.Appearance;

namespace Maps
{
    /// <summary>
    /// Delegate for lod changes
    /// </summary>
    /// <param name="lastLod">The old lod</param>
    /// <param name="newLod">The new lod</param>
    public delegate void MapLodChangedHandler(int lastLod, int newLod);

    /// <summary>
    /// Interface for maps partitioned by tiles
    /// </summary>
    public interface ITiledMap : IMap
    {
        /// <summary>
        /// The tiled map appearance
        /// </summary>
        TiledMapAppearance Appearance
        {
            get;
            set;
        }

        /// <summary>
        /// The current lod
        /// </summary>
        int Lod
        {
            get;
        }

        /// <summary>
        /// Called when the lod has changed
        /// </summary>
        event MapLodChangedHandler LodChanged;
    }
}