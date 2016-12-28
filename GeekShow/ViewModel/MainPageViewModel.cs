using GeekShow.Component;
using GeekShow.Core.Model;
using GeekShow.Shared.Component;
using GeekShow.View;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Members

        int _selectedMenuIndex;

        RelayCommand _openSettingsCommand;

        #endregion

        #region Constructor

        public MainPageViewModel(INavigationService navigationService, Core.Service.ITvShowService tvShowService,
            SearchTvShowViewModel searchTvShowViewModel, ListMyTvShowViewModel listMyTvShowViewModel)
            : base(navigationService, tvShowService)
        {
            LoadPersistedTvShows();

            SearchTvShowViewModel = searchTvShowViewModel;
            ListMyTvShowViewModel = listMyTvShowViewModel;
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

        public ICommand OpenSettingsCommand
        {
            get
            {
                return _openSettingsCommand ?? (_openSettingsCommand = new RelayCommand(param => OpenSettings()));
            }
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

        #region Private Methods

        private void OpenSettings()
        {
            NavigationService.Navigate(typeof(SettingsPage));
        }

        #endregion
    }
}
