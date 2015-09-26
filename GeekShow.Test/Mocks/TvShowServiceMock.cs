using GeekShow.Shared.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekShow.Shared.Model;

namespace GeekShow.Test.Mocks
{
    public class TvShowServiceMock : MockBase, ITvShowService
    {
        public TvShowItem GetTvShow(int showId)
        {
            RecordCall(nameof(ITvShowService.GetTvShow));

            ProcessMethodAction(nameof(ITvShowService.GetTvShow));

            return ReturnValueForAnyArgs<TvShowItem>(nameof(ITvShowService.GetTvShow));
        }

        public Task<TvShowItem> GetTvShowAsync(int showId)
        {
            RecordCall(nameof(ITvShowService.GetTvShowAsync));

            ProcessMethodAction(nameof(ITvShowService.GetTvShowAsync));

            return ReturnValueForAnyArgs<Task<TvShowItem>>(nameof(ITvShowService.GetTvShowAsync));
        }

        public TvShowQuickInfoItem GetTvShowQuickInfo(string tvShowName)
        {
            RecordCall(nameof(ITvShowService.GetTvShowQuickInfo));

            ProcessMethodAction(nameof(ITvShowService.GetTvShowQuickInfo));

            return ReturnValueForAnyArgs<TvShowQuickInfoItem>(nameof(ITvShowService.GetTvShowQuickInfo));
        }

        public Task<TvShowQuickInfoItem> GetTvShowQuickInfoAsync(string tvShowName)
        {
            RecordCall(nameof(ITvShowService.GetTvShowQuickInfoAsync));

            ProcessMethodAction(nameof(ITvShowService.GetTvShowQuickInfoAsync));

            return ReturnValueForAnyArgs<Task<TvShowQuickInfoItem>>(nameof(ITvShowService.GetTvShowQuickInfoAsync));
        }

        public IEnumerable<TvShowItem> SearchShow(string searchValue)
        {
            RecordCall(nameof(ITvShowService.SearchShow));

            ProcessMethodAction(nameof(ITvShowService.SearchShow));

            return ReturnValueForAnyArgs<IEnumerable<TvShowItem>>(nameof(ITvShowService.SearchShow));
        }

        public Task<IEnumerable<TvShowItem>> SearchShowAsync(string searchValue)
        {
            RecordCall(nameof(ITvShowService.SearchShowAsync));

            ProcessMethodAction(nameof(ITvShowService.SearchShowAsync));

            return ReturnValueForAnyArgs<Task<IEnumerable<TvShowItem>>>(nameof(ITvShowService.SearchShowAsync));
        }
    }
}
