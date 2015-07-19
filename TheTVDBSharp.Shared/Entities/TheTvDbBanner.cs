using System;
using System.Xml.Serialization;

namespace TheTVDBSharp.Entities
{

    [XmlInclude(typeof(TheTvDbFanArtBanner)),
        XmlInclude(typeof(TheTvDbPosterBanner)),
        XmlInclude(typeof(TheTvDbSeasonBanner)),
        XmlInclude(typeof(TheTvDbSeriesBanner))]
    public class TheTvDbBanner : IEquatable<TheTvDbBanner>
    {
        public uint BannerId { get; }

        public string RemotePath { get; set; }

        public TheTvDbLanguage? Language { get; set; }

        public double? Rating { get; set; }

        public int? RatingCount { get; set; }

        /// <summary>
        ///     Protected parameter-less constructor to facilitate serialization
        /// </summary>
        protected TheTvDbBanner() { }

        public TheTvDbBanner(uint bannerId)
        {
            BannerId = bannerId;
        }

        public bool Equals(TheTvDbBanner other) => other?.BannerId == BannerId;

        public override bool Equals(object obj) => Equals(obj as TheTvDbBanner);

        public override int GetHashCode() => BannerId.GetHashCode();
    }
}
