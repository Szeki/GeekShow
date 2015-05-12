using GeekShow.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Service
{
    public interface ITvShowService
    {
        IEnumerable<TvShowItem> SearchShow(string searchValue);
        Task<IEnumerable<TvShowItem>> SearchShowAsync(string searchValue);

        TvShowQuickInfoItem GetTvShowQuickInfo(string tvShowName);
        Task<TvShowQuickInfoItem> GetTvShowQuickInfoAsync(string tvShowName);

        TvShowItem GetTvShow(int showId);
        Task<TvShowItem> GetTvShowAsync(int showId);
    }
}
