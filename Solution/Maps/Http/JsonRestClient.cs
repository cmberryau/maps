using System;
using System.IO;
using Newtonsoft.Json;

namespace Maps.Http
{
    /// <summary>
    /// Threadsafe (mutex) rest client for deserialising strongly typed objects from json
    /// </summary>
    /// <typeparam name="T">An object that can be deserialized from json</typeparam>
    public sealed class JsonRestClient <T> : IDisposable
    {
        private readonly RestClient _restClient;
        private readonly JsonSerializer _jsonSerializer;
        private readonly object _threadLock;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of JsonRestClient
        /// </summary>
        /// <param name="restClient">The rest client to use for executing requests</param>
        public JsonRestClient(RestClient restClient)
        {
            if (restClient == null)
            {
                throw new ArgumentNullException(nameof(restClient));
            }

            _threadLock = new object();

            lock (_threadLock)
            {
                _restClient = restClient;
                _jsonSerializer = new JsonSerializer();
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
                throw new ObjectDisposedException(nameof(JsonRestClient<T>));
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
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            return _jsonSerializer.Deserialize<T>(jsonReader);
                        }
                    }
                }
            }

            throw new InvalidOperationException(nameof(JsonRestClient<T>) + " has been disposed");
        }

        /// <summary>
        /// Closes all connections initiated by the JsonRestClient
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

        /// <summary>
        /// Returns the JsonRestClient object as a string
        /// </summary>
        public override string ToString()
        {
            return _restClient.ToString();
        }
    }
}