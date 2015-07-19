using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class SeriesServiceProxyTest
    {
        private readonly ITheTvDbSeriesService _seriesServiceProxy = new TheTvDbSeriesServiceProxy(GlobalConfiguration.ApiConfiguration);

        [TestMethod]
        public async Task Search_Series_Scrubs_Test()
        {
            var seriesCollectionRaw = await _seriesServiceProxy.Search("Scrubs", TheTvDbLanguage.English);
            Assert.IsNotNull(seriesCollectionRaw);
        }

        [TestMethod]
        public async Task Retrieve_Series_Scrubs_Test()
        {
            var seriesRaw = await _seriesServiceProxy.Retrieve(76156, TheTvDbLanguage.English);
            Assert.IsNotNull(seriesRaw);
        }

        [TestMethod]
        public async Task RetrieveFull_Series_Scrubs_Test()
        {
            var seriesRaw = await _seriesServiceProxy.RetrieveFull(76156, TheTvDbLanguage.English);
            Assert.IsNotNull(seriesRaw);
        }
    }
}
