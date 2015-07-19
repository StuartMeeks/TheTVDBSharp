using System;

namespace TheTVDBSharp
{
    public class TheTvDbParseException : Exception
    {

        public TheTvDbParseException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {
        }

    }
}
