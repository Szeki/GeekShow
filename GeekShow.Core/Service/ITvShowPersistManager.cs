using GeekShow.Core.Model;
using GeekShow.Core.Model.TvMaze;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekShow.Core.Component
{
    public interface ITvShowPersistManager
    {
        void SaveTvShows(IEnumerable<TvMazeTvShow> tvShows);
        Task SaveTvShowsAsync(IEnumerable<TvMazeTvShow> tvShows);

        IEnumerable<TvMazeTvShow> LoadTvShows();
        Task<IEnumerable<TvMazeTvShow>> LoadTvShowsAsync();

        IEnumerable<NotifiedTvShowItem> LoadNotifiedTvShows();
        Task<IEnumerable<NotifiedTvShowItem>> LoadNotifiedTvShowsAsync();

        void SaveNotifiedTvShows(IEnumerable<NotifiedTvShowItem> tvShows);
        Task SaveNotifiedTvShowsAsync(IEnumerable<NotifiedTvShowItem> tvShows);
    }
}
