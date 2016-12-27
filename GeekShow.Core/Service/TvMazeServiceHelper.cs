using GeekShow.Core.Model.TvMaze;
using System.Linq;
using System.Text.RegularExpressions;

namespace GeekShow.Core.Service
{
    public static class TvMazeServiceHelper
    {
        private const string EpisodeUrlPattern = @"http://api.tvmaze.com/episodes/(?<episodeId>\d+)";

        public static string GetEpisodeIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            var match = Regex.Match(url, EpisodeUrlPattern);

            if (match.Success)
            {
                return match.Groups["episodeId"].Value;
            }

            return string.Empty;
        }

        public static string NormalizeHtmlText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Replace("<p>", string.Empty)
                       .Replace("</p>", string.Empty)
                       .Replace("<em>", string.Empty)
                       .Replace("</em>", string.Empty)
                       .Replace("<strong>", string.Empty)
                       .Replace("</strong>", string.Empty);
        }

        public static void UpdateTvShow(ITvShowService service, TvMazeTvShow tvShow, TvMazeItem updatedTvShow)
        {
            if (tvShow.Updated == updatedTvShow.Updated)
            {
                return;
            }

            tvShow.TvShow = updatedTvShow;

            var previousEpisodeId = GetEpisodeIdFromUrl(updatedTvShow.Links?.PreviousEpisode.Href);
            var nextEpisodeId = GetEpisodeIdFromUrl(updatedTvShow.Links?.NextEpisode.Href);

            if (previousEpisodeId != tvShow.PreviousEpisode?.Id)
            {
                var previousEpisode = tvShow.Episodes.FirstOrDefault(e => e.Id == previousEpisodeId);

                if (previousEpisode == null)
                {
                    previousEpisode = service.GetEpisode(previousEpisodeId);

                    tvShow.Episodes.Add(previousEpisode);
                }

                tvShow.PreviousEpisode = previousEpisode;
            }
            
            if (string.IsNullOrEmpty(nextEpisodeId))
            {
                tvShow.NextEpisode = null;

                return;
            }
            
            var nextEpisode = tvShow.Episodes.FirstOrDefault(e => e.Id == nextEpisodeId);

            if (nextEpisode == null || string.IsNullOrEmpty(nextEpisode.Summary) ||
                nextEpisode.EpisodeName == "TBA" || nextEpisode.EpisodeName == "TBD")
            {
                var episode = service.GetEpisode(nextEpisodeId);
                
                if (nextEpisode == null)
                {
                    tvShow.Episodes.Add(episode);
                }

                nextEpisode = episode;
            }

            tvShow.NextEpisode = nextEpisode;
        }
    }
}
