using System;
using System.IO;
using System.Text;

namespace Maps.Logging
{
    /// <summary>
    /// Responsible for forwarding string messages to a given method
    /// </summary>
    public class ForwardingTextWriter : TextWriter
    {
        /// <inheritdoc />
        public override Encoding Encoding
        {
            get;
        }

        private readonly Action<string> _debugAction;

        /// <summary>
        /// Initializes a new instance of ForwardingTextWriter
        /// </summary>
        /// <param name="debugAction">The debug action</param>
        public ForwardingTextWriter(Action<string> debugAction)
        {
            if (debugAction == null)
            {
                throw new ArgumentNullException(nameof(debugAction));
            }

            Encoding = new UTF8Encoding();
            _debugAction = debugAction;            
        }

        /// <summary>
        /// Initializes a new instance of ForwardingTextWriter
        /// </summary>
        /// <param name="debugAction">The debug action</param>
        /// <param name="encoding">The encoding to use</param>
        public ForwardingTextWriter(Action<string> debugAction, Encoding encoding)
        {
            if (debugAction == null)
            {
                throw new ArgumentNullException(nameof(debugAction));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            Encoding = encoding;
            _debugAction = debugAction;
        }

        /// <inheritdoc />
        public override void Write(string value)
        {
            _debugAction(value);
        }
    }
}