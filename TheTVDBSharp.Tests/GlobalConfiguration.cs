using System;

namespace TheTVDBSharp.Tests
{
    public static partial class GlobalConfiguration
    {
        public static readonly string ApiKey = "";
        public static readonly string BaseUrl = "http://thetvdb.com";

        public static ITheTvDbApiConfiguration ApiConfiguration
        {
            get
            {
                return new TheTvDbApiConfiguration(ApiKey, BaseUrl);
            }
        }

        public static ITheTvDbClient Manager
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ApiKey)) throw new Exception("API Key is required for samples to work");

                // Create new TheTVDB manager which allows to perform api calls. Enter your api key here.
                // If the api key is not valid the server returns a 404 (.... crap ....) so I was not able
                // to create a unique exception for that case. TheTVDB triggers 404 also in many other cases.

                return new TheTvDbClient(ApiKey);
            }
        }
    }
}
