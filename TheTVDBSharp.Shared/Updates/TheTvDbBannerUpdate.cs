using System;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Updates
{
    public class TheTvDbBannerUpdate
    {
        public uint SeriesId { get; set; }

        public uint? SeasonNumber { get; set; }

        public string RemotePath { get; set; }

        public TheTvDbLanguage? Language { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
