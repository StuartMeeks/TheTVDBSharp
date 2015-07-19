namespace TheTVDBSharp.Entities
{
    public sealed class TheTvDbSeriesBanner : TheTvDbBanner
    {
        public TheTvDbSeriesBannerType? BannerType { get; set; }

        /// <summary>
        ///     Private parameter-less constructor to facilitate serialization
        /// </summary>
        private TheTvDbSeriesBanner() { }

        public TheTvDbSeriesBanner(uint bannerId)
            : base(bannerId)
        {
        }
    }
}