using GeekShow.Component;
using GeekShow.Core.Model.TvMaze;
using GeekShow.Core.Service;
using GeekShow.Shared.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class EpisodeSummaryViewModel : ViewModelBase
    {
        private RelayCommand _previousEpisodeCommand;
        private RelayCommand _nextEpisodeCommand;
        private RelayCommand _homeCommand;

        private TvMazeTvShow _tvShow;
        private TvMazeEpisode _episode;

        private List<TvMazeEpisode> _episodes;
        private int _currentEpisodeIndex;
        private bool _isProcessInProgress;

        private readonly IPopupMessageService popupMessageService;

        public EpisodeSummaryViewModel(INavigationService navigationService, ITvShowService tvShowService, IPopupMessageService popupMessageService)
            : base(navigationService, tvShowService)
        {
            this.popupMessageService = popupMessageService;
        }

        #region Properties

        public ICommand HomeCommand
        {
            get
            {
                return _homeCommand ?? (_homeCommand = new RelayCommand(param => GoHome()));
            }
        }

        public ICommand PreviousEpisodeCommand
        {
            get
            {
                return _previousEpisodeCommand ?? (_previousEpisodeCommand = new RelayCommand(param => SelectPreviousEpisode()));
            }
        }

        public ICommand NextEpisodeCommand
        {
            get
            {
                return _nextEpisodeCommand ?? (_nextEpisodeCommand = new RelayCommand(param => SelectNextEpisode()));
            }
        }

        public TvMazeTvShow TvShow
        {
            get
            {
                return _tvShow;
            }
            set
            {
                if (_tvShow == value) return;

                _tvShow = value;

                RaisePropertyChanged(nameof(TvShow));
            }
        }

        public TvMazeEpisode Episode
        {
            get
            {
                return _episode;
            }
            set
            {
                if (_episode == value) return;

                _episode = value;

                RaisePropertyChanged(nameof(Episode));
            }
        }

        public bool IsProcessInProgress
        {
            get
            {
                return _isProcessInProgress;
            }
            private set
            {
                if (_isProcessInProgress == value) return;

                _isProcessInProgress = value;

                RaisePropertyChanged(nameof(IsProcessInProgress));
            }
        }

        #endregion

        #region Public Methods

        public void InitializeEpisodeSummary()
        {
            _episodes = TvShow.Episodes.OrderBy(e => e.EpisodeId).ToList();
            _currentEpisodeIndex = _episodes.Count - 1;

            if(_currentEpisodeIndex < 0)
            {
                return;
            }

            Episode = _episodes[_currentEpisodeIndex];
        }

        #endregion

        #region Private Methods

        private void GoHome()
        {
            NavigationService.GoHome();
        }

        private void SelectNextEpisode()
        {
            if (_currentEpisodeIndex == _episodes.Count - 1)
            {
                return;
            }

            Episode = _episodes[++_currentEpisodeIndex];
        }

        private async void SelectPreviousEpisode()
        {
            if(_currentEpisodeIndex == 0 && Episode != null && Episode.Season == 1 && Episode.EpisodeNumber == 1)
            {
                return;
            }

            if(_currentEpisodeIndex == 0)
            {
                IsProcessInProgress = true;

                try
                {
                    var previousEpisode = await DownloadPreviousEpisode();

                    TvShow.Episodes.Add(previousEpisode);
                    _episodes.Insert(0, previousEpisode);

                    SaveTvShows();

                    Episode = previousEpisode;
                }
                catch (Exception ex)
                {
                    popupMessageService.DisplayMessage(ex.Message, "Error retrieving episode");
                }
                finally
                {
                    IsProcessInProgress = false;
                }

                return;
            }

            Episode = _episodes[--_currentEpisodeIndex];
        }

        private async Task<TvMazeEpisode> DownloadPreviousEpisode()
        {
            var season = Episode.EpisodeNumber == 1 ? Episode.Season - 1 : Episode.Season;
            var episodeNumber = Episode.EpisodeNumber - 1;

            if(Episode.EpisodeNumber == 1)
            {
                var seasons = await TvShowService.GetTvShowSeasonsAsync(TvShow.Id);

                episodeNumber = seasons.Single(s => s.SeasonNumber == season).EpisodeOrder.Value;
            }

            var episode = await TvShowService.GetEpisodeByNumberAsync(TvShow.Id, season, episodeNumber);
            
            return episode;
        }

        #endregion
    }
}
