using GeekShow.Shared.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekShow.Shared.Model;

namespace GeekShow.Test.Mocks
{
    public class TvShowPersistManagerMock : MockBase, ITvShowPersistManager
    {
        public IEnumerable<NotifiedTvShowItem> LoadNotifiedTvShows()
        {
            RecordCall(nameof(ITvShowPersistManager.LoadNotifiedTvShows));

            ProcessMethodAction(nameof(ITvShowPersistManager.LoadNotifiedTvShows));

            return ReturnValueForAnyArgs<IEnumerable<NotifiedTvShowItem>>(nameof(ITvShowPersistManager.LoadNotifiedTvShows));
        }

        public Task<IEnumerable<NotifiedTvShowItem>> LoadNotifiedTvShowsAsync()
        {
            RecordCall(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync));

            ProcessMethodAction(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync));

            return ReturnValueForAnyArgs<Task<IEnumerable<NotifiedTvShowItem>>>(nameof(ITvShowPersistManager.LoadNotifiedTvShowsAsync));
        }

        public IEnumerable<TvShowSubscribedItem> LoadTvShows()
        {
            RecordCall(nameof(ITvShowPersistManager.LoadTvShows));

            ProcessMethodAction(nameof(ITvShowPersistManager.LoadTvShows));

            return ReturnValueForAnyArgs<IEnumerable<TvShowSubscribedItem>>(nameof(ITvShowPersistManager.LoadTvShows));
        }

        public Task<IEnumerable<TvShowSubscribedItem>> LoadTvShowsAsync()
        {
            RecordCall(nameof(ITvShowPersistManager.LoadTvShowsAsync));

            ProcessMethodAction(nameof(ITvShowPersistManager.LoadTvShowsAsync));

            return ReturnValueForAnyArgs<Task<IEnumerable<TvShowSubscribedItem>>>(nameof(ITvShowPersistManager.LoadTvShowsAsync));
        }

        public void SaveNotifiedTvShows(IEnumerable<NotifiedTvShowItem> tvShows)
        {
            RecordCall(nameof(ITvShowPersistManager.SaveNotifiedTvShows), tvShows);

            ProcessMethodAction(nameof(ITvShowPersistManager.SaveNotifiedTvShows));
        }

        public Task SaveNotifiedTvShowsAsync(IEnumerable<NotifiedTvShowItem> tvShows)
        {
            RecordCall(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync), tvShows);

            ProcessMethodAction(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync));

            return ReturnValueForAnyArgs<Task>(nameof(ITvShowPersistManager.SaveNotifiedTvShowsAsync));
        }

        public void SaveTvShows(IEnumerable<TvShowSubscribedItem> tvShows)
        {
            RecordCall(nameof(ITvShowPersistManager.SaveTvShows), tvShows);

            ProcessMethodAction(nameof(ITvShowPersistManager.SaveTvShows));
        }

        public Task SaveTvShowsAsync(IEnumerable<TvShowSubscribedItem> tvShows)
        {
            RecordCall(nameof(ITvShowPersistManager.SaveTvShowsAsync), tvShows);

            ProcessMethodAction(nameof(ITvShowPersistManager.SaveTvShowsAsync));

            return ReturnValueForAnyArgs<Task>(nameof(ITvShowPersistManager.SaveTvShowsAsync));
        }
    }
}
