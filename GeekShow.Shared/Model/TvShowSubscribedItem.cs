using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                RaisePropertyChanged("Network");
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

                RaisePropertyChanged("AirTime");
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

                RaisePropertyChanged("RunTime");
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

                RaisePropertyChanged("LastEpisodeId");
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

                RaisePropertyChanged("LastEpisodeName");
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

                RaisePropertyChanged("LastEpisodeDate");
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

                RaisePropertyChanged("NextEpisodeId");
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

                RaisePropertyChanged("NextEpisodeName");
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

                RaisePropertyChanged("NextEpisodeDate");
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
