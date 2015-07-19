
namespace TheTVDBSharp.Entities
{
    public sealed class TheTvDbPosterBanner : TheTvDbBanner
    {
        public int? Width { get; set; }

        public int? Height { get; set; }

        /// <summary>
        ///     Private parameter-less constructor to facilitate serialization
        /// </summary>
        private TheTvDbPosterBanner() { }

        public TheTvDbPosterBanner(uint bannerId)
            : base(bannerId)
        {
        }
    }
}
