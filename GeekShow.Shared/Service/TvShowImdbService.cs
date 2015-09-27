using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekShow.Shared.Model;
using System.Net.Http;
using GeekShow.Shared.Component;
using System.Text.RegularExpressions;

namespace GeekShow.Shared.Service
{
    public class TvShowImdbService : ITvShowService
    {
        #region Members

        readonly static string SearchShowUrl = @"http://www.omdbapi.com/?s={0}&type=series&r=json";
        readonly static string GetShowByIdUrl = @"http://www.omdbapi.com/?i={0}&type=series&plot=full&r=json";
        readonly static string GetShowByNameUrl = @"http://www.omdbapi.com/?t={0}&type=series&plot=full&r=json";
        readonly TvShowImdbServiceQueryReultParser _parser;

        #endregion

        #region Constructor

        public TvShowImdbService()
        {
            _parser = new TvShowImdbServiceQueryReultParser();
        }

        #endregion

        #region ITvShowService implementation

        public TvShowItem GetTvShow(string showId)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(string.Format(GetShowByIdUrl, showId));

                var response = task.Result;

                return (TvShowItem)_parser.ParseTvShowResponse(response);
            }
        }

        public Task<TvShowItem> GetTvShowAsync(string showId)
        {
            return Task.Factory.StartNew(() => GetTvShow(showId));
        }

        public TvShowQuickInfoItem GetTvShowQuickInfo(string tvShowName)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(string.Format(GetShowByNameUrl, tvShowName));

                var response = task.Result;

                var tvShowItem = (TvShowQuickInfoItem)_parser.ParseTvShowResponse(response);

                var subTask = EnrichEpisodeInformation(tvShowItem);

                subTask.Wait();

                return tvShowItem;
            }
        }

        public Task<TvShowQuickInfoItem> GetTvShowQuickInfoAsync(string tvShowName)
        {
            return Task.Factory.StartNew(() => GetTvShowQuickInfo(tvShowName));
        }

        public IEnumerable<TvShowItem> SearchShow(string searchValue)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri(string.Format(SearchShowUrl, searchValue)));

                var returnValue = task.Result;

                var parsedItem = _parser.GetSearchedShows(returnValue);

                return parsedItem.Select(t => (TvShowItem)t);
            }
        }

        public Task<IEnumerable<TvShowItem>> SearchShowAsync(string searchValue)
        {
            return Task.Factory.StartNew(() => SearchShow(searchValue));
        }

        #endregion

        #region Private Methods
        
        private async Task EnrichEpisodeInformation(TvShowQuickInfoItem tvShow)
        {
            var episodeService = IoC.Container.ResolveType<ITvShowEpisodeService>();
            var today = DateTimeProvider.UtcNow.Date;

            var episodes = await episodeService.GetEpisodesAsync(tvShow.ShowName, tvShow.Started.Year);

            TvShowEpisode nextEpisode = null;

            foreach(var episodeQuick in episodes.Reverse())
            {
                var episode = await episodeService.GetEpisodeAsync(tvShow.ShowName, episodeQuick.Season, episodeQuick.EpisodeNumber);

                if(episode.ReleaseDate == null)
                {
                    continue;
                }

                if(Regex.IsMatch(episode.Title, "Episode #.+"))
                {
                    episode.Title = episodeQuick.Name;
                }

                if(episode.ReleaseDate >= today)
                {
                    nextEpisode = episode;
                }
                else
                {
                    EnrichEpisode(tvShow, episode, nextEpisode);

                    return;
                }
            }
        }

        private void EnrichEpisode(TvShowQuickInfoItem tvShow, TvShowEpisode lastEpisode, TvShowEpisode nextEpisode)
        {
            tvShow.LastEpisodeDate = lastEpisode.ReleaseDate;
            tvShow.LastEpisodeId = GetEpisodeId(lastEpisode.Season, lastEpisode.Episode);
            tvShow.LastEpisodeName = lastEpisode.Title;
            tvShow.NextEpisodeDate = nextEpisode?.ReleaseDate;
            tvShow.NextEpisodeId = nextEpisode == null ? null : GetEpisodeId(nextEpisode.Season, nextEpisode.Episode);
            tvShow.NextEpisodeName = nextEpisode?.Title;
        }

        private string GetEpisodeId(int season, int episodeNumber)
        {
            return string.Format("{0}x{1:D2}", season, episodeNumber);
        }

        #endregion
    }
}
