using System.Threading.Tasks;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Services
{
    public interface ITheTvDbUpdateService
    {
#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        Task<System.IO.Stream> Retrieve(TheTvDbInterval interval);
#elif WINDOWS_UAP
        Task<Windows.Storage.Streams.IInputStream> Retrieve(TheTvDbInterval interval);
#endif

        Task<string> RetrieveUncompressed(TheTvDbInterval interval);
    }
}
