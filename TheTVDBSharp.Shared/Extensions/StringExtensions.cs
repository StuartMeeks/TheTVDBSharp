using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace TheTVDBSharp.Extensions
{
    public static class StringExtensions
    {
        public static XDocument ToXDocument(this string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return null;

            try
            {
                return XDocument.Parse(raw);
            }
            catch (XmlException)
            {
                return null;
            }
        }

        public static List<string> SplitByPipe(this string raw)
        {
            return string.IsNullOrWhiteSpace(raw) 
                ? null 
                : raw.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
