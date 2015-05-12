using GeekShow.Component;
using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class TvShowSearchItemDetailsViewModel : ViewModelBase
    {
        #region Members

        RelayCommand _subscribeCommand;
        RelayCommand _cancelCommand;

        bool _isProcessingData;

        #endregion

        #region Constructor

        public TvShowSearchItemDetailsViewModel(TvShowItem tvShow)
        {
            TvShow = tvShow;
        }

        #endregion

        #region Properties

        public TvShowItem TvShow
        {
            get;
            private set;
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

                RaisePropertyChanged("IsProcessingData");
            }
        }

        #endregion

        #region Private Methods

        private async void SubscribeToTvShow()
        {
            if(IsProcessingData)
            {
                return;
            }

            IsProcessingData = true;

            var processed = false;

            if (TvShows.All(item => item.ShowId != TvShow.ShowId))
            {
                var show = TvShowSubscribedItem.CloneBaseItem(TvShow);

                try
                {
                    var showQuickInfo = await TvShowService.GetTvShowQuickInfoAsync(show.Name);

                    EnrichTvShow(show, showQuickInfo);

                    AddTvShowAndPersist(show);

                    processed = true;
                }
                catch
                {
                    var popupService = IoC.Container.ResolveType<IPopupMessageService>();

                    popupService.DisplayMessage("Problem with network connection", "No internet connection");
                }
            }

            IsProcessingData = false;

            if (processed)
            {
                NavigationService.Navigate(typeof(MainPage), MainPageItems.List);
            }
        }

        private void EnrichTvShow(TvShowSubscribedItem tvShow, TvShowQuickInfoItem tvShowQuickInfo)
        {
            tvShow.Network = tvShowQuickInfo.Network;
            tvShow.AirTime = tvShowQuickInfo.AirTime;
            tvShow.RunTime = tvShowQuickInfo.Runtime;
            tvShow.LastEpisodeId = tvShowQuickInfo.LastEpisodeId;
            tvShow.LastEpisodeName = tvShowQuickInfo.LastEpisodeName;
            tvShow.LastEpisodeDate = tvShowQuickInfo.LastEpisodeDate;
            tvShow.NextEpisodeId = tvShowQuickInfo.NextEpisodeId;
            tvShow.NextEpisodeName = tvShowQuickInfo.NextEpisodeName;
            tvShow.NextEpisodeDate = tvShowQuickInfo.NextEpisodeDate;
        }

        private void CancelDetailsView()
        {
            if (IsProcessingData)
            {
                return;
            }

            NavigationService.GoBack();
        }

        #endregion
    }
}
