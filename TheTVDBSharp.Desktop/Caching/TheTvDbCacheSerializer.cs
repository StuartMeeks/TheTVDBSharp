using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TheTVDBSharp.Entities;
using TheTVDBSharp.Extensions;

namespace TheTVDBSharp.Caching
{
    [Serializable]
    [XmlRoot(ElementName = "TheTvDbCache")]
    public class TheTvDbCacheSerializer : ITheTvDbCache
    {

        private readonly FileInfo _cacheFile;

        private List<TheTvDbSeries> _series;
        [XmlArray("Series"), XmlArrayItem("SeriesItem")]
        public List<TheTvDbSeries> Series
        {
            get { return _series; }
            set { _series = value; }
        }

        private DateTime? _lastUpdated;
        [XmlElement("LastUpdated")]
        public DateTime? LastUpdated
        {
            get { return _lastUpdated; }
            set { _lastUpdated = value; }
        }

        [XmlIgnore]
        public bool IsSaved { get; set; }

        [XmlIgnore]
        public bool LoadOk { get; set; }

        [XmlIgnore]
        public string LoadResult { get; set; }

        private TheTvDbCacheSerializer() { }

        private TheTvDbCacheSerializer(FileInfo cacheFile)
        {
            _cacheFile = cacheFile;
        }

        public static TheTvDbCacheSerializer Load(FileInfo cacheFile, FileInfo loadFrom = null)
        {
            try
            {
                var fileToUse = loadFrom ?? cacheFile;

                if (fileToUse.Exists)
                {
                    var theTvDbCache = Deserialize(fileToUse);

                    theTvDbCache.IsSaved = true;
                    theTvDbCache.LoadOk = true;
                    theTvDbCache.LoadResult = string.Empty;

                    return theTvDbCache;
                }

                var cache = new TheTvDbCacheSerializer(cacheFile)
                {
                    LoadOk = true,
                    LoadResult = string.Empty,
                    IsSaved = false,
                    LastUpdated = null,
                    Series = new List<TheTvDbSeries>()
                };

                cache.Save(0);

                return cache;
            }
            catch (Exception e)
            {
                return new TheTvDbCacheSerializer
                {
                    LoadOk = false,
                    LoadResult = e.Message
                };
            }
        }

        public void Save(int backupCount)
        {
            RotateCacheBackups(backupCount);

            Serialize(this, _cacheFile);

            IsSaved = true;
        }

        public void Clear(int backupCount)
        {
            _series.Clear();
            _lastUpdated = null;

            Save(backupCount);
        }

        public TheTvDbInterval GetUpdateInterval()
        {
            return !_lastUpdated.HasValue
                ? TheTvDbInterval.All
                : DateTime.Now.Subtract(_lastUpdated.Value).ToTheTvDbInterval();
        }

        public void UpdateSeries(uint seriesId, TheTvDbSeries series)
        {
            if (series == null)
            {
                if (_series.Any(p => p.SeriesId == seriesId))
                {
                    // Remove the series from the cache
                    _series.Remove(_series.First(p => p.SeriesId == seriesId));
                }
                else
                {
                    throw new TheTvDbCacheUpdateException("Series ID to remove was not found in the cache.");
                }
            }
            else
            {
                if (seriesId == series.SeriesId)
                {
                    if (_series.Any(p => p.SeriesId == seriesId))
                    {
                        // the series is already in the cache, so edit the existing entry
                        _series.First(p => p.SeriesId == seriesId).UpdateFrom(series);
                    }
                    else
                    {
                        // the series is not in the cache, add it
                        _series.Add(series);
                    }
                }
                else
                {
                    throw new TheTvDbCacheUpdateException("Series IDs do not match");
                }
            }

            IsSaved = false;
        }

        public void UpdateEpisode(uint seriesId, uint episodeId, TheTvDbEpisode episode)
        {
            if (episode == null)
            {
                if (_series.Any(p => p.SeriesId == seriesId) &&
                    _series.First(p => p.SeriesId == seriesId).Episodes.Any(p => p.EpisodeId == episodeId))
                {
                    // Remove the series from the cache
                    _series.First(p => p.SeriesId == seriesId)
                        .Episodes.Remove(
                            _series.First(p => p.SeriesId == seriesId).Episodes.First(p => p.EpisodeId == episodeId));
                }
                else
                {
                    throw new TheTvDbCacheUpdateException("Series ID to remove was not found in the cache.");
                }
            }
            else
            {
                if (episodeId == episode.EpisodeId && seriesId == episode.SeriesId)
                {
                    if (_series.Any(p => p.SeriesId == seriesId) &&
                        _series.First(p => p.SeriesId == seriesId).Episodes.Any(p => p.EpisodeId == episodeId))
                    {
                        // the series is already in the cache, so edit the existing entry
                        _series.First(p => p.SeriesId == seriesId)
                            .Episodes.First(p => p.EpisodeId == episodeId)
                            .UpdateFrom(episode);
                    }
                    else
                    {
                        // the series is not in the cache, add it
                        _series.First(p => p.SeriesId == seriesId).Episodes.Add(episode);
                    }
                }
                else
                {
                    throw new TheTvDbCacheUpdateException("Series IDs do not match");
                }
            }

            IsSaved = false;
        }

        private void RotateCacheBackups(int backupCount)
        {
            if (_cacheFile.Exists)
            {
                var lastRotateHours = 999.9;
                if (File.Exists(_cacheFile.FullName + ".0"))
                {
                    // Find out when the last rotation was and only rotate if it has been at least a day.
                    var lastRotateTime = File.GetLastWriteTime(_cacheFile.FullName + ".0");
                    lastRotateHours = DateTime.Now.Subtract(lastRotateTime).TotalHours;
                }
                if (lastRotateHours >= 24.0)
                {
                    for (var i = backupCount; i >= 0; i--)
                    {
                        var beforeFilename = _cacheFile.FullName + "." + i;
                        if (File.Exists(beforeFilename))
                        {
                            var afterFileName = _cacheFile.FullName + "." + (i + 1);
                            if (File.Exists(afterFileName))
                            {
                                File.Delete(afterFileName);
                            }
                            File.Move(beforeFilename, afterFileName);
                        }
                    }

                    File.Copy(_cacheFile.FullName, _cacheFile.FullName + ".0");
                }
            }
        }

        private static void Serialize(TheTvDbCacheSerializer theTvDbCache, FileInfo file)
        {
            var serializer = new XmlSerializer(typeof(TheTvDbCacheSerializer));
            using (var fileStream = new FileStream(file.FullName, FileMode.Create, FileAccess.ReadWrite))
            {
                serializer.Serialize(fileStream, theTvDbCache);
            }
        }

        private static TheTvDbCacheSerializer Deserialize(FileInfo file)
        {
            TheTvDbCacheSerializer theTvDbCache;

            var serializer = new XmlSerializer(typeof(TheTvDbCacheSerializer));
            using (var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            {
                theTvDbCache = (TheTvDbCacheSerializer)serializer.Deserialize(fileStream);
            }

            return theTvDbCache;
        }

    }
}
