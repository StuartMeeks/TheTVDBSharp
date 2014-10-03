﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace TheTVDBSharp.Testing
{
    [TestClass]
    public class TheTvdbManagerTest
    {
        private readonly ITheTvdbManager _tvdbManager = new TheTvdbManager(GlobalConfiguration.ApiKey);

        [TestMethod]
        public async Task GetFullSeries_Test()
        {
            var series = await _tvdbManager.GetSeries(76156, Models.Language.English);
            Assert.IsNotNull(series);
        }

        [TestMethod]
        public async Task GetUpdates_Test()
        {
            var updateContainer = await _tvdbManager.GetUpdates(Models.Interval.Day);
            Assert.IsNotNull(updateContainer);
        }
    }
}
