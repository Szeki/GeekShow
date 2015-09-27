using System;

namespace GeekShow.Shared.Model
{
    public class TvShowQuickInfoItem
    {
        #region Properties

        public string ShowId
        {
            get;
            set;
        }

        public string ShowName
        {
            get;
            set;
        }

        public string ShowUrl
        {
            get;
            set;
        }

        public int Premiered
        {
            get;
            set;
        }

        public DateTime Started
        {
            get;
            set;
        }

        public DateTime? Ended
        {
            get;
            set;
        }


        public string LastEpisodeId
        {
            get;
            set;
        }

        public string LastEpisodeName
        {
            get;
            set;
        }

        public DateTime? LastEpisodeDate
        {
            get;
            set;
        }

        public string NextEpisodeId
        {
            get;
            set;
        }

        public string NextEpisodeName
        {
            get;
            set;
        }

        public DateTimeOffset? NextEpisodeDate
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string Classification
        {
            get;
            set;
        }

        public string Genres
        {
            get;
            set;
        }

        public string Network
        {
            get;
            set;
        }

        public string AirTime
        {
            get;
            set;
        }

        public string Runtime
        {
            get;
            set;
        }

        public string Plot
        {
            get;
            set;
        }

        #endregion
    }
}
