﻿using System;
using TheTVDBSharp.Services;

namespace TheTVDBSharp.Tests
{
    public static partial class GlobalConfiguration
    {
        public static readonly string ApiKey = "";
        public static readonly string BaseUrl = "http://thetvdb.com";

        public static ProxyConfiguration ApiConfiguration
        {
            get
            {
                return new ProxyConfiguration(ApiKey, BaseUrl);
            }
        }

        public static ITheTvdbManager Manager
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ApiKey)) throw new Exception("API Key is required for samples to work");

                // Create new TheTVDB manager which allows to perform api calls. Enter your api key here.
                // If the api key is not valid the server returns a 404 (.... crap ....) so I was not able
                // to create a unique exception for that case. TheTVDB triggers 404 also in many other cases.

                return new TheTvdbManager(ApiKey);
            }
        }
    }
}
