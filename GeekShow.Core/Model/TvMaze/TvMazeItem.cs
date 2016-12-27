using GeekShow.Core.Component;
using GeekShow.Core.Service;
using Newtonsoft.Json;
using System;

namespace GeekShow.Core.Model.TvMaze
{
    public class TvMazeItem : NotifyPropertyChanged
    {
        #region Members

        private string status;

        #endregion

        #region Properties

        public int Id
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status == value) return;

                status = value;

                RaisePropertyChanged(nameof(Status));
            }
        }

        public string[] Genres
        {
            get;
            set;
        }

        [JsonIgnore]
        public string GenresString => Genres == null ? string.Empty : string.Join(", ", Genres);

        public int Runtime
        {
            get;
            set;
        }

        public DateTime? Premiered
        {
            get;
            set;
        }

        public TvMazeSchedule Schedule
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

        public TvMazeNetwork Network
        {
            get;
            set;
        }

        public long Updated
        {
            get;
            set;
        }

        [JsonProperty("_links")]
        public TvMazeLinks Links
        {
            get;
            set;
        }

        public TvMazeExternals Externals
        {
            get;
            set;
        }

        public TvMazeRating Rating
        {
            get;
            set;
        }

        #endregion
    }
}
