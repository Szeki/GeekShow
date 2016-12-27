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
    }
}
