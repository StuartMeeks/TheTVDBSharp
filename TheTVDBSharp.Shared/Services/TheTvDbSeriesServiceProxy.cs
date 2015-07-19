using System;
using System.Threading.Tasks;
using System.IO;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Extensions;

namespace TheTVDBSharp.Services
{
    public class TheTvDbSeriesServiceProxy : TheTvDbProxyBase, ITheTvDbSeriesService
    {
        private const string FullSeriesUrlFormat = "{0}/api/{1}/series/{2}/all/{3}.zip";
        private const string SeriesUrlFormat = "{0}/api/{1}/series/{2}/all/{3}.xml";
        private const string SearchSeriesUrlFormat = "{0}/api/GetSeries.php?seriesname={1}&language={2}";

        public TheTvDbSeriesServiceProxy(ITheTvDbApiConfiguration apiConfiguration)
            : base(apiConfiguration)
        {
        }

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        public async Task<System.IO.Stream> RetrieveFull(uint showId, TheTvDbLanguage language)
#elif WINDOWS_UAP
        public async Task<Windows.Storage.Streams.IInputStream> RetrieveFull(uint showId, TheTvDbLanguage language)
#endif
        {
            var url = string.Format(FullSeriesUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                ProxyConfiguration.ApiKey, 
                showId, 
                language.ToShortString());

            var response = await GetAsync(url);

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
            return await response.Content.ReadAsStreamAsync();
#elif WINDOWS_UAP
            return (await response.Content.ReadAsStreamAsync()).AsInputStream();
#endif
        }

        public async Task<string> Retrieve(uint showId, TheTvDbLanguage language)
        {
            var url = string.Format(SeriesUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                ProxyConfiguration.ApiKey, 
                showId, 
                language.ToShortString());

            var response = await GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Search(string query, TheTvDbLanguage language)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var url = string.Format(SearchSeriesUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                query, 
                language.ToShortString());

            var response = await GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
