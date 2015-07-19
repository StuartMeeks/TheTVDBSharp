using System;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Extensions
{
    public static class TimeSpanExtensions
    {

        public static TheTvDbInterval ToTheTvDbInterval(this TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays < 1)
            {
                return TheTvDbInterval.Day;
            }

            if (timeSpan.TotalDays < 7)
            {
                return TheTvDbInterval.Week;
            }

            if (timeSpan.TotalDays < 30)
            {
                return TheTvDbInterval.Month;
            }

            return TheTvDbInterval.All;
        }
    }
}
