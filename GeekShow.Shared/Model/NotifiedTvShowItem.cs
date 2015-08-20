using System;

namespace GeekShow.Shared.Model
{
    public class NotifiedTvShowItem
    {
        #region Constructor

        public NotifiedTvShowItem(int showId)
        {
            ShowId = showId;
        }

        #endregion

        #region Properties

        public int ShowId
        {
            get;
            private set;
        }

        public string EpisodeId
        {
            get;
            set;
        }

        public DateTimeOffset EpisodeDate
        {
            get;
            set;
        }

        public DateTimeOffset NotifiedAt
        {
            get;
            set;
        }

        #endregion
    }
}
