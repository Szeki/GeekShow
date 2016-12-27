using GeekShow.Component;
using GeekShow.Core.Model.TvMaze;
using GeekShow.Shared.Component;
using GeekShow.View;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class ListMyTvShowViewModel : ViewModelBase
    {
        #region Members

        RelayCommand _tvShowItemClickedCommand;

        #endregion

        #region Constructor

        public ListMyTvShowViewModel(INavigationService navigationService, Core.Service.ITvShowService tvShowService)
            : base(navigationService, tvShowService)
        {

        }

        #endregion

        #region Properties

        public ICommand TvShowItemClickedCommand
        {
            get
            {
                return _tvShowItemClickedCommand ?? (_tvShowItemClickedCommand = new RelayCommand(param => TvShowItemClicked(param)));
            }
        }

        #endregion

        public override void ClearViewModel()
        {
            base.ClearViewModel();
        }

        #region Private Methods

        private void TvShowItemClicked(object param)
        {
            var tvShow = param as TvMazeTvShow;

            if (tvShow == null)
            {
                return;
            }

            NavigationService.Navigate(typeof(TvShowSubscribedItemDetailsPage), tvShow);
        }

        #endregion
    }
}
