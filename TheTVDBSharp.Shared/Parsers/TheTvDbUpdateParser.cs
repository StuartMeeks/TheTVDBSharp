using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TheTVDBSharp.Extensions;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Updates;

namespace TheTVDBSharp.Parsers
{
    public class TheTvDbUpdateParser : ITheTvDbUpdateParser
    {
#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        public TheTvDbUpdateContainer Parse(System.IO.Stream updateContainerStream, TheTvDbInterval interval)
#elif WINDOWS_UAP
        public TheTvDbUpdateContainer Parse(Windows.Storage.Streams.IInputStream updateContainerStream, TheTvDbInterval interval)
#endif
        {
            if (updateContainerStream == null) throw new ArgumentNullException(nameof(updateContainerStream));

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
            using (var archive = new ZipArchive(updateContainerStream, ZipArchiveMode.Read))
#elif WINDOWS_UAP
            using (var archive = new ZipArchive(updateContainerStream.AsStreamForRead(), ZipArchiveMode.Read))
#endif
            {
                var entryName = $"updates_{interval.ToApiString()}.xml";
                var updateContainerRaw = archive.GetEntry(entryName).ReadToEnd();
                return ParseUncompressed(updateContainerRaw);
            }
        }

        public TheTvDbUpdateContainer ParseUncompressed(string updateContainerRaw)
        {
            if (updateContainerRaw == null) throw new ArgumentNullException(nameof(updateContainerRaw));

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(updateContainerRaw);
            }
            catch (XmlException e)
            {
                throw new TheTvDbParseException("Search series collection string cannot be parsed into a xml document.", e);
            }

            var updateContainerXml = doc.Element("Data");
            if (updateContainerXml == null) throw new TheTvDbParseException("Error while parsing update xml document. Xml Element 'Data' is missing.");

            var updateContainer = new TheTvDbUpdateContainer();

            uint lastUpdatedEpoch;
            var lastUpdatedRaw = updateContainerXml.Attribute("time").Value;
            if (uint.TryParse(lastUpdatedRaw, out lastUpdatedEpoch))
            {
                updateContainer.LastUpdated = lastUpdatedEpoch.ToDateTime();
            }

            updateContainer.SeriesUpdates = updateContainerXml.Elements("Series")
                .Select(ParseSeriesUpdate)
                .ToList();

            updateContainer.EpisodeUpdates = updateContainerXml.Elements("Episode")
                .Select(ParseEpisodeUpdate)
                .ToList();

            updateContainer.BannerUpdates = updateContainerXml.Elements("Banner")
                .Select(ParseBannerUpdate)
                .ToList();

            return updateContainer;
        }

        private static TheTvDbSeriesUpdate ParseSeriesUpdate(XElement seriesUpdateXml)
        {
            if (seriesUpdateXml == null) throw new ArgumentNullException(nameof(seriesUpdateXml));

            return new TheTvDbSeriesUpdate
            {
                SeriesId = seriesUpdateXml.ElementAsUInt("id").GetValueOrDefault(),
                LastUpdated = seriesUpdateXml.ElementFromEpochToDateTime("time").GetValueOrDefault()
            };
        }

        private static TheTvDbEpisodeUpdate ParseEpisodeUpdate(XElement episodeUpdateXml)
        {
            if (episodeUpdateXml == null) throw new ArgumentNullException(nameof(episodeUpdateXml));

            return new TheTvDbEpisodeUpdate
            {
                EpisodeId = episodeUpdateXml.ElementAsUInt("id").GetValueOrDefault(),
                SeriesId = episodeUpdateXml.ElementAsUInt("Series").GetValueOrDefault(),
                LastUpdated = episodeUpdateXml.ElementFromEpochToDateTime("time").GetValueOrDefault()
            };
        }

        private static TheTvDbBannerUpdate ParseBannerUpdate(XElement bannerUpdateXml)
        {
            if (bannerUpdateXml == null) throw new ArgumentNullException(nameof(bannerUpdateXml));

            return new TheTvDbBannerUpdate
            {
                SeriesId = bannerUpdateXml.ElementAsUInt("Series").GetValueOrDefault(),
                RemotePath = bannerUpdateXml.ElementAsString("path"),
                SeasonNumber = bannerUpdateXml.ElementAsUInt("SeasonNum"),
                Language = bannerUpdateXml.ElementAsString("language").ToTheTvDbLanguage(),
                LastUpdated = bannerUpdateXml.ElementFromEpochToDateTime("time").GetValueOrDefault()
            };
        }

    }
}
