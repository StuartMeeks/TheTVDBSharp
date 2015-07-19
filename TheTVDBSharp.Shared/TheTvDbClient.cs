using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Parsers;
using TheTVDBSharp.Services;
using TheTVDBSharp.Updates;

namespace TheTVDBSharp
{
    /// <summary>
    /// The main class which will handle all user interaction
    /// </summary>
    public class TheTvDbClient : ITheTvDbClient
    {
        private readonly ITheTvDbEpisodeService _episodeServiceProxy;
        private readonly ITheTvDbSeriesService _seriesServiceProxy;
        private readonly ITheTvDbUpdateService _updateServiceProxy;
        private readonly ITheTvDbBannerService _bannerServiceProxy;

        private readonly ITheTvDbEpisodeParser _episodeParser;
        private readonly ITheTvDbSeriesParser _seriesParser;
        private readonly ITheTvDbUpdateParser _updateParser;

        /// <summary>
        /// Creates a new instance with the provided api configuration
        /// </summary>
        /// <param name="apiConfiguration">The API configuration</param>
        public TheTvDbClient(ITheTvDbApiConfiguration apiConfiguration)
        {
            if (apiConfiguration == null)
                throw new ArgumentNullException(nameof(apiConfiguration));
            if (string.IsNullOrWhiteSpace(apiConfiguration.BaseUrl))
                throw new ArgumentOutOfRangeException(nameof(apiConfiguration), "Base url must be set");

            // Proxy Services
            _seriesServiceProxy = new TheTvDbSeriesServiceProxy(apiConfiguration);
            _episodeServiceProxy = new TheTvDbEpisodeServiceProxy(apiConfiguration);
            _updateServiceProxy = new TheTvDbUpdateServiceProxy(apiConfiguration);
            _bannerServiceProxy = new TheTvDbBannerServiceProxy(apiConfiguration);

            // Initialize parse services
            var actorParser = new TheTvDbActorParser();
            var bannerParser = new TheTvDbBannerParser();
            _episodeParser = new TheTvDbEpisodeParser();
            _seriesParser = new TheTvDbSeriesParser(actorParser, bannerParser, _episodeParser);
            _updateParser = new TheTvDbUpdateParser();
        }

        /// <summary>
        /// Creates a new instance with the provided API key and a base api url
        /// </summary>
        /// <param name="apiKey">The API key provided by TVDB</param>
        /// <param name="baseUrl">The API base url</param>
        public TheTvDbClient(string apiKey, string baseUrl = "http://thetvdb.com")
            : this(new TheTvDbApiConfiguration(apiKey, baseUrl))
        {
        }

        /// <summary>
        /// Search for a series with a given query and a language and returns null if api response is not well formed
        /// </summary>
        /// <param name="query">Query that identifies the series.</param>
        /// <param name="language">Series language.</param>
        /// <returns>Returns a readonly collection of series or null if response is not well formed</returns>
        public async Task<List<TheTvDbSeries>> SearchSeriesAsync(string query, TheTvDbLanguage language)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var seriesCollectionRaw = await _seriesServiceProxy.Search(query, language);
            return _seriesParser.ParseSearch(seriesCollectionRaw);
        }

        /// <summary>
        /// Get all updates since a given interval.
        /// </summary>
        /// <param name="interval">The interval you need to retrieve. 
        /// E.g. if you have last updated your data the same day than you should set Interval.Day. 
        /// The higher the interval the higher the bandwidth costs are.</param>
        /// <param name="compression">Only set compress mode to false when you know what your are doing. 
        /// Disabled compression raises the bandwith costs a lot.</param>
        /// <returns>Returns the update container which consists of all update elements.</returns>
        public async Task<TheTvDbUpdateContainer> GetUpdatesAsync(TheTvDbInterval interval, bool compression = true)
        {
            if (compression)
            {
                var updateContainerStream = await _updateServiceProxy.Retrieve(interval);
                return _updateParser.Parse(updateContainerStream, interval);
            }

            var updateContainerRaw = await _updateServiceProxy.RetrieveUncompressed(interval);
            return _updateParser.ParseUncompressed(updateContainerRaw);
        }

        /// <summary>
        /// Get a specific series based on its id and if compression mode is true also all banners and actors; 
        /// or null if api response is not well formed
        /// </summary>
        /// <param name="seriesId">Id of the series.</param>
        /// <param name="language">Language of the series.</param>
        /// <param name="compression">Set compression mode to false if you want to have an uncompressed transmission 
        /// which increases the bandwith load a lot. Compressed transmission also loads all banners and actors.</param>
        /// <returns>Returns the corresponding series or null if api response is not well formed</returns>
        public async Task<TheTvDbSeries> GetSeriesAsync(uint seriesId, TheTvDbLanguage language, bool compression = true)
        {
            if (compression)
            {
                var fullSeriesStream = await _seriesServiceProxy.RetrieveFull(seriesId, language);
                return await _seriesParser.ParseFull(fullSeriesStream, language);
            }
            
            var seriesRaw = await _seriesServiceProxy.Retrieve(seriesId, language);
            return _seriesParser.Parse(seriesRaw);
        }

        /// <summary>
        /// Get a specific episode based on its id and the given language and returns null if api response is not well formed
        /// </summary>
        /// <param name="episodeId">Id of the episode</param>
        /// <param name="language">Episode language</param>
        /// <returns>The corresponding episode or null if api response is not well formed</returns>
        public async Task<TheTvDbEpisode> GetEpisodeAsync(uint episodeId, TheTvDbLanguage language)
        {
            var episodeRaw = await _episodeServiceProxy.Retrieve(episodeId, language);
            return _episodeParser.Parse(episodeRaw);
        }

        /// <summary>
        ///  Get a specific banner based on its remote path.
        /// </summary>
        /// <param name="remotePath">The remote path of the banner which can be 
        /// found in the BannerBase or in the BannerUpdate model.</param>
        /// <returns>Returns the banner as byte array.</returns>
#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        public async Task<System.IO.Stream> GetBannerAsync(string remotePath)
#elif WINDOWS_UAP
        public async Task<Windows.Storage.Streams.IInputStream> GetBannerAsync(string remotePath)
#endif
        {
            if (remotePath == null) throw new ArgumentNullException(nameof(remotePath));

            return await _bannerServiceProxy.Retrieve(remotePath);
        }
    }
}