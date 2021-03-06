﻿namespace TheTVDBSharp.Entities
{
    /// <summary>
    ///     Different content ratings. View <c>http://en.wikipedia.org/wiki/TV_Parental_Guidelines</c> for more info.
    /// </summary>
    public enum TheTvDbContentRating
    {
        /// <summary>
        ///     Not suitable for children under 14.
        /// </summary>
        Tv14,

        /// <summary>
        ///     This program contains material that parents may find unsuitable for younger children.
        /// </summary>
        Tvpg,

        /// <summary>
        ///     This program is designed to be appropriate for all children.
        /// </summary>
        Tvy,

        /// <summary>
        ///     This program is designed for children age 7 and above.
        /// </summary>
        Tvy7,

        /// <summary>
        ///     Most parents would find this program suitable for all ages.
        /// </summary>
        Tvg,

        /// <summary>
        ///     This program is specifically designed to be viewed by adults and therefore may be unsuitable for children under 17.
        /// </summary>
        Tvma
    }

}