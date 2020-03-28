using System;

namespace Maps.Geographical.Places
{
    /// <summary>
    /// Entry point to place sources
    /// </summary>
    public interface IPlaceProvider : IDisposable
    {
        /// <summary>
        /// The level of offline support provided
        /// </summary>
        OfflineSupportLevel OfflineSupport
        {
            get;
        }

        /// <summary>
        /// The place source
        /// </summary>
        IPlaceSource CreatePlaceSource();
    }
}