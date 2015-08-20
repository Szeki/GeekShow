using System;

namespace GeekShow.Shared.Model
{
    public class TvShowSubscribedItem : TvShowItem
    {
        #region Members

        string _network;
        string _airTime;
        int _runTime;
        string _lastEpisodeId;
        string _lastEpisodeName;
        DateTime? _lastEpisodeDate;
        string _nextEpisodeId;
        string _nextEpisodeName;
        DateTimeOffset? _nextEpisodeDate;

        #endregion

        #region Constructor

        public TvShowSubscribedItem(int showId, string name)
            : base(showId, name)
        {
        }

        #endregion

        #region Properties

        public string Network
        {
            get
            {
                return _network;
            }
            set
            {
                if (_network == value) return;

                _network = value;

                RaisePropertyChanged(nameof(Network));
            }
        }

        public string AirTime
        {
            get
            {
                return _airTime;
            }
            set
            {
                if (_airTime == value) return;

                _airTime = value;

                RaisePropertyChanged(nameof(AirTime));
            }
        }

        public int RunTime
        {
            get
            {
                return _runTime;
            }
            set
            {
                if (_runTime == value) return;

                _runTime = value;

                RaisePropertyChanged(nameof(RunTime));
            }
        }

        public string LastEpisodeId
        {
            get
            {
                return _lastEpisodeId;
            }
            set
            {
                if (_lastEpisodeId == value) return;

                _lastEpisodeId = value;

                RaisePropertyChanged(nameof(LastEpisodeId));
            }
        }

        public string LastEpisodeName
        {
            get
            {
                return _lastEpisodeName;
            }
            set
            {
                if (_lastEpisodeName == value) return;

                _lastEpisodeName = value;

                RaisePropertyChanged(nameof(LastEpisodeName));
            }
        }

        public DateTime? LastEpisodeDate
        {
            get
            {
                return _lastEpisodeDate;
            }
            set
            {
                if (_lastEpisodeDate == value) return;

                _lastEpisodeDate = value;

                RaisePropertyChanged(nameof(LastEpisodeDate));
            }
        }

        public string NextEpisodeId
        {
            get
            {
                return _nextEpisodeId;
            }
            set
            {
                if (_nextEpisodeId == value) return;

                _nextEpisodeId = value;

                RaisePropertyChanged(nameof(NextEpisodeId));
            }
        }

        public string NextEpisodeName
        {
            get
            {
                return _nextEpisodeName;
            }
            set
            {
                if (_nextEpisodeName == value) return;

                _nextEpisodeName = value;

                RaisePropertyChanged(nameof(NextEpisodeName));
            }
        }

        public DateTimeOffset? NextEpisodeDate
        {
            get
            {
                return _nextEpisodeDate;
            }
            set
            {
                if (_nextEpisodeDate == value) return;

                _nextEpisodeDate = value;

                RaisePropertyChanged(nameof(NextEpisodeDate));
            }
        }

        #endregion

        #region Static Methods

        public static TvShowSubscribedItem CloneBaseItem(TvShowItem item)
        {
            if(item == null)
            {
                return null;
            }

            return new TvShowSubscribedItem(item.ShowId, item.Name)
            {
                Country = item.Country,
                Link = item.Link,
                Started = item.Started,
                Ended = item.Ended,
                Status = item.Status,
                Classification = item.Classification,
                Seasons = item.Seasons
            };
        }

        #endregion
    }
}
