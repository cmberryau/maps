using System;
using System.IO;
using System.Xml.Serialization;

namespace Maps.Http
{
    /// <summary>
    /// Threadsafe (mutex) rest client for deserialising strongly typed objects from xml
    /// </summary>
    /// <typeparam name="T">An object that can be deserialized from XML</typeparam>
    public sealed class XmlRestClient <T> : IDisposable
    {
        private readonly RestClient _restClient;
        private readonly XmlSerializer _xmlSerializer;
        private readonly object _threadLock;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of XmlRestClient
        /// </summary>
        /// <param name="restClient">The rest client to use for executing requests</param>
        public XmlRestClient(RestClient restClient)
        {
            if (restClient == null)
            {
                throw new ArgumentNullException(nameof(restClient));
            }

            _threadLock = new object();

            lock (_threadLock)
            {
                _restClient = restClient;
                _xmlSerializer = new XmlSerializer(typeof(T));
            }
        }

        /// <summary>
        /// Executes the given RestRequest and attempts to deserialize the result
        /// </summary>
        /// <param name="request">The request to execute</param>
        /// <param name="excludeBaseUrl">Should the base URL be excluded?</param>
        public T ExecuteDeserialize(RestRequest request, bool excludeBaseUrl = false)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(XmlRestClient<T>));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            lock (_threadLock)
            {
                if (_restClient != null)
                {
                    var responseString = _restClient.Execute(request, excludeBaseUrl);

                    using (var reader = new StringReader(responseString))
                    {
                        return (T)_xmlSerializer.Deserialize(reader);
                    }
                }
            }

            throw new InvalidOperationException(nameof(XmlRestClient<T>) + " has been disposed");
        }

        /// <summary>
        /// Closes all connections initiated by the XmlRestClient
        /// </summary>
        public void Dispose()
        {
            lock (_threadLock)
            {
                // all connections are held by the rest client
                if (_restClient != null)
                {
                    _restClient.Dispose();
                }                
            }

            _disposed = true;
        }
    }
}