using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Threading.Tasks;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Parsers;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class UpdateParseServiceTest
    {
        private readonly ITheTvDbUpdateParser _updateParseService = new TheTvDbUpdateParser();

        [TestMethod]
        public async Task Parse_Update_Day_Test()
        {
            var updateContainerStream = await SampleDataHelper.GetStreamAsync(SampleDataHelper.SampleData.UpdatesDay);
            var updateContainer = _updateParseService.Parse(updateContainerStream, TheTvDbInterval.Day);

            Assert.IsNotNull(updateContainer);
            Assert.AreEqual(141, updateContainer.BannerUpdates.Count);
            Assert.AreEqual(3468, updateContainer.EpisodeUpdates.Count);
            Assert.AreEqual(591, updateContainer.SeriesUpdates.Count);
            Assert.AreEqual(new DateTime(2014, 9, 9, 17, 30, 1), updateContainer.LastUpdated);
        }
    }
}
