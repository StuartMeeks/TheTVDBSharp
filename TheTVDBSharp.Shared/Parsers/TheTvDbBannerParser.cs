using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TheTVDBSharp.Extensions;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Parsers
{
    public class TheTvDbBannerParser : ITheTvDbBannerParser
    {
        /// <summary>
        /// Parse banner collection as string and returns null if xml is not valid
        /// </summary>
        /// <param name="bannersRaw">Banner collection xml document as string</param>
        /// <returns></returns>
        public List<TheTvDbBanner> Parse(string bannersRaw)
        {
            if (bannersRaw == null) throw new ArgumentNullException(nameof(bannersRaw));

            // If xml cannot be created return null
            XDocument doc;
            try
            {
                doc = XDocument.Parse(bannersRaw);
            }
            catch (XmlException ex)
            {
                throw new TheTvDbParseException("Banners collection string cannot be parsed into a xml document.", ex);
            }

            // If Banners element is missing return null
            var bannersXml = doc.Element("Banners");
            if (bannersXml == null) throw new TheTvDbParseException("Error while parsing banners xml document. Xml element 'Banners' is missing.");

            return bannersXml.Elements("Banner")
                .Select(Parse)
                .Where(banner => banner != null)
                .ToList();
        }

        /// <summary>
        /// Parse banner xml element and returns null if xml is not valid
        /// </summary>
        /// <param name="bannerXml">Banner xml element</param>
        /// <returns>Return the created banner or null if xml is not valid</returns>
        public TheTvDbBanner Parse(XElement bannerXml)
        {
            if (bannerXml == null) throw new ArgumentNullException(nameof(bannerXml));

            TheTvDbBanner banner;

            // If banner has no id return null
            var id = bannerXml.ElementAsUInt("id");
            if (!id.HasValue) throw new TheTvDbParseException("Error while parsing a banner xml element. Banner id is missing.");

            var bannerType = bannerXml.ElementAsString("BannerType");
            switch (bannerType)
            {
                case "fanart":
                    banner = CreateFanart(bannerXml, id.Value);
                    break;
                case "poster":
                    banner = CreatePoster(bannerXml, id.Value);
                    break;
                case "season":
                    banner = CreateSeason(bannerXml, id.Value);
                    break;
                case "series":
                    banner = CreateSeries(bannerXml, id.Value);
                    break;
                default:
                    throw new TheTvDbParseException(
                        $"Error while parsing a banner xml element. BannerType '{bannerType}' is unknown.");
            }

            banner.Language = bannerXml.ElementAsString("Language").ToTheTvDbLanguage();
            banner.Rating = bannerXml.ElementAsDouble("Rating");
            banner.RatingCount = bannerXml.ElementAsInt("RatingCount");
            banner.RemotePath = bannerXml.ElementAsString("BannerPath");

            return banner;
        }

        private static TheTvDbBanner CreateFanart(XElement bannerXml, uint id)
        {
            if (bannerXml == null) throw new ArgumentNullException(nameof(bannerXml));

            var banner = new TheTvDbFanArtBanner(id);

            var size = ParseSize(bannerXml.ElementAsString("BannerType2"));
            if (size != null)
            {
                banner.Width = size.Item1;
                banner.Height = size.Item2;
            }

            var colorRawCollection = bannerXml.ElementAsString("Colors").SplitByPipe();
            if (colorRawCollection != null)
            {
                banner.Colors = colorRawCollection
                    .Select(c =>
                    {
                        var colorRawParts = c.Split(',');
                        return new TheTvDbColor(
                            byte.Parse(colorRawParts[0]),
                            byte.Parse(colorRawParts[1]),
                            byte.Parse(colorRawParts[2])
                            );
                    })
                    .ToList();
            }

            banner.RemoteThumbnailPath = bannerXml.ElementAsString("ThumbnailPath");
            banner.RemoteVignettePath = bannerXml.ElementAsString("VignettePath");

            return banner;
        }

        private static TheTvDbBanner CreatePoster(XElement bannerXml, uint id)
        {
            if (bannerXml == null) throw new ArgumentNullException(nameof(bannerXml));

            var banner = new TheTvDbPosterBanner(id);

            var size = ParseSize(bannerXml.ElementAsString("BannerType2"));
            if (size == null) return banner;

            banner.Width = size.Item1;
            banner.Height = size.Item2;

            return banner;
        }

        private static TheTvDbBanner CreateSeason(XElement bannerXml, uint id)
        {
            if (bannerXml == null) throw new ArgumentNullException(nameof(bannerXml));

            return new TheTvDbSeasonBanner(id)
            {
                Season = bannerXml.ElementAsInt("Season"),
                IsWide = bannerXml.ElementAsString("BannerType2") == "seasonwide"
            };
        }

        private static TheTvDbBanner CreateSeries(XElement bannerXml, uint id)
        {
            if (bannerXml == null) throw new ArgumentNullException(nameof(bannerXml));

            return new TheTvDbSeriesBanner(id)
            {
                BannerType = bannerXml.ElementAsEnum<TheTvDbSeriesBannerType>("BannerType2")
            };
        }

        private static Tuple<int, int> ParseSize(string sizeRaw)
        {
            if (string.IsNullOrWhiteSpace(sizeRaw)) return null;

            var splits = sizeRaw.Split('x');
            return new Tuple<int, int>(int.Parse(splits[0]), int.Parse(splits[1]));
        }
    }
}
