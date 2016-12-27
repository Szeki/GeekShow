using GeekShow.Core.Model.TvMaze;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekShow.Core.Service
{
    public class TvMazeService : ITvShowService
    {
        private const string BaseUrl = "http://api.tvmaze.com";
        private const string SearchPath = "search/shows?q=";
        private const string ShowPath = "shows";
        private const string EpisodePath = "episodes";

        public IEnumerable<TvMazeItem> SearchShow(string showName)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri($"{BaseUrl}/{SearchPath}{UrlEncode(showName)}"));

                var response = task.Result;

                var result = JsonConvert.DeserializeObject<IEnumerable<TvMazeSearchResult>>(response).ToList();

                return result.OrderByDescending(r => r.Score).Select(r => r.Show);
            }
        }

        public async Task<IEnumerable<TvMazeItem>> SearchShowAsync(string showName)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri($"{BaseUrl}/{SearchPath}{UrlEncode(showName)}"));

                var response = await task;

                var result = JsonConvert.DeserializeObject<IEnumerable<TvMazeSearchResult>>(response).ToList();

                return result.OrderByDescending(r => r.Score).Select(r => r.Show);
            }
        }

        public TvMazeItem GetTvShow(int id)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri($"{BaseUrl}/{ShowPath}/{id}"));

                var response = task.Result;

                return JsonConvert.DeserializeObject<TvMazeItem>(response);
            }
        }

        public async Task<TvMazeItem> GetTvShowAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri($"{BaseUrl}/{ShowPath}/{id}"));

                var response = await task;

                return JsonConvert.DeserializeObject<TvMazeItem>(response);
            }
        }
        
        public TvMazeEpisode GetEpisode(string episodeId)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri($"{BaseUrl}/{EpisodePath}/{episodeId}"));

                var response = task.Result;

                return JsonConvert.DeserializeObject<TvMazeEpisode>(response);
            }
        }

        public async Task<TvMazeEpisode> GetEpisodeAsync(string episodeId)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri($"{BaseUrl}/{EpisodePath}/{episodeId}"));

                var response = await task;

                return JsonConvert.DeserializeObject<TvMazeEpisode>(response);
            }
        }

        private string UrlEncode(string value)
        {
            return WebUtility.UrlEncode(value);
        }
    }
}
