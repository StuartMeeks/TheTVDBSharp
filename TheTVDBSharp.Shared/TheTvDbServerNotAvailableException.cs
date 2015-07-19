using System;

namespace TheTVDBSharp
{
    public class TheTvDbServerNotAvailableException : Exception
    {
        public TheTvDbServerNotAvailableException(
            string message = "Server is currently not available. Please try again later", Exception inner = null)
            : base(message, inner)
        {
        }
    }
}
