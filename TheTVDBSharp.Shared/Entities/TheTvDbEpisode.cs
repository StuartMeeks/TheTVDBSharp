using System;
using System.Collections.Generic;

namespace TheTVDBSharp.Entities
{
    /// <summary>
    ///     Entity describing an episode of a <see cref="TheTvDbSeries" />show.
    /// </summary>
    public class TheTvDbEpisode : IEquatable<TheTvDbEpisode>
    {
        /// <summary>
        ///     Unique identifier for an episode.
        /// </summary>
        public uint EpisodeId { get; }

        /// <summary>
        ///     This episode's number in the appropriate season.
        /// </summary>
        public int EpisodeNumber { get; set; }

        /// <summary>
        ///     This episode's season EpisodeId.
        /// </summary>
        public uint? SeasonId { get; set; }

        /// <summary>
        ///     This episode's season number.
        /// </summary>
        public uint? SeasonNumber { get; set; }

        /// <summary>
        ///     This episode's series SeriesId
        /// </summary>
        public uint? SeriesId { get; set; }

        /// <summary>
        ///     Main language spoken in the episode.
        /// </summary>
        public TheTvDbLanguage? Language { get; set; }

        /// <summary>
        ///     This episode's title.
        /// </summary>
        public string EpisodeName { get; set; }

        /// <summary>
        ///     A short description of the episode.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The date of the first time this episode has aired.
        /// </summary>
        public DateTime? FirstAired { get; set; }

        /// <summary>
        ///     Average rating as shown on IMDb.
        /// </summary>
        public double? Rating { get; set; }

        /// <summary>
        ///     Amount of votes cast.
        /// </summary>
        public int? RatingCount { get; set; }

        /// <summary>
        ///     Director of the episode.
        /// </summary>
        public List<string> Directors { get; set; }

        /// <summary>
        ///     Writers(s) of the episode.
        /// </summary>
        public List<string> Writers { get; set; }

        /// <summary>
        ///     A list of guest stars.
        /// </summary>
        public List<string> GuestStars { get; set; }

        /// <summary>
        ///     Path of the episode thumbnail
        /// </summary>
        public string ThumbRemotePath { get; set; }

        /// <summary>
        ///     Width dimension of the thumbnail in pixels;
        /// </summary>
        public int? ThumbWidth { get; set; }

        /// <summary>
        ///     Height dimension of the thumbnail in pixels.
        /// </summary>
        public int? ThumbHeight { get; set; }

        /// <summary>
        ///     Timestamp of the last update to this episode.
        /// </summary>
        public DateTime? LastUpdated { get; set; }

        /// <summary>
        ///     Private parameter-less constructor to facilitate serialization
        /// </summary>
        private TheTvDbEpisode() { }

        public TheTvDbEpisode(uint episodeId)
        {
            EpisodeId = episodeId;
        }

        public bool Equals(TheTvDbEpisode other) => other?.EpisodeId == EpisodeId;

        public override bool Equals(object obj) => Equals(obj as TheTvDbEpisode);

        public override int GetHashCode() => EpisodeId.GetHashCode();

        public void UpdateFrom(TheTvDbEpisode other)
        {
            EpisodeNumber = other.EpisodeNumber;
            SeasonId = other.SeasonId;
            SeasonNumber = other.SeasonNumber;
            SeriesId = other.SeriesId;

            Language = other.Language;
            EpisodeName = other.EpisodeName;
            Description = other.Description;

            FirstAired = other.FirstAired;
            Rating = other.Rating;
            RatingCount = other.RatingCount;

            Directors = other.Directors;
            Writers = other.Writers;
            GuestStars = other.GuestStars;

            ThumbRemotePath = other.ThumbRemotePath;
            ThumbWidth = other.ThumbWidth;
            ThumbHeight = other.ThumbHeight;

            LastUpdated = other.LastUpdated;
        }

    }
}