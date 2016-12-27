using GeekShow.Component;
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
    public sealed partial class TvShowSearchItemDetailsPage : Page
    {
        public TvShowSearchItemDetailsPage()
        {
            this.DataContext = IoC.Resolve<TvShowSearchItemDetailsViewModel>();
            this.InitializeComponent();

            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();

                e.Handled = true;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.NavigationMode == NavigationMode.New || e.NavigationMode == NavigationMode.Refresh)
            {
                var viewModel = this.DataContext as TvShowSearchItemDetailsViewModel;

                if(viewModel == null)
                {
                    return;
                }

                viewModel.TvShow = e.Parameter as TvMazeItem;
            }
        }
    }
}
