using TheTVDBSharp.Entities;
using TheTVDBSharp.Updates;

namespace TheTVDBSharp.Parsers
{
    public interface ITheTvDbUpdateParser
    {
#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        TheTvDbUpdateContainer Parse(System.IO.Stream updateContainerStream, TheTvDbInterval interval);
#elif WINDOWS_UAP
        TheTvDbUpdateContainer Parse(Windows.Storage.Streams.IInputStream updateContainerStream, TheTvDbInterval interval);
#endif

        TheTvDbUpdateContainer ParseUncompressed(string updateContainerRaw);
    }
}
