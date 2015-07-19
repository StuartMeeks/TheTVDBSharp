namespace TheTVDBSharp.Entities
{
    public sealed class TheTvDbSeasonBanner : TheTvDbBanner
    {
        public int? Season { get; set; }

        public bool? IsWide { get; set; }

        /// <summary>
        ///     Private parameter-less constructor to facilitate serialization
        /// </summary>
        private TheTvDbSeasonBanner() { }

        public TheTvDbSeasonBanner(uint bannerId)
            : base(bannerId)
        {
        }
    }
}
