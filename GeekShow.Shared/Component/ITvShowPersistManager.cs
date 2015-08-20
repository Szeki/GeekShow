using GeekShow.Shared.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekShow.Shared.Component
{
    public interface ITvShowPersistManager
    {
        void SaveTvShows(IEnumerable<TvShowSubscribedItem> tvShows);
        Task SaveTvShowsAsync(IEnumerable<TvShowSubscribedItem> tvShows);

        IEnumerable<TvShowSubscribedItem> LoadTvShows();
        Task<IEnumerable<TvShowSubscribedItem>> LoadTvShowsAsync();

        IEnumerable<NotifiedTvShowItem> LoadNotifiedTvShows();
        Task<IEnumerable<NotifiedTvShowItem>> LoadNotifiedTvShowsAsync();

        void SaveNotifiedTvShows(IEnumerable<NotifiedTvShowItem> tvShows);
        Task SaveNotifiedTvShowsAsync(IEnumerable<NotifiedTvShowItem> tvShows);
    }
}
