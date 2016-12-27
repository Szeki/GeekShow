using GeekShow.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekShow.Shared.Service
{
    public interface ITvShowEpisodeService
    {
        IEnumerable<TvShowEpisodeQucikInfo> GetEpisodes(string tvShowName);
        Task<IEnumerable<TvShowEpisodeQucikInfo>> GetEpisodesAsync(string tvShowName);
        IEnumerable<TvShowEpisodeQucikInfo> GetEpisodes(string tvShowName, int? showStartYear);
        Task<IEnumerable<TvShowEpisodeQucikInfo>> GetEpisodesAsync(string tvShowName, int? showStartYear);
        TvShowEpisode GetEpisode(string tvShowName, int season, int episodeNumber);
        Task<TvShowEpisode> GetEpisodeAsync(string tvShowName, int season, int episodeNumber);
    }
}
