using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Tests
{
    [TestClass]
    public class TheTvdbManagerTest
    {
        private readonly ITheTvDbClient _tvdbManager = GlobalConfiguration.Manager;

        [TestMethod]
        public async Task GetFullSeries_Test()
        {
            var series = await _tvdbManager.GetSeriesAsync(76156, TheTvDbLanguage.English);
            Assert.IsNotNull(series);
        }

        [TestMethod]
        public async Task GetUpdates_Test()
        {
            var updateContainer = await _tvdbManager.GetUpdatesAsync(TheTvDbInterval.Day);
            Assert.IsNotNull(updateContainer);
        }
    }
}
