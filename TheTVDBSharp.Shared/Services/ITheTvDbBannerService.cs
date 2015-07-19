using System.Threading.Tasks;

namespace TheTVDBSharp.Services
{
    public interface ITheTvDbBannerService
    {
#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        Task<System.IO.Stream> Retrieve(string remotePath);
#elif WINDOWS_UAP
        Task<Windows.Storage.Streams.IInputStream> Retrieve(string remotePath);
#endif
    }
}
