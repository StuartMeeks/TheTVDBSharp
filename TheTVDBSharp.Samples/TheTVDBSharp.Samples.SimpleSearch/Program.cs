﻿using System;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Samples.SimpleSearch
{
    class Program
    {
        static void Main()
        {
            var tvdb = GlobalConfiguration.Client;

            while (true)
            {
                Console.Write("Enter a series name: ");
                var searchQuery = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("| Searching the entire TheTVDB database |");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine();

                // Search for a series by name and with a specified language.
                var searchResult = tvdb.SearchSeriesAsync(searchQuery, TheTvDbLanguage.English).GetAwaiter().GetResult();

                Console.WriteLine("{0} shows found... Downloading each show", searchResult.Count);
                Console.WriteLine();

                // Search call returns a readonly collection which can be enumerated.
                foreach (var series in searchResult)
                {
                    // To get more details of a series (not just metadata) like all episodes or banners 
                    // or actors call GetSeries(seriesId, language)
                    var fullSeries = tvdb.GetSeriesAsync(series.SeriesId, TheTvDbLanguage.English).GetAwaiter().GetResult();

                    Console.WriteLine("- {0} ({1} Episodes)", series.SeriesName, fullSeries.Episodes.Count);
                }

                Console.WriteLine();
                Console.Write("<<Press Enter to perform a new search>>");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
