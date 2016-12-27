using GeekShow.Component;
using GeekShow.Core.Model;
using GeekShow.Core.Model.TvMaze;
using GeekShow.Core.Service;
using GeekShow.Shared.Component;
using System;
using System.Linq;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class TvShowSearchItemDetailsViewModel : ViewModelBase
    {
        #region Members

        RelayCommand _subscribeCommand;
        RelayCommand _cancelCommand;

        bool _isProcessingData;

        readonly IPopupMessageService _popupService;

        #endregion

        #region Constructor

        public TvShowSearchItemDetailsViewModel(INavigationService navigationService, Core.Service.ITvShowService tvShowService, IPopupMessageService popupService)
            : base(navigationService, tvShowService)
        {
            _popupService = popupService;
        }

        #endregion

        #region Properties

        public TvMazeItem TvShow
        {
            get;
            set;
        }

        public ICommand SubscribeCommand
        {
            get
            {
                return _subscribeCommand ?? (_subscribeCommand = new RelayCommand(param => SubscribeToTvShow()));
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand(param => CancelDetailsView()));
            }
        }

        public bool IsProcessingData
        {
            get
            {
                return _isProcessingData;
            }
            private set
            {
                if (_isProcessingData == value) return;

                _isProcessingData = value;

                RaisePropertyChanged(nameof(IsProcessingData));
            }
        }

        #endregion

        #region Private Methods

        private void SubscribeToTvShow()
        {
            if(IsProcessingData)
            {
                return;
            }

            IsProcessingData = true;

            var processed = false;

            if (TvShows.All(item => item.TvShow.Id != TvShow.Id))
            {
                try
                {
                    AddTvShowAndPersist(GetTvShow(TvShow));

                    processed = true;
                }
                catch(Exception ex)
                {
                    _popupService.DisplayMessage("Problem during subscription", ex.Message);
                }
            }

            IsProcessingData = false;

            if (processed)
            {
                NavigationService.Navigate(typeof(MainPage), MainPageItems.List);
            }
        }
        
        private void CancelDetailsView()
        {
            if (IsProcessingData)
            {
                return;
            }

            NavigationService.GoBack();
        }

        private TvMazeTvShow GetTvShow(TvMazeItem item)
        {
            var tvShow = new TvMazeTvShow()
            {
                TvShow = TvShow
            };

            var previousEpisode = TvShow.Links.PreviousEpisode == null ? null : TvShowService.GetEpisode(TvMazeServiceHelper.GetEpisodeIdFromUrl(TvShow.Links.PreviousEpisode.Href));
            var nextEpisode = TvShow.Links.NextEpisode == null ? null : TvShowService.GetEpisode(TvMazeServiceHelper.GetEpisodeIdFromUrl(TvShow.Links.NextEpisode.Href));
            
            tvShow.PreviousEpisode = previousEpisode;
            tvShow.NextEpisode = nextEpisode;

            if (previousEpisode != null)
            {
                tvShow.Episodes.Add(previousEpisode);
            }

            if (nextEpisode != null)
            {
                tvShow.Episodes.Add(nextEpisode);
            }

            return tvShow;
        }

        #endregion
    }
}
