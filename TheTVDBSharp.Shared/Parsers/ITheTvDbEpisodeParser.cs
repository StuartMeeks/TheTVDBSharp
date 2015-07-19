using System.Xml.Linq;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Parsers
{
    public interface ITheTvDbEpisodeParser
    {
        TheTvDbEpisode Parse(string episodeRaw);

        TheTvDbEpisode Parse(XElement episodeElement);
    }
}
