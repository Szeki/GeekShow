using GeekShow.Core.Model.TvMaze;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekShow.Core.Service
{
    public interface ITvShowService
    {
        IEnumerable<TvMazeItem> SearchShow(string showName);
        Task<IEnumerable<TvMazeItem>> SearchShowAsync(string showName);

        TvMazeItem GetTvShow(int id);
        Task<TvMazeItem> GetTvShowAsync(int id);

        TvMazeEpisode GetEpisode(string episodeId);
        Task<TvMazeEpisode> GetEpisodeAsync(string episodeId);

        TvMazeEpisode GetEpisodeByNumber(int showId, int season, int episodeNumber);
        Task<TvMazeEpisode> GetEpisodeByNumberAsync(int showId, int season, int episodeNumber);

        IEnumerable<TvMazeSeason> GetTvShowSeasons(int showId);
        Task<IEnumerable<TvMazeSeason>> GetTvShowSeasonsAsync(int showId);
    }
}
