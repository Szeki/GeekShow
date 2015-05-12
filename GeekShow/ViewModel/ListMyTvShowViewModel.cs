using GeekShow.Component;
using GeekShow.Shared.Model;
using GeekShow.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeekShow.ViewModel
{
    public class ListMyTvShowViewModel : ViewModelBase
    {
        #region Members

        RelayCommand _tvShowItemClickedCommand;

        #endregion

        #region Constructor
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

        #region Private Methods

        private void TvShowItemClicked(object param)
        {
            var tvShow = param as TvShowSubscribedItem;

            if (tvShow == null)
            {
                return;
            }

            NavigationService.Navigate(typeof(TvShowSubscribedItemDetailsPage), new TvShowSubscribedItemDetailsViewModel(tvShow));
        }

        #endregion
    }
}
