using System;

namespace Maps.Http
{
    /// <summary>
    /// REST request class used together with RestClient
    /// to create complete RESTful requests without exposing
    /// users to the implementation
    /// </summary>
    public sealed class RestRequest
    {
        /// <summary>
        /// The URL suffix to be appended to the base URL
        /// </summary>
        public string UrlSuffix
        {
            get
            {
                if (_urlSuffix == null)
                {
                    return _unpreparedUrl;
                }

                return _urlSuffix;
            }
            private set
            {
                _urlSuffix = value;
            }
        }

        private string _urlSuffix;
        private readonly string _unpreparedUrl;

        /// <summary>
        /// Initializes a new instance of RestRequest
        /// </summary>
        /// <param name="unpreparedUrl">The continuation of the base URL with tags for replacement</param>
        public RestRequest(string unpreparedUrl)
        {
            if (string.IsNullOrEmpty(unpreparedUrl))
            {
                throw new ArgumentException("Argument is null or empty", nameof(unpreparedUrl));
            }

            _unpreparedUrl = unpreparedUrl;
        }

        /// <summary>
        /// Replaces matching tags in the Url with the given value
        /// </summary>
        /// <param name="tag">The tag to search for</param>
        /// <param name="value">The value to replace with</param>
        public void AddValue(string tag, string value)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentException("Argument is null or empty", nameof(tag));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Argument is null or empty", nameof(value));
            }

            if (!_unpreparedUrl.Contains(tag))
            {
                throw new ArgumentException("Argument not found in Url", nameof(tag));
            }

            if (UrlSuffix == null)
            {
                UrlSuffix = _unpreparedUrl.Replace(tag, value);
            }
            else
            {
                UrlSuffix = UrlSuffix.Replace(tag, value);
            }
        }

        /// <summary>
        /// Returns the RestRequest object as a string
        /// </summary>
        public override string ToString()
        {
            return UrlSuffix;
        }
    }
}