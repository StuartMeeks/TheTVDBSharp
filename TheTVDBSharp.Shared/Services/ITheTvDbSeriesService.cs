using System.Threading.Tasks;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Services
{
    public interface ITheTvDbSeriesService
    {
        /// <summary>
        ///     Retrieves the complete show with the given id and returns the xml string.
        /// </summary>
        /// <param name="showId">Id of the show you wish to lookup.</param>
        /// <param name="language">ISO 639-1 language code of the episode</param>
#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        Task<System.IO.Stream> RetrieveFull(uint showId, TheTvDbLanguage language);
#elif WINDOWS_UAP
        Task<Windows.Storage.Streams.IInputStream> RetrieveFull(uint showId, TheTvDbLanguage language);
#endif

        /// <summary>
        ///     Retrieves the show with the given id and returns the xml string.
        /// </summary>
        /// <param name="showId">Id of the show you wish to lookup.</param>
        /// <param name="language">ISO 639-1 language code of the episode</param>
        Task<string> Retrieve(uint showId, TheTvDbLanguage language);

        /// <summary>
        ///     Returns the xml string representing a search response for the given parameter.
        /// </summary>
        /// <param name="query">Query to perform the search with. E.g. series title.</param>
        /// <param name="language">ISO 639-1 language code of the episode</param>
        Task<string> Search(string query, TheTvDbLanguage language);
    }
}
