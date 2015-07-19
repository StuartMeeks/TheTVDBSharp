using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Parsers;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class SeriesParseServiceTest
    {
        private readonly ITheTvDbSeriesParser _seriesParseService;

        public SeriesParseServiceTest()
        {
            var actorParseService = new TheTvDbActorParser();
            var bannerParseService = new TheTvDbBannerParser();
            var episodeParseService = new TheTvDbEpisodeParser();

            _seriesParseService = new TheTvDbSeriesParser(actorParseService,
                bannerParseService,
                episodeParseService); 
        }

        [TestMethod]
        public async Task Parse_Series_76156_Test()
        {
            var sampleSeriesRaw = await SampleDataHelper.GetTextAsync(SampleDataHelper.SampleData.Series76156);
            var series = _seriesParseService.Parse(sampleSeriesRaw);

            Assert.IsNotNull(series);
            Assert.AreEqual((uint)76156, series.SeriesId);
            Assert.AreEqual(TheTvDbFrequency.Wednesday, series.AirDay);
            Assert.AreEqual(194, series.Episodes.Count);
            Assert.AreEqual(1, series.Genres.Count);
            Assert.AreEqual(new TimeSpan(20, 0, 0), series.AirTime);
        }

        [TestMethod]
        public async Task Parse_Search_Scrubs_Test()
        {
            var sampleSeriesCollectionRaw = await SampleDataHelper.GetTextAsync(SampleDataHelper.SampleData.SearchScrubs);
            var seriesCollection = _seriesParseService.ParseSearch(sampleSeriesCollectionRaw);

            Assert.IsNotNull(seriesCollection);
            Assert.AreEqual(2, seriesCollection.Count);
            Assert.AreEqual((uint)76156, seriesCollection.First().SeriesId);
            Assert.AreEqual((uint)167151, seriesCollection.Last().SeriesId);
        }

        [TestMethod]
        public async Task Parse_FullSeries_76156_Test()
        {
            var sampleFullSeriesCompressedStream = await SampleDataHelper.GetStreamAsync(SampleDataHelper.SampleData.SeriesFull76156);
            var series = await _seriesParseService.ParseFull(sampleFullSeriesCompressedStream, TheTvDbLanguage.English);

            Assert.IsNotNull(series);
            Assert.AreEqual((uint)76156, series.SeriesId);
            Assert.AreEqual(TheTvDbFrequency.Wednesday, series.AirDay);
            Assert.AreEqual(194, series.Episodes.Count);
            Assert.AreEqual(1, series.Genres.Count);
            Assert.AreEqual(18, series.Actors.Count);
            Assert.AreEqual(138, series.Banners.Count);
            Assert.AreEqual(new TimeSpan(20, 0, 0), series.AirTime);
        }

        [TestMethod]
        public async Task Parse_RemoveYearIfInTitle_Test()
        {
            var sampleSeriesRaw = await SampleDataHelper.GetTextAsync(SampleDataHelper.SampleData.Series76156);
            var series = _seriesParseService.Parse(sampleSeriesRaw);

            Assert.AreEqual("Scrubs", series.SeriesName);
        }
    }
}
