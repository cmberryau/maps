using System;

namespace Maps.Http
{
    /// <summary>
    /// Threadsafe (mutex) rest client
    /// </summary>
    public abstract class RestClient : IDisposable
    {
        /// <summary>
        /// The base Url for requests
        /// </summary>
        protected readonly string BaseUrl;

        /// <summary>
        /// The timeout limit for requests
        /// </summary>
        protected readonly int Timeout;

        private const int DefaultTimeout = 5000;

        /// <summary>
        /// Initialises the base fields of the RestClient
        /// </summary>
        /// <param name="baseUrl">The base URL to use for requests e.g 
        /// "https://reverse.geocoder.cit.api.here.com/6.2/"</param>
        /// <param name="timeout">The optional timeout parameter</param>
        protected RestClient(string baseUrl, int timeout = DefaultTimeout)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentException("Argument is null or empty", nameof(baseUrl));
            }

            BaseUrl = baseUrl;
            Timeout = timeout;
        }

        /// <summary>
        /// Executes the given RestRequest and returns the string result
        /// </summary>
        /// <param name="request">The request to execute</param>
        /// <param name="excludeBaseUrl">Should the base URL be excluded?</param>
        public abstract string Execute(RestRequest request, bool excludeBaseUrl = false);

        /// <summary>
        /// Disposes of all resources held by the RestClient
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Returns the RestClient object as a string
        /// </summary>
        public override string ToString()
        {
            return BaseUrl;
        }
    }
}