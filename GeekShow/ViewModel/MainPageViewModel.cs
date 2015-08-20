using GeekShow.Shared.Model;

namespace GeekShow.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Members

        int _selectedMenuIndex;

        #endregion

        #region Constructor

        public MainPageViewModel()
        {
            LoadPersistedTvShows();

            SearchTvShowViewModel = new SearchTvShowViewModel();
            ListMyTvShowViewModel = new ListMyTvShowViewModel();
        }

        #endregion

        #region Properties

        public int SelectedMenuIndex
        {
            get
            {
                return _selectedMenuIndex;
            }
            set
            {
                if (_selectedMenuIndex == value) return;

                _selectedMenuIndex = value;

                RaisePropertyChanged(nameof(SelectedMenuIndex));
            }
        }

        public SearchTvShowViewModel SearchTvShowViewModel
        {
            get;
            private set;
        }

        public ListMyTvShowViewModel ListMyTvShowViewModel
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        public override void ClearViewModel()
        {
            SearchTvShowViewModel.ClearViewModel();
            ListMyTvShowViewModel.ClearViewModel();
        }

        public void SelectViewModel(MainPageItems item)
        {
            SelectedMenuIndex = item == MainPageItems.List ? 0 : 1;
        }

        #endregion
    }
}
