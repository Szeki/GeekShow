using GeekShow.Component;
using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class TvShowSubscribedItemDetailsViewModel : ViewModelBase
    {
        #region Members

        RelayCommand _dropItemCommand;
        RelayCommand _backCommand;
        RelayCommand _refreshCommand;

        bool _isProcessInProgress;

        #endregion

        #region Constructor

        public TvShowSubscribedItemDetailsViewModel(TvShowSubscribedItem tvShow)
        {
            TvShow = tvShow;
        }

        #endregion

        #region Properties

        public TvShowSubscribedItem TvShow
        {
            get;
            private set;
        }

        public ICommand DropItemCommand
        {
            get
            {
                return _dropItemCommand ?? (_dropItemCommand = new RelayCommand(param => DropItem()));
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand(param => Back()));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand(param => Refresh()));
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

        #region Private Methods

        private void DropItem()
        {
            if(IsProcessInProgress)
            {
                return;
            }

            RemoveTvShowAndPersist(TvShow);

            NavigationService.GoBack();
        }

        private void Back()
        {
            if (IsProcessInProgress)
            {
                return;
            }

            NavigationService.GoBack();
        }

        private async void Refresh()
        {
            if (IsProcessInProgress)
            {
                return;
            }

            IsProcessInProgress = true;

            try
            {
                var tvShowBaseInfo = TvShowService.GetTvShowAsync(TvShow.ShowId);
                var tvShowQuickInfo = TvShowService.GetTvShowQuickInfoAsync(TvShow.Name);

                EnrichTvShow(TvShow, await tvShowBaseInfo, await tvShowQuickInfo);

                SaveTvShows();
            }
            catch
            {
                var popupService = IoC.Container.ResolveType<IPopupMessageService>();

                popupService.DisplayMessage("Problem with network connection", "No internet connection");
            }

            IsProcessInProgress = false;
        }

        private void EnrichTvShow(TvShowSubscribedItem tvShow, TvShowItem tvShowBaseInfo, TvShowQuickInfoItem tvShowQuickInfo)
        {
            tvShow.Network = tvShowQuickInfo.Network;
            tvShow.AirTime = tvShowQuickInfo.AirTime;
            tvShow.RunTime = tvShowQuickInfo.Runtime;
            tvShow.EndDate = tvShowQuickInfo.Ended;
            tvShow.LastEpisodeId = tvShowQuickInfo.LastEpisodeId;
            tvShow.LastEpisodeName = tvShowQuickInfo.LastEpisodeName;
            tvShow.LastEpisodeDate = tvShowQuickInfo.LastEpisodeDate;
            tvShow.NextEpisodeId = tvShowQuickInfo.NextEpisodeId;
            tvShow.NextEpisodeName = tvShowQuickInfo.NextEpisodeName;
            tvShow.NextEpisodeDate = tvShowQuickInfo.NextEpisodeDate;
            tvShow.Seasons = tvShowBaseInfo.Seasons;
            tvShow.Ended = tvShowBaseInfo.EndDate == null ? 0 : tvShowBaseInfo.EndDate.Value.Year;
        }

        #endregion
    }
}
