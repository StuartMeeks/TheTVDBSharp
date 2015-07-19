using System.Collections.Generic;
using System.Xml.Linq;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Parsers
{
    public interface ITheTvDbActorParser
    {
        List<TheTvDbActor> Parse(string actorsRaw);

        TheTvDbActor Parse(XElement actorElement);
    }
}
