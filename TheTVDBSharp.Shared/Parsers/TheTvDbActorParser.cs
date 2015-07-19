using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Extensions;

namespace TheTVDBSharp.Parsers
{
    public class TheTvDbActorParser : ITheTvDbActorParser
    {
        /// <summary>
        /// Parse and actors collection as string and returns null if xml not valid
        /// </summary>
        /// <param name="actorsRaw">Actors xml document</param>
        /// <returns>Returns the parsed actors collection or null if xml is not valid</returns>
        public List<TheTvDbActor> Parse(string actorsRaw)
        {
            if (actorsRaw == null) throw new ArgumentNullException(nameof(actorsRaw));

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(actorsRaw);
            }
            catch (XmlException e)
            {
                throw new TheTvDbParseException("Actors collection string cannot be parsed into a xml document.", e);
            }

            // If Actors element is missing return null
            var actorsXml = doc.Element("Actors");
            if (actorsXml == null) throw new TheTvDbParseException("Error while parsing actors xml document. Xml Element 'Actors' is missing.");

            return actorsXml.Elements("Actor")
                .Select(Parse)
                .Where(actor => actor != null)
                .ToList();
        }

        /// <summary>
        /// Parse an actor xml element and returns null if xml not valid
        /// </summary>
        /// <param name="actorXml">Actor xml element</param>
        /// <returns>Returns parsed actor or null if xml is not valid</returns>
        public TheTvDbActor Parse(XElement actorXml)
        {
            if (actorXml == null) throw new ArgumentNullException(nameof(actorXml));

            // If actor has no id throw ParseException
            var id = actorXml.ElementAsUInt("id");
            if (!id.HasValue) throw new TheTvDbParseException("Error while parsing an actor xml element. EpisodeId is missing.");

            return new TheTvDbActor(id.Value)
            {
                ImageRemotePath = actorXml.ElementAsString("Image"),
                Name = actorXml.ElementAsString("Name"),
                Role = actorXml.ElementAsString("Role"),
                SortOrder = actorXml.ElementAsInt("SortOrder").GetValueOrDefault()
            };
        }
    }
}
