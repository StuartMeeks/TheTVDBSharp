using System.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using TheTVDBSharp.Parsers;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class ActorParseServiceTest
    {
        private readonly ITheTvDbActorParser _actorParseService = new TheTvDbActorParser();

        [TestMethod]
        public async Task Parse_Actors_76156_Test()
        {
            var actorCollectionRaw = await SampleDataHelper.GetTextAsync(SampleDataHelper.SampleData.SeriesFull76156Actors);
            var actors = _actorParseService.Parse(actorCollectionRaw);

            Assert.IsNotNull(actors);
            Assert.AreEqual(18, actors.Count);
            Assert.AreEqual((uint)43640, actors.First().ActorId);
            Assert.AreEqual("Zach Braff", actors.First().Name);
            Assert.AreEqual(0, actors.First().SortOrder);
        }
    }
}
