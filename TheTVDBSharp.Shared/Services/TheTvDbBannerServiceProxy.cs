using System.IO;
using System.Threading.Tasks;

namespace TheTVDBSharp.Services
{
    public class TheTvDbBannerServiceProxy : TheTvDbProxyBase, ITheTvDbBannerService
    {
        private const string BannerUrlFormat = "{0}/banners/{1}";

        public TheTvDbBannerServiceProxy(ITheTvDbApiConfiguration apiConfiguration) 
            : base(apiConfiguration)
        {
        }

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        public async Task<System.IO.Stream> Retrieve(string remotePath)
#elif WINDOWS_UAP
        public async Task<Windows.Storage.Streams.IInputStream> Retrieve(string remotePath)
#endif
        {
            var url = string.Format(BannerUrlFormat, 
                ProxyConfiguration.BaseUrl, 
                remotePath);

            var response = await GetAsync(url);

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
            return await response.Content.ReadAsStreamAsync();
#elif WINDOWS_UAP
            return (await response.Content.ReadAsStreamAsync()).AsInputStream();
#endif
        }
    }
}
