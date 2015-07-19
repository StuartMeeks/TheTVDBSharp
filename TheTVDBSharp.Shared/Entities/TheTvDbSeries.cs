using System;
using System.Collections.Generic;

namespace TheTVDBSharp.Entities
{
    /// <summary>
    ///     Entity describing a show.
    /// </summary>
    public class TheTvDbSeries : IEquatable<TheTvDbSeries>
    {
        /// <summary>
        ///     Unique identifier used by TVDB and TVDBSharp.
        /// </summary>
        public uint SeriesId { get; }

        /// <summary>
        ///     Main language of the show.
        /// </summary>
        public TheTvDbLanguage? Language { get; set; }

        /// <summary>
        ///     Name of the show.
        /// </summary>
        public string SeriesName { get; set; }

        /// <summary>
        ///     A short overview of the show.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The date the show aired for the first time.
        /// </summary>
        public DateTime? FirstAired { get; set; }

        /// <summary>
        ///     Network that broadcasts the show.
        /// </summary>
        public string Network { get; set; }

        /// <summary>
        ///     Unique identifier used by IMDb.
        /// </summary>
        public string ImdbId { get; set; }

        /// <summary>
        ///     Day of the week when the show airs.
        /// </summary>
        public TheTvDbFrequency? AirDay { get; set; }

        /// <summary>
        ///     Time of the day when the show airs.
        /// </summary>
        public TimeSpan? AirTime { get; set; }

        /// <summary>
        ///     Rating of the content provided by an official organ.
        /// </summary>
        public TheTvDbContentRating? ContentRating { get; set; }

        /// <summary>
        ///     Average rating as shown on IMDb.
        /// </summary>
        public double? Rating { get; set; }

        /// <summary>
        ///     Amount of votes cast.
        /// </summary>
        public int? RatingCount { get; set; }

        /// <summary>
        ///     Current status of the show.
        /// </summary>
        public TheTvDbStatus? Status { get; set; }

        /// <summary>
        ///     List of all actors in the show.
        /// </summary>
        public List<TheTvDbActor> Actors { get; set; }

        /// <summary>
        ///     A list of genres the show is associated with.
        /// </summary>
        public List<string> Genres { get; set; }

        /// <summary>
        ///     Link to the banner image.
        /// </summary>
        public string BannerRemotePath { get; set; }

        /// <summary>
        ///     Link to a fanart image.
        /// </summary>
        public string FanartRemotePath { get; set; }

        /// <summary>
        ///     Let me know if you find out what this is.
        /// </summary>
        public string PosterRemotePath { get; set; }

        /// <summary>
        ///     Timestamp of the latest update.
        /// </summary>
        public DateTime? LastUpdated { get; set; }

        /// <summary>
        ///     Let me know if you find out what this is.
        /// </summary>
        public int? Runtime { get; set; }

        /// <summary>
        ///     No clue
        /// </summary>
        public string Zap2ItId { get; set; }

        /// <summary>
        ///     A list of all episodes associated with this show.
        /// </summary>
        public List<TheTvDbEpisode> Episodes { get; set; }

        /// <summary>
        ///     A list of all banners associated with this show.
        /// </summary>
        public List<TheTvDbBanner> Banners { get; set; }

        /// <summary>
        ///     Private parameter-less constructor to facilitate serialization
        /// </summary>
        private TheTvDbSeries() { }

        public TheTvDbSeries(uint seriesId)
        {
            SeriesId = seriesId;
        }

        public bool Equals(TheTvDbSeries other) => other?.SeriesId == SeriesId;

        public override bool Equals(object obj) => Equals(obj as TheTvDbSeries);

        public override int GetHashCode() => SeriesId.GetHashCode();

        public void UpdateFrom(TheTvDbSeries other)
        {
            Language = other.Language;
            SeriesName = other.SeriesName;
            Description = other.Description;

            FirstAired = other.FirstAired;
            Network = other.Network;
            ImdbId = other.ImdbId;

            AirDay = other.AirDay;
            AirTime = other.AirTime;
            ContentRating = other.ContentRating;
            Rating = other.Rating;
            Status = other.Status;

            Actors = other.Actors;
            Genres = other.Genres;

            BannerRemotePath = other.BannerRemotePath;
            FanartRemotePath = other.FanartRemotePath;
            PosterRemotePath = other.PosterRemotePath;

            LastUpdated = other.LastUpdated;
        }


    }
}