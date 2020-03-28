using System;
using System.IO;
using System.Net;

namespace Maps.Http
{
    /// <summary>
    /// The default REST client
    /// </summary>
    public sealed class DefaultRestClient : RestClient
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of DefaultRestClient
        /// </summary>
        /// <param name="baseUrl"></param>
        public DefaultRestClient(string baseUrl)
            : base(baseUrl)
        {
            
        }

        /// <summary>
        /// Executes the given RestRequest and returns the string result
        /// </summary>
        /// <param name="request">The request to execute</param>
        /// <param name="excludeBaseUrl">Should the base URL be excluded?</param>
        public override string Execute(RestRequest request, bool excludeBaseUrl = false)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DefaultRestClient));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var completeUrl = excludeBaseUrl ? request.UrlSuffix : BaseUrl + request.UrlSuffix;
            var webRequest = WebRequest.Create(completeUrl) as HttpWebRequest;

            if (webRequest == null)
            {
                throw new NullReferenceException("WebRequest.Create could not create a " +
                                                 "HttpWebRequest object");
            }

            webRequest.Timeout = Timeout;
            webRequest.KeepAlive = false;

            try
            {
                using (var response = (HttpWebResponse) webRequest.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var responseString = reader.ReadToEnd();
                            webRequest.Abort();

                            return responseString;
                        }
                    }
                }
            }
            finally
            {
                webRequest.Abort();
            }
        }

        /// <summary>
        /// Closes all resources held by the DefaultRestClient
        /// </summary>
        public override void Dispose()
        {
            // DefaultRestClient does not hold any resources
            _disposed = true;
        }
    }
}