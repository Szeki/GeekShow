using GeekShow.Component;
using GeekShow.Core.Model.TvMaze;
using GeekShow.Shared.Component;
using GeekShow.View;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class SearchTvShowViewModel : ViewModelBase
    {
        #region Members

        RelayCommand _searchCommand;
        RelayCommand _tvShowItemClicked;

        string _searchText;
        bool _isProgressBarVisible;

        List<TvMazeItem> _searchResult;

        readonly IPopupMessageService _popupService;

        #endregion

        #region Constructor

        public SearchTvShowViewModel(INavigationService navigationService, 
            IPopupMessageService popupService, Core.Service.ITvShowService service)
            : base(navigationService, service)
        {
            _popupService = popupService;
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

                RaisePropertyChanged(nameof(SearchText));
            }
        }

        public bool CanSearch => !string.IsNullOrEmpty(SearchText) && !IsProgressBarVisible;

        public bool IsProgressBarVisible
        {
            get
            {
                return _isProgressBarVisible;
            }
            private set
            {
                if (_isProgressBarVisible == value) return;

                _isProgressBarVisible = value;

                RaisePropertyChanged(nameof(IsProgressBarVisible));
            }
        }

        public List<TvMazeItem> SearchResult
        {
            get
            {
                return _searchResult;
            }
            private set
            {
                if (_searchResult == value) return;

                _searchResult = value;

                RaisePropertyChanged(nameof(SearchResult));
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

            IsProgressBarVisible = true;

            SearchResult = null;

            try
            {
                var searchResult = await TvShowService.SearchShowAsync(SearchText);

                SearchResult = searchResult.ToList();
            }
            catch
            {
                _popupService.DisplayMessage("Problem with network connection", "No internet connection");

                SearchResult = new List<TvMazeItem>();
            }

            IsProgressBarVisible = false;
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
            var tvShow = param as TvMazeItem;

            if(tvShow == null)
            {
                return;
            }
            
            NavigationService.Navigate(typeof(TvShowSearchItemDetailsPage), tvShow);
        }
        
        #endregion
    }
}
