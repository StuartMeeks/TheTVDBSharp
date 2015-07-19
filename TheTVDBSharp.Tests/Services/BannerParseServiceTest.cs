using System.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Parsers;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class BannerParseServiceTest
    {
        private readonly ITheTvDbBannerParser _bannerParseService = new TheTvDbBannerParser();

        [TestMethod]
        public async Task Parse_Banners_76156_Test()
        {
            var bannerCollectionRaw = await SampleDataHelper.GetTextAsync(SampleDataHelper.SampleData.SeriesFull76156Banners);
            var banners = _bannerParseService.Parse(bannerCollectionRaw);

            Assert.IsNotNull(banners);
            Assert.AreEqual(140, banners.Count);
            Assert.AreEqual((uint)23585, banners.First().BannerId);
            Assert.AreEqual((byte)226, (banners.First() as TheTvDbFanArtBanner).Colors.First().G);
        }
    }
}
