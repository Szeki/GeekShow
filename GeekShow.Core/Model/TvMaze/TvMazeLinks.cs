using GeekShow.Core.Component;

namespace GeekShow.Core.Model.TvMaze
{
    public class TvMazeLinks : NotifyPropertyChanged
    {
        private TvMazeLink previousEpisode;

        private TvMazeLink nextEpisode;

        public TvMazeLink Self
        {
            get;
            set;
        }

        public TvMazeLink PreviousEpisode
        {
            get
            {
                return previousEpisode;
            }
            set
            {
                if (previousEpisode == value) return;

                previousEpisode = value;
            }
        }

        public TvMazeLink NextEpisode
        {
            get
            {
                return nextEpisode;
            }
            set
            {
                if (nextEpisode == value) return;

                nextEpisode = value;
            }
        }
    }
}
