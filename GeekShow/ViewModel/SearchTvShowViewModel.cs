using GeekShow.Component;
using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.Shared.Service;
using GeekShow.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class SearchTvShowViewModel : ViewModelBase
    {
        #region Members

        RelayCommand _searchCommand;
        RelayCommand _tvShowItemClicked;

        string _searchText;
        bool _isSearchInProgress;

        List<TvShowItem> _searchResult;

        #endregion

        #region Constructor

        public SearchTvShowViewModel()
        {
        }

        #endregion

        #region Properties

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(param => SearchTvShow()));
            }
        }

        public ICommand TvShowItemClickedCommand
        {
            get
            {
                return _tvShowItemClicked ?? (_tvShowItemClicked = new RelayCommand(param => TvShowClicked(param)));
            }
        }

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText == value) return;

                _searchText = value;

                RaisePropertyChanged("SearchText");
            }
        }

        public bool CanSearch
        {
            get
            {
                return !string.IsNullOrEmpty(SearchText) && !IsSearchInProgress;
            }
        }

        public bool IsSearchInProgress
        {
            get
            {
                return _isSearchInProgress;
            }
            private set
            {
                if (_isSearchInProgress == value) return;

                _isSearchInProgress = value;

                RaisePropertyChanged("IsSearchInProgress");
            }
        }

        public List<TvShowItem> SearchResult
        {
            get
            {
                return _searchResult;
            }
            private set
            {
                if (_searchResult == value) return;

                _searchResult = value;

                RaisePropertyChanged("SearchResult");
            }
        }

        #endregion

        #region Public Methods

        public async void SearchTvShow()
        {
            if(!CanSearch)
            {
                return;
            }

            IsSearchInProgress = true;

            SearchResult = null;

            try
            {
                var searchResult = await TvShowService.SearchShowAsync(SearchText);

                SearchResult = new List<TvShowItem>(searchResult);
            }
            catch
            {
                var popupService = IoC.Container.ResolveType<IPopupMessageService>();

                popupService.DisplayMessage("Problem with network connection", "No internet connection");

                SearchResult = new List<TvShowItem>();
            }

            IsSearchInProgress = false;
        }

        public override void ClearViewModel()
        {
            SearchText = string.Empty;
            SearchResult = null;
        }

        #endregion

        #region Private Methods

        private void TvShowClicked(object param)
        {
            var tvShow = param as TvShowItem;

            if(tvShow == null)
            {
                return;
            }

            NavigationService.Navigate(typeof(TvShowSearchItemDetailsPage), new TvShowSearchItemDetailsViewModel(tvShow));
        }

        #endregion
    }
}
