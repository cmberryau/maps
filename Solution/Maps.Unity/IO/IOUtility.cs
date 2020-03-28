using System;
using System.IO;
using Maps.Extensions;
using UnityEngine;

namespace Maps.Unity.IO
{
    /// <summary>
    /// Provides IO utility methods for the Maps integration in Unity3d
    /// </summary>
    public static class IOUtility
    {
        /// <summary>
        ///    Provides a platform-specific character used to separate directory levels 
        ///    in a path string that reflects a hierarchical file system organization.
        /// </summary>
        public static char DirectorySeparatorChar
        {
            get
            {
                var seperator = Path.DirectorySeparatorChar;

                if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    // Path.DirectorySeperatorChar is incorrect on iOS
                    seperator = '/';
                }

                return seperator;
            }
        }

        /// <summary>
        /// Evaluates if a streaming asset exists, provides a complete uri
        /// </summary>
        /// <param name="filename">The filename of the asset to evaluate</param>
        /// <returns>The complete uri to access the resource</returns>
        public static string EnsureStreamingAsset(string filename)
        {
            return EnsureLocalResource(Application.streamingAssetsPath +
                                       DirectorySeparatorChar + Configuration.DefaultFolderName +
                                       DirectorySeparatorChar + filename);
        }

        /// <summary>
        /// Evaluates if the local resource exists, provides a modified uri
        /// </summary>
        /// <param name="uri">The uri to evaluate</param>
        /// <returns>The modified uri to access the resource</returns>
        public static string EnsureLocalResource(string uri)
        {
            if (uri.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Cannot be null or whitespace", nameof(uri));
            }

            var cleanedUri = uri;

            // android requires special treatment
            if (Application.platform == RuntimePlatform.Android)
            {
                var www = new WWW(uri);

                while (!www.isDone)
                {
                    Debug.Log($"Reading data from {uri}...");
                }

                if (!www.error.IsNullOrWhiteSpace())
                {
                    throw new IOException($"Error occured while reading: {www.error}");
                }

                Debug.Log($"Read total of {www.bytes.Length}");

                if (www.bytesDownloaded <= 0)
                {
                    throw new IOException("No bytes read");
                }

                throw new NotImplementedException();

                var writeUri = Application.persistentDataPath + DirectorySeparatorChar + 
                    "log4net.config";

                Debug.Log($"Writing data ( {www.bytes.Length} bytes) to {writeUri}");
                File.WriteAllBytes(writeUri, www.bytes);

                cleanedUri = writeUri;
            }
            else
            {
                if (!Directory.Exists(uri))
                {
                    if (!File.Exists(uri))
                    {
                        throw new ArgumentException("Could not validate uri");
                    }
                }
            }

            return cleanedUri;
        }
    }
}
