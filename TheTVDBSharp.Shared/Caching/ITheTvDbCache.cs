using System;
using System.Collections.Generic;
using TheTVDBSharp.Entities;

namespace TheTVDBSharp.Caching
{
    public interface ITheTvDbCache
    {

        DateTime? LastUpdated { get; set; }

        bool LoadOk { get; set; }

        string LoadResult { get; set; }

        List<TheTvDbSeries> Series { get; set; }

        bool IsSaved { get; set; }

        void Save(int backupCount);

        void Clear(int backupCount);

        TheTvDbInterval GetUpdateInterval();

        void UpdateSeries(uint seriesId, TheTvDbSeries series);

        void UpdateEpisode(uint seriesId, uint episodeId, TheTvDbEpisode episode);

    }
}
