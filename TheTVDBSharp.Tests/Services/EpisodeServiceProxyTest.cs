using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class EpisodeServiceProxyTest
    {
        readonly ITheTvDbEpisodeService _episodeService = new TheTvDbEpisodeServiceProxy(GlobalConfiguration.ApiConfiguration);

        [TestMethod]
        public async Task Retrieve_Episode_306213_Test()
        {
            var realEpisodeRaw = await _episodeService.Retrieve(306213, TheTvDbLanguage.English);
            Assert.IsNotNull(realEpisodeRaw);
        }
    }
}
