using GeekShow.Core.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekShow.Shared.Service
{
    [Obsolete]
    public class TvShowTvRageService : ITvShowService
    {
        #region Members

        readonly TvShowTvRageServiceQueryResultParser _parser;
        readonly static string SearchShowUrlFormat = @"http://services.tvrage.com/feeds/search.php?show={0}";
        readonly static string ShowQuickInfoUrlFormat = @"http://services.tvrage.com/tools/quickinfo.php?show={0}";
        readonly static string ShowInfoUrlFormat = @"http://services.tvrage.com/feeds/showinfo.php?sid={0}";

        #endregion

        #region Constructor

        public TvShowTvRageService()
        {
            _parser = new TvShowTvRageServiceQueryResultParser();
        }

        #endregion

        #region ITvShowService implementation

        public IEnumerable<TvShowItem> SearchShow(string searchValue)
        {
            using(var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri(string.Format(SearchShowUrlFormat, searchValue)));

                var returnValue = task.Result;

                return _parser.ParseSearchResult(returnValue);
            }
        }

        public Task<IEnumerable<TvShowItem>> SearchShowAsync(string searchValue)
        {
            return Task.Factory.StartNew(() => SearchShow(searchValue));
        }

        public TvShowQuickInfoItem GetTvShowQuickInfo(string tvShowName)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri(string.Format(ShowQuickInfoUrlFormat, tvShowName)));

                var returnValue = task.Result;

                return _parser.ParseQuckInfoResult(returnValue);
            }
        }

        public Task<TvShowQuickInfoItem> GetTvShowQuickInfoAsync(string tvShowName)
        {
            return Task.Factory.StartNew(() => GetTvShowQuickInfo(tvShowName));
        }

        public TvShowItem GetTvShow(string showId)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri(string.Format(ShowInfoUrlFormat, showId)));

                var returnValue = task.Result;

                return _parser.ParseShowQueryResult(returnValue);
            }
        }

        public Task<TvShowItem> GetTvShowAsync(string showId)
        {
            return Task.Factory.StartNew(() => GetTvShow(showId));
        }
        
        #endregion
    }
}
