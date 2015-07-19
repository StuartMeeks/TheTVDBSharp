using System.Threading.Tasks;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Services
{
    public interface ITheTvDbEpisodeService
    {
        /// <summary>
        ///     Retrieves the episode with the given id and returns the corresponding XML tree.
        /// </summary>
        /// <param name="episodeId">ID of the episode to retrieve</param>
        /// <param name="language">ISO 639-1 language code of the episode</param>
        Task<string> Retrieve(uint episodeId, TheTvDbLanguage language);
    }
}
