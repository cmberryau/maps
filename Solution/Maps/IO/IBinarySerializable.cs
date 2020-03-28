using System.IO;

namespace Maps.IO
{
    /// <summary>
    /// Interface for classes that are binary serializable
    /// </summary>
    public interface IBinarySerializable<out T>
    {
        /// <summary>
        /// Serializes the instance
        /// </summary>
        /// <param name="destination">The destination stream</param>
        void Serialize(Stream destination);

        /// <summary>
        /// Deserializes the instance
        /// </summary>
        /// <param name="source">The source stream</param>
        T Deserialize(Stream source);
    }
}