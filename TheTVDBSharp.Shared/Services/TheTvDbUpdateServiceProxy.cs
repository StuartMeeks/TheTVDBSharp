using System;
using System.IO;
using System.Threading.Tasks;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Extensions;

namespace TheTVDBSharp.Services
{
    public class TheTvDbUpdateServiceProxy : TheTvDbProxyBase, ITheTvDbUpdateService
    {
        private const string UpdateCompressedUrlFormat = "{0}/api/{1}/updates/updates_{2}.zip";
        private const string UpdateUncompressedUrlFormat = "{0}/api/{1}/updates/updates_{2}.xml";

        public TheTvDbUpdateServiceProxy(ITheTvDbApiConfiguration apiConfiguration)
            : base(apiConfiguration)
        {
        }

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        public async Task<System.IO.Stream> Retrieve(TheTvDbInterval interval)
#elif WINDOWS_UAP
        public async Task<Windows.Storage.Streams.IInputStream> Retrieve(TheTvDbInterval interval)
#endif
        {
            var url = string.Format(UpdateCompressedUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                ProxyConfiguration.ApiKey, 
                interval.ToApiString());

            var response = await GetAsync(url);

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
            return await response.Content.ReadAsStreamAsync();
#elif WINDOWS_UAP
            return (await response.Content.ReadAsStreamAsync()).AsInputStream();
#endif
        }

        public async Task<string> RetrieveUncompressed(TheTvDbInterval interval)
        {
            var url = string.Format(UpdateUncompressedUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                ProxyConfiguration.ApiKey, 
                interval.ToApiString());

            var response = await GetAsync(url);
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}
