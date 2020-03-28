using System;

namespace Maps.IO
{
    /// <summary>
    /// A source of strings
    /// </summary>
    public interface IStringSource : IDisposable
    {
        /// <summary>
        /// Evaluates if the IStringSource has a string
        /// </summary>
        /// <param name="id">The id to search for</param>
        bool HasString(long id);

        /// <summary>
        /// Evaluates if the IStringSource has a string already
        /// </summary>
        /// <param name="content">The string content to search for</param>
        /// <exception cref="System.ArgumentException">Thrown when string
        /// is null, empty or white space</exception>
        bool HasString(string content);

        /// <summary>
        /// Gets a string id
        /// </summary>
        /// <param name="content">The string content to search for</param>
        /// <exception cref="System.ArgumentException">Thrown when string
        /// is null, empty or white space</exception>
        long GetId(string content);

        /// <summary>
        /// Gets a string
        /// </summary>
        /// <param name="id">The id to search for</param>
        string Get(long id);
    }
}