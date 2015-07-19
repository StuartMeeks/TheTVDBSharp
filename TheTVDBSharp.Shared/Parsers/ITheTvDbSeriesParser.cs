using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Parsers
{
    public interface ITheTvDbSeriesParser
    {
        TheTvDbSeries Parse(string seriesRaw);

        TheTvDbSeries Parse(XElement seriesElement, bool isSearchElement = false);

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        Task<TheTvDbSeries> ParseFull(System.IO.Stream seriesStream, TheTvDbLanguage language);
#elif WINDOWS_UAP
        Task<TheTvDbSeries> ParseFull(Windows.Storage.Streams.IInputStream seriesStream, TheTvDbLanguage language);
#endif

        List<TheTvDbSeries> ParseSearch(string seriesSearchRaw);
    }
}
