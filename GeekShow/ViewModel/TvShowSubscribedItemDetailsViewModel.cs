﻿using GeekShow.Component;
using GeekShow.Core.Model.TvMaze;
using GeekShow.Core.Service;
using GeekShow.Shared.Component;
using GeekShow.View;
using System;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class TvShowSubscribedItemDetailsViewModel : ViewModelBase
    {
        #region Members

        RelayCommand _dropItemCommand;
        RelayCommand _backCommand;
        RelayCommand _refreshCommand;
        RelayCommand _openEpisodeSummaryCommand;

        bool _isProcessInProgress;

        readonly IPopupMessageService _popupService;
        
        #endregion

        #region Constructor

        public TvShowSubscribedItemDetailsViewModel(INavigationService navigationService, IPopupMessageService popupService,
            Core.Service.ITvShowService service)
            : base(navigationService, service)
        {
            _popupService = popupService;
        }

        #endregion

        #region Properties

        public TvMazeTvShow TvShow
        {
            get;
            set;
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

        public ICommand OpenEpisodeSummaryCommand
        {
            get
            {
                return (_openEpisodeSummaryCommand ?? (_openEpisodeSummaryCommand = new RelayCommand(param => OpenEpisodes())));
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
                var tvShowBaseInfo = await TvShowService.GetTvShowAsync(TvShow.Id);

                TvMazeServiceHelper.UpdateTvShow(TvShowService, TvShow, tvShowBaseInfo);

                SaveTvShows();
            }
            catch(Exception ex)
            {
                _popupService.DisplayMessage(ex.Message, "Problem during refresh");
            }

            IsProcessInProgress = false;
        }
        
        private void OpenEpisodes()
        {
            NavigationService.Navigate(typeof(EpisodeSummaryPage), TvShow);
        }

        #endregion
    }
}
