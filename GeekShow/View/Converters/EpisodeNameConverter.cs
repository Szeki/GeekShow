using GeekShow.Core.Model.TvMaze;
using System;
using Windows.UI.Xaml.Data;

namespace GeekShow.View.Converters
{
    public class EpisodeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var episode = value as TvMazeEpisode;

            if(episode == null)
            {
                return null;
            }

            return $"{episode.EpisodeId}: {episode.EpisodeName}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
