using GeekShow.Core.Model;
using GeekShow.Core.Model.TvMaze;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GeekShow.View.Converters
{
    public class NextEpisodeDatesVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //var tvShow = value as TvMazeTvShow;

            //if(tvShow == null || tvShow.NextEpisode == null)
            //{
            //    return Visibility.Collapsed;
            //}

            var tvShowEpisode = value as TvMazeEpisode;

            if (tvShowEpisode == null)
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
