using System;

namespace TheTVDBSharp.Extensions
{
    /// <summary>
    /// Provides Date and Time extension methods.
    /// </summary>
    public static class UintExtensions
    {
        public static DateTime ToDateTime(this uint unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime).ToLocalTime();
        }
    }
}