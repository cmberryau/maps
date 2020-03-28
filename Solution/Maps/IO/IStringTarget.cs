namespace Maps.IO
{
    /// <summary>
    /// A target for strings
    /// </summary>
    public interface IStringTarget : IStringSource
    {
        /// <summary>
        /// Writes a string
        /// </summary>
        /// <param name="id">The id to write to</param>
        /// <param name="content">The string content to write</param>
        void Write(long id, string content);
    }
}