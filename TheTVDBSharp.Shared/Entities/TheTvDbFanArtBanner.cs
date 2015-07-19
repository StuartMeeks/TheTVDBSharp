using System.Collections.Generic;

namespace TheTVDBSharp.Entities
{
    public sealed class TheTvDbFanArtBanner : TheTvDbBanner
    {
        public List<TheTvDbColor> Colors { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public string RemoteThumbnailPath { get; set; }

        public string RemoteVignettePath { get; set; }

        /// <summary>
        ///     Private parameter-less constructor to facilitate serialization
        /// </summary>
        private TheTvDbFanArtBanner() { }

        public TheTvDbFanArtBanner(uint bannerId)
            : base(bannerId)
        {
        }
    }
}
