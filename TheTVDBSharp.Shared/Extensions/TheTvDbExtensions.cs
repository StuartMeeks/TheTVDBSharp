using System;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Extensions
{
    public static class TheTvDbExtensions
    {

        public static TheTvDbContentRating? ToTheTvDbContentRating(this string rating)
        {
            switch (rating)
            {
                case "TV-14":
                    return TheTvDbContentRating.Tv14;

                case "TV-PG":
                    return TheTvDbContentRating.Tvpg;

                case "TV-Y":
                    return TheTvDbContentRating.Tvy;

                case "TV-Y7":
                    return TheTvDbContentRating.Tvy7;

                case "TV-G":
                    return TheTvDbContentRating.Tvg;

                case "TV-MA":
                    return TheTvDbContentRating.Tvma;

                default:
                    return null;
            }
        }

        public static string ToShortString(this TheTvDbLanguage val)
        {
            switch (val)
            {
                case TheTvDbLanguage.Chinese:
                    return "zh";
                case TheTvDbLanguage.Croatian:
                    return "hr";
                case TheTvDbLanguage.Czech:
                    return "cs";
                case TheTvDbLanguage.Dansk:
                    return "da";
                case TheTvDbLanguage.Deutsch:
                    return "de";
                case TheTvDbLanguage.English:
                    return "en";
                case TheTvDbLanguage.Español:
                    return "es";
                case TheTvDbLanguage.Français:
                    return "fr";
                case TheTvDbLanguage.Greek:
                    return "el";
                case TheTvDbLanguage.Hebrew:
                    return "he";
                case TheTvDbLanguage.Italiano:
                    return "it";
                case TheTvDbLanguage.Japanese:
                    return "ja";
                case TheTvDbLanguage.Korean:
                    return "ko";
                case TheTvDbLanguage.Magyar:
                    return "hu";
                case TheTvDbLanguage.Nederlands:
                    return "nl";
                case TheTvDbLanguage.Norsk:
                    return "no";
                case TheTvDbLanguage.Polski:
                    return "pl";
                case TheTvDbLanguage.Portuguese:
                    return "pt";
                case TheTvDbLanguage.Russian:
                    return "ru";
                case TheTvDbLanguage.Slovenian:
                    return "sl";
                case TheTvDbLanguage.Suomeksi:
                    return "fi";
                case TheTvDbLanguage.Svenska:
                    return "sv";
                case TheTvDbLanguage.Turkish:
                    return "tr";
                default:
                    return null;
            }
        }

        public static int? ToId(this TheTvDbLanguage val)
        {
            switch (val)
            {
                case TheTvDbLanguage.Chinese:
                    return 27;
                case TheTvDbLanguage.Croatian:
                    return 31;
                case TheTvDbLanguage.Czech:
                    return 28;
                case TheTvDbLanguage.Dansk:
                    return 10;
                case TheTvDbLanguage.Deutsch:
                    return 14;
                case TheTvDbLanguage.English:
                    return 7;
                case TheTvDbLanguage.Español:
                    return 16;
                case TheTvDbLanguage.Français:
                    return 17;
                case TheTvDbLanguage.Greek:
                    return 20;
                case TheTvDbLanguage.Hebrew:
                    return 24;
                case TheTvDbLanguage.Italiano:
                    return 15;
                case TheTvDbLanguage.Japanese:
                    return 25;
                case TheTvDbLanguage.Korean:
                    return 32;
                case TheTvDbLanguage.Magyar:
                    return 19;
                case TheTvDbLanguage.Nederlands:
                    return 13;
                case TheTvDbLanguage.Norsk:
                    return 9;
                case TheTvDbLanguage.Polski:
                    return 18;
                case TheTvDbLanguage.Portuguese:
                    return 26;
                case TheTvDbLanguage.Russian:
                    return 22;
                case TheTvDbLanguage.Slovenian:
                    return 30;
                case TheTvDbLanguage.Suomeksi:
                    return 11;
                case TheTvDbLanguage.Svenska:
                    return 8;
                case TheTvDbLanguage.Turkish:
                    return 21;
                default:
                    return null;
            }
        }

        public static TheTvDbLanguage? ToTheTvDbLanguage(this string val)
        {
            switch (val)
            {
                case "zh":
                    return TheTvDbLanguage.Chinese;
                case "hr":
                    return TheTvDbLanguage.Croatian;
                case "cs":
                    return TheTvDbLanguage.Czech;
                case "da":
                    return TheTvDbLanguage.Dansk;
                case "de":
                    return TheTvDbLanguage.Deutsch;
                case "en":
                    return TheTvDbLanguage.English;
                case "es":
                    return TheTvDbLanguage.Español;
                case "fr":
                    return TheTvDbLanguage.Français;
                case "el":
                    return TheTvDbLanguage.Greek;
                case "he":
                    return TheTvDbLanguage.Hebrew;
                case "it":
                    return TheTvDbLanguage.Italiano;
                case "ja":
                    return TheTvDbLanguage.Japanese;
                case "ko":
                    return TheTvDbLanguage.Korean;
                case "hu":
                    return TheTvDbLanguage.Magyar;
                case "nl":
                    return TheTvDbLanguage.Nederlands;
                case "no":
                    return TheTvDbLanguage.Norsk;
                case "pl":
                    return TheTvDbLanguage.Polski;
                case "pt":
                    return TheTvDbLanguage.Portuguese;
                case "ru":
                    return TheTvDbLanguage.Russian;
                case "sl":
                    return TheTvDbLanguage.Slovenian;
                case "fi":
                    return TheTvDbLanguage.Suomeksi;
                case "sv":
                    return TheTvDbLanguage.Svenska;
                case "tr":
                    return TheTvDbLanguage.Turkish;
                default:
                    return null;
            }
        }

        public static string ToApiString(this TheTvDbInterval interval)
        {
            switch (interval)
            {
                case TheTvDbInterval.Day:
                    return "day";

                case TheTvDbInterval.Week:
                    return "week";

                case TheTvDbInterval.Month:
                    return "month";

                case TheTvDbInterval.All:
                    return "all";

                default:
                    throw new ArgumentOutOfRangeException("Unsupported interval enum: " + interval);
            }
        }
    }
}
