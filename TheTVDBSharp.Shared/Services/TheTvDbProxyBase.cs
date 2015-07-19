using System;
using System.Threading.Tasks;

namespace TheTVDBSharp.Services
{
    public abstract class TheTvDbProxyBase
    {
        protected ITheTvDbApiConfiguration ProxyConfiguration { get; }

        protected TheTvDbProxyBase(ITheTvDbApiConfiguration apiConfiguration)
        {
            if (apiConfiguration == null) throw new ArgumentNullException(nameof(apiConfiguration));
            ProxyConfiguration = apiConfiguration;
        }

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        protected async Task<System.Net.Http.HttpResponseMessage> GetAsync(string url)
#elif WINDOWS_UAP
        protected async Task<System.Net.Http.HttpResponseMessage> GetAsync(string url)
#endif
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            try
            {
                var uri = new Uri(url);

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
                using (var client = new System.Net.Http.HttpClient())
#elif WINDOWS_UAP
                using (var client = new System.Net.Http.HttpClient())
#endif
                {
#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
                    // Setting timeout for httpclient on portable architecture. 
                    // UAP supports timeout configuration via System.Threading.Task
                    if (ProxyConfiguration.Timeout.HasValue)
                    {
                        client.Timeout = ProxyConfiguration.Timeout.Value;
                    }
#endif

                    var response = await client.GetAsync(uri, System.Net.Http.HttpCompletionOption.ResponseContentRead);
                    if (!response.IsSuccessStatusCode) throw new TheTvDbBadResponseException(response.StatusCode);

                    return response;
                }
            }
            catch (Exception e)
            {
                throw new TheTvDbServerNotAvailableException(inner: e);
            }
        }
    }
}
