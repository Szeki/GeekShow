using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekShow.Shared.Service
{
    public class TvShowTvRageService : ITvShowService
    {
        #region Members

        readonly TvShowServiceQueryResultParser _parser;
        readonly static string SearchShowUrlFormat = @"http://services.tvrage.com/feeds/search.php?show={0}";
        readonly static string ShowQuickInfoUrlFormat = @"http://services.tvrage.com/tools/quickinfo.php?show={0}";
        readonly static string ShowInfoUrlFormat = @"http://services.tvrage.com/feeds/showinfo.php?sid={0}";

        #endregion

        #region Constructor

        public TvShowTvRageService()
        {
            _parser = new TvShowServiceQueryResultParser();
        }

        #endregion

        #region ITvShowService implementation

        public IEnumerable<Model.TvShowItem> SearchShow(string searchValue)
        {
            using(var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri(string.Format(SearchShowUrlFormat, searchValue)));

                var returnValue = task.Result;

                return _parser.ParseSearchResult(returnValue);
            }
        }

        public Task<IEnumerable<Model.TvShowItem>> SearchShowAsync(string searchValue)
        {
            return Task.Factory.StartNew(() => SearchShow(searchValue));
        }

        public Model.TvShowQuickInfoItem GetTvShowQuickInfo(string tvShowName)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri(string.Format(ShowQuickInfoUrlFormat, tvShowName)));

                var returnValue = task.Result;

                return _parser.ParseQuckInfoResult(returnValue);
            }
        }

        public Task<Model.TvShowQuickInfoItem> GetTvShowQuickInfoAsync(string tvShowName)
        {
            return Task.Factory.StartNew(() => GetTvShowQuickInfo(tvShowName));
        }

        public Model.TvShowItem GetTvShow(int showId)
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync(new Uri(string.Format(ShowInfoUrlFormat, showId)));

                var returnValue = task.Result;

                return _parser.ParseShowQueryResult(returnValue);
            }
        }

        public Task<Model.TvShowItem> GetTvShowAsync(int showId)
        {
            return Task.Factory.StartNew(() => GetTvShow(showId));
        }
        
        #endregion

    }
}
