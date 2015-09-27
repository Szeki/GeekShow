using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekShow.Shared.Model;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace GeekShow.Shared.Service
{
    public class TvShowEpisodeImdbService : ITvShowEpisodeService
    {
        #region Members

        readonly static string SearchEpisodesUrl = @"http://imdbapi.poromenos.org/js/?name={0}&year={1}";
        readonly static string GetEpisodesUrl = @"http://www.omdbapi.com/?t={0}&season={1}&episode={2}&plot=full&r=json";
        readonly static string DefaultEpisodeNamePattern = @"Episode #.+";
        readonly TvShowEpisodeServiceQueryResultParser _parser;

        #endregion

        #region Constructor

        public TvShowEpisodeImdbService()
        {
            _parser = new TvShowEpisodeServiceQueryResultParser();
        }

        #endregion

        #region ITvShowEpisodeHelperService implemenation

        public IEnumerable<TvShowEpisodeQucikInfo> GetEpisodes(string tvShowName)
        {
            return GetEpisodes(tvShowName, null);
        }

        public IEnumerable<TvShowEpisodeQucikInfo> GetEpisodes(string tvShowName, int? showStartYear)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri(string.Format(SearchEpisodesUrl, tvShowName, showStartYear)));

                var response = task.Result;

                var episodes = _parser.ParseEpisodesFromResponse(tvShowName, response);

                return episodes.Where(ep => !Regex.IsMatch(ep.Name, DefaultEpisodeNamePattern)).OrderBy(ep => ep.Season).ThenBy(ep => ep.EpisodeNumber);
            }
        }

        public Task<IEnumerable<TvShowEpisodeQucikInfo>> GetEpisodesAsync(string tvShowName)
        {
            return Task.Factory.StartNew(() => GetEpisodes(tvShowName));
        }

        public Task<IEnumerable<TvShowEpisodeQucikInfo>> GetEpisodesAsync(string tvShowName, int? showStartYear)
        {
            return Task.Factory.StartNew(() => GetEpisodes(tvShowName, showStartYear));
        }

        public TvShowEpisode GetEpisode(string tvShowName, int season, int episodeNumber)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(string.Format(GetEpisodesUrl, tvShowName, season, episodeNumber));

                var response = task.Result;

                return _parser.ParseEpisodeFromResponse(response);
            }
        }

        public Task<TvShowEpisode> GetEpisodeAsync(string tvShowName, int season, int episodeNumber)
        {
            return Task.Factory.StartNew(() => GetEpisode(tvShowName, season, episodeNumber));
        }

        #endregion
    }
}
