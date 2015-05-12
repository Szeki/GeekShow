using GeekShow.Component;
using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace GeekShow
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Members
        #endregion

        #region Constructor

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.DataContext = IoC.Container.ResolveType<MainPageViewModel>();
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
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            if (e.NavigationMode == NavigationMode.New)
            {
                var viewModel = this.DataContext as MainPageViewModel;

                viewModel.ClearViewModel();

                var parameterValue = e.Parameter == null ? string.Empty : e.Parameter.ToString();

                if (!string.IsNullOrEmpty(parameterValue))
                {
                    viewModel.SelectViewModel((MainPageItems)e.Parameter);
                }
            }
        }

        #endregion

        #region EventHandlers

        private void MainContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FirstMenuItemImage1.Visibility = MainContainer.SelectedIndex == 1 ? Visibility.Collapsed : Visibility.Visible;
            FirstMenuItemImage2.Visibility = MainContainer.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;

            SecondMenuItemImage1.Visibility = MainContainer.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
            SecondMenuItemImage2.Visibility = MainContainer.SelectedIndex == 1 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void FirstMenuItemStackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var viewModel = this.DataContext as MainPageViewModel;

            viewModel.SelectedMenuIndex = 0;
        }

        private void SecondMenuItemStackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var viewModel = this.DataContext as MainPageViewModel;

            viewModel.SelectedMenuIndex = 1;
        }

        #endregion
    }
}
