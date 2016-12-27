using GeekShow.Component;
using GeekShow.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace GeekShow.View
{
    public sealed partial class SearchTvShowView : UserControl
    {
        public SearchTvShowView()
        {
            this.InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                var viewModel = this.DataContext as SearchTvShowViewModel;

                if (viewModel.CanSearch)
                {
                    viewModel.SearchTvShow();
                }
            }
        }
    }
}
