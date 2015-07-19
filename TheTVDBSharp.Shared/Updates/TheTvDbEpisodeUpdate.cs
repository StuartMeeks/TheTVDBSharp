using System;

namespace TheTVDBSharp.Updates
{
    public class TheTvDbEpisodeUpdate
    {
        public uint EpisodeId { get; set; }

        public uint SeriesId { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}