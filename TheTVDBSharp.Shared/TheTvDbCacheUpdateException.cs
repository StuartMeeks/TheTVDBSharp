using System;

namespace TheTVDBSharp
{
    public class TheTvDbCacheUpdateException : Exception
    {

        public TheTvDbCacheUpdateException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {
        }

    }
}
