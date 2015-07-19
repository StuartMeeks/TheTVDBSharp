using System;
using System.Linq;
using System.Threading.Tasks;
using TheTVDBSharp.Caching;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Updates;

namespace TheTVDBSharp
{
    public class TheTvDbManager1
    {

        private readonly ITheTvDbCache _cache;
        public ITheTvDbCache Cache => _cache;

        private readonly ITheTvDbClient _client;
        public ITheTvDbClient Client => _client;

        private readonly TheTvDbLanguage _preferredLanguage;
        public TheTvDbLanguage PreferredLanguage => _preferredLanguage;

        public TheTvDbManager1(ITheTvDbCache cache, ITheTvDbClient client, TheTvDbLanguage preferredLanguage)
        {
            _cache = cache;
            _client = client;
            _preferredLanguage = preferredLanguage;
        }

        public async void ApplyUpdatesAsync(IProgress<int> progress, bool includeBanners)
        {
            if (!_cache.LastUpdated.HasValue) return;

            var updates = await _client.GetUpdatesAsync(_cache.GetUpdateInterval());
            var cacheUpdates = GetCacheUpdates(updates);
            var totalUpdates = cacheUpdates.GetTotalUpdates(includeBanners);
            var completedUpdates = 0;

            foreach (var seriesUpdate in cacheUpdates.SeriesUpdates)
            {
                var series = await _client.GetSeriesAsync(seriesUpdate.SeriesId, _preferredLanguage, false);
                _cache.UpdateSeries(seriesUpdate.SeriesId, series);

                completedUpdates++;
                ReportProgress(completedUpdates, totalUpdates, progress);
            }

            foreach (var episodeUpdate in cacheUpdates.EpisodeUpdates)
            {
                var episode = await _client.GetEpisodeAsync(episodeUpdate.EpisodeId, _preferredLanguage);
                _cache.UpdateEpisode(episodeUpdate.SeriesId, episodeUpdate.EpisodeId, episode);

                completedUpdates++;
                ReportProgress(completedUpdates, totalUpdates, progress);
            }

            if (includeBanners)
            {
                foreach (var bannerUpdate in cacheUpdates.BannerUpdates)
                {
                    //TODO: Apply banner updates (for each type of banner)

                    completedUpdates++;
                    ReportProgress(completedUpdates, totalUpdates, progress);
                }
            }

            _cache.LastUpdated = updates.LastUpdated;
        }

        public async Task UpdateSeriesAsync(uint seriesId)
        {
            var series = await _client.GetSeriesAsync(seriesId, _preferredLanguage);
            _cache.UpdateSeries(series.SeriesId, series);
        }

        public void RemoveSeries(uint seriesId)
        {
            _cache.UpdateSeries(seriesId, null);
        }

        private TheTvDbUpdateContainer GetCacheUpdates(TheTvDbUpdateContainer updates)
        {
            var seriesCacheUpdates =
                updates.SeriesUpdates.Where(
                    seriesUpdate => _cache.Series.Any(p => p.SeriesId == seriesUpdate.SeriesId)).ToList();

            var episodeCacheUpdates =
                updates.EpisodeUpdates.Where(
                    episodeUpdate => _cache.Series.Any(p => p.SeriesId == episodeUpdate.SeriesId)).ToList();

            var bannerCacheUpdates =
                updates.BannerUpdates.Where(
                    bannerUpdate => _cache.Series.Any(p => p.SeriesId == bannerUpdate.SeriesId)).ToList();

            return new TheTvDbUpdateContainer
            {
                SeriesUpdates = seriesCacheUpdates,
                EpisodeUpdates = episodeCacheUpdates,
                BannerUpdates = bannerCacheUpdates,
                LastUpdated = updates.LastUpdated
            };
        }

        private void ReportProgress(int completed, int total, IProgress<int> progress)
        {
            progress.Report(completed / total * 100);
        }

    }
}
