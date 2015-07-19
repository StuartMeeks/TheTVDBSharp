using System;
using System.Threading.Tasks;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Extensions;

namespace TheTVDBSharp.Services
{
    public class TheTvDbEpisodeServiceProxy : TheTvDbProxyBase, ITheTvDbEpisodeService
    {
        private const string EpisodeUrlFormat = "{0}/api/{1}/episodes/{2}/{3}.xml";

        public TheTvDbEpisodeServiceProxy(ITheTvDbApiConfiguration apiConfiguration)
            : base(apiConfiguration)
        {
        }

        public async Task<string> Retrieve(uint episodeId, TheTvDbLanguage language)
        {
            var url = string.Format(EpisodeUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                ProxyConfiguration.ApiKey, 
                episodeId, 
                language.ToShortString());
            
            var response = await GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
