﻿using GeekShow.Component;
using GeekShow.Core.Model.TvMaze;
using GeekShow.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GeekShow.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TvShowSubscribedItemDetailsPage : Page
    {
        #region Constructor

        public TvShowSubscribedItemDetailsPage()
        {
            this.DataContext = IoC.Resolve<TvShowSubscribedItemDetailsViewModel>();
            this.InitializeComponent();
        }

        #endregion

        #region Page EventHandlers

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New || e.NavigationMode == NavigationMode.Refresh || e.NavigationMode == NavigationMode.Back)
            {
                var viewModel = this.DataContext as TvShowSubscribedItemDetailsViewModel;

                if(viewModel == null)
                {
                    return;
                }

                viewModel.TvShow = e.Parameter as TvMazeTvShow;
            }
        }

        #endregion
    }
}
