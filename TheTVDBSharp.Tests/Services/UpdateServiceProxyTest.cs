using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Tests.Services
{
    [TestClass]
    public class UpdateServiceProxyTest
    {
        readonly ITheTvDbUpdateService _updateService = new TheTvDbUpdateServiceProxy(GlobalConfiguration.ApiConfiguration);

        [TestMethod]
        public async Task Retrieve_Updates_Day_Test()
        {
            var realUpdateStream = await _updateService.Retrieve(TheTvDbInterval.Day);
            Assert.IsNotNull(realUpdateStream);
        }
    }
}
