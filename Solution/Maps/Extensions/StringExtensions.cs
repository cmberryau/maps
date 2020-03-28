namespace Maps.Extensions
{
    /// <summary>
    /// Provides useful extensions for the string class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Evaluates if the string is empty or whitespace
        /// </summary>
        /// <param name="s">The string to evaluate</param>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            if (s == null || s.Trim().Length == 0)
            {
                return true;
            }

            return false;
        }
    }
}