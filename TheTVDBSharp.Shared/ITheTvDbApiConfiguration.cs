namespace TheTVDBSharp
{
    public interface ITheTvDbApiConfiguration
    {
        /// <summary>
        ///     The API key provided by TVDB.
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// The API URL of TVDB.
        /// </summary>
        string BaseUrl { get; }

#if WINDOWS_PORTABLE || WINDOWS_DESKTOP
        System.TimeSpan? Timeout { get; set; }
#endif
    }
}
