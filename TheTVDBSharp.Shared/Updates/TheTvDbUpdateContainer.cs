using System;
using System.Collections.Generic;

namespace TheTVDBSharp.Updates
{
    public class TheTvDbUpdateContainer
    {
        public DateTime LastUpdated { get; set; }

        public List<TheTvDbSeriesUpdate> SeriesUpdates { get; set; }

        public List<TheTvDbEpisodeUpdate> EpisodeUpdates { get; set; }

        public List<TheTvDbBannerUpdate> BannerUpdates { get; set; }

        public int GetTotalUpdates(bool includeBanners)
        {
            var seriesUpdates = SeriesUpdates?.Count ?? 0;
            var episodeUpdates = EpisodeUpdates?.Count ?? 0;
            var bannerUpdates = BannerUpdates?.Count ?? 0;

            return includeBanners
                ? seriesUpdates + episodeUpdates + bannerUpdates
                : seriesUpdates + episodeUpdates;
        }
    }
}
