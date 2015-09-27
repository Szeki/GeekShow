using GeekShow.Component;
using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.Shared.Service;
using GeekShow.View;
using System.Collections.Generic;
using System.Linq;
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
        bool _isProgressBarVisible;

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

                SearchResult = new List<TvShowItem>(searchResult);
            }
            catch
            {
                var popupService = IoC.Container.ResolveType<IPopupMessageService>();

                popupService.DisplayMessage("Problem with network connection", "No internet connection");

                SearchResult = new List<TvShowItem>();
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

        private async void TvShowClicked(object param)
        {
            var tvShow = param as TvShowItem;

            if(tvShow == null)
            {
                return;
            }

            IsProgressBarVisible = true;

            await EnrichSeasonInformation(tvShow);

            await EnrichCountryAndPlotInformation(tvShow);

            IsProgressBarVisible = false;

            NavigationService.Navigate(typeof(TvShowSearchItemDetailsPage), new TvShowSearchItemDetailsViewModel(tvShow));
        }
        
        private async Task EnrichSeasonInformation(TvShowItem tvShow)
        {
            if(tvShow == null || tvShow.Seasons != default(int))
            {
                return;
            }

            var helperService = IoC.Container.ResolveType<ITvShowEpisodeService>();

            try
            {
                var episodes = await helperService.GetEpisodesAsync(tvShow.Name, tvShow.Started);

                if (episodes.Any())
                {
                    tvShow.Seasons = episodes.Max(ep => ep.Season);
                }
            }
            catch { }
        }

        private async Task EnrichCountryAndPlotInformation(TvShowItem tvShow)
        {
            if(tvShow == null || !string.IsNullOrEmpty(tvShow.Country) || !string.IsNullOrEmpty(tvShow.Plot))
            {
                return;
            }

            try
            {
                var tvShowFullInfo = await TvShowService.GetTvShowAsync(tvShow.ShowId);

                tvShow.Country = tvShowFullInfo.Country;
                tvShow.Plot = tvShowFullInfo.Plot;
            }
            catch { }
        }

        #endregion
    }
}
