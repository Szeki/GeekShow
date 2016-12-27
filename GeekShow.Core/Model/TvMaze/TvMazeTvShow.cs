using GeekShow.Core.Component;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GeekShow.Core.Model.TvMaze
{
    public class TvMazeTvShow : NotifyPropertyChanged
    {
        private TvMazeItem tvShow;

        private TvMazeEpisode previousEpisode;
        private TvMazeEpisode nextEpisode;

        private List<TvMazeEpisode> episodes;
        
        public TvMazeItem TvShow
        {
            get
            {
                return tvShow;
            }
            set
            {
                if (tvShow == value) return;

                tvShow = value;

                RaisePropertyChanged(nameof(TvShow));
            }
        }

        public List<TvMazeEpisode> Episodes
        {
            get
            {
                if(episodes == null)
                {
                    episodes = new List<TvMazeEpisode>();
                }

                return episodes;
            }
            set
            {
                if(value == null)
                {
                    episodes = new List<TvMazeEpisode>();
                }
                else
                {
                    episodes = value;
                }
            }
        }

        [JsonIgnore]
        public int Id => TvShow == null ? 0 : TvShow.Id;

        [JsonIgnore]
        public long Updated => TvShow == null ? 0 : TvShow.Updated;

        public TvMazeEpisode PreviousEpisode
        {
            get
            {
                return previousEpisode;
            }
            set
            {
                if (previousEpisode == value) return;

                previousEpisode = value;

                RaisePropertyChanged(nameof(PreviousEpisode));
            }
        }

        public TvMazeEpisode NextEpisode
        {
            get
            {
                return nextEpisode;
            }
            set
            {
                if (nextEpisode == value) return;

                nextEpisode = value;

                RaisePropertyChanged(nameof(NextEpisode));
            }
        }
    }
}
