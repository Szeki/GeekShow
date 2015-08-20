using GeekShow.Shared.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GeekShow.View.Converters
{
    public class NextEpisodeDatesVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var tvShow = value as TvShowSubscribedItem;

            if(tvShow == null || tvShow.NextEpisodeDate == null)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
