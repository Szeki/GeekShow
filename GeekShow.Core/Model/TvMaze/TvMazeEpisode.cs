using GeekShow.Core.Service;
using Newtonsoft.Json;
using System;

namespace GeekShow.Core.Model.TvMaze
{
    public class TvMazeEpisode
    {
        public string Id
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        [JsonProperty("name")]
        public string EpisodeName
        {
            get;
            set;
        }

        public int Season
        {
            get;
            set;
        }

        [JsonProperty("number")]
        public int EpisodeNumber
        {
            get;
            set;
        }

        public string EpisodeId => $"{Season}x{EpisodeNumber.ToString("D2")}";

        public DateTime AirDate
        {
            get;
            set;
        }

        public string AirTime
        {
            get;
            set;
        }
        
        public string AirStamp
        {
            get;
            set;
        }

        public int Runtime
        {
            get;
            set;
        }

        public string Summary
        {
            get;
            set;
        }

        [JsonIgnore]
        public string NormalizedSummary => TvMazeServiceHelper.NormalizeHtmlText(Summary);

        [JsonProperty("_links")]
        public TvMazeLinks Links
        {
            get;
            set;
        }
    }
}
