using System.Collections.Generic;
using System.Threading.Tasks;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Updates;

namespace TheTVDBSharp
{
    public interface ITheTvDbClient
    {

        Task<List<TheTvDbSeries>> SearchSeriesAsync(string query, TheTvDbLanguage language);

        Task<TheTvDbUpdateContainer> GetUpdatesAsync(TheTvDbInterval interval, bool compression = true);

        Task<TheTvDbSeries> GetSeriesAsync(uint seriesId, TheTvDbLanguage language, bool compression = true);

        Task<TheTvDbEpisode> GetEpisodeAsync(uint episodeId, TheTvDbLanguage language);

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        Task<System.IO.Stream> GetBannerAsync(string remotePath);
#elif WINDOWS_UAP
        Task<Windows.Storage.Streams.IInputStream> GetBannerAsync(string remotePath);
#endif

    }
}
