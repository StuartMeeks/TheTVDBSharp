using System.Collections.Generic;
using System.Xml.Linq;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Parsers
{
    public interface ITheTvDbBannerParser
    {
        List<TheTvDbBanner> Parse(string bannersRaw);

        TheTvDbBanner Parse(XElement bannerElement);
    }
}
