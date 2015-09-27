using GeekShow.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Component.Helper
{
    public static class ImdbHelper
    {
        #region Members

        readonly static string ImdbUrlPath = @"http://www.imdb.com/title/{0}/";

        #endregion

        #region Public Methods

        public static TvShowItem ConvertToTvShowItem(TvShowImdbSearchItem tvShow)
        {
            if (tvShow == null)
            {
                return null;
            }

            var showYears = tvShow.Year.Split(new string[] { "–" }, StringSplitOptions.RemoveEmptyEntries);

            var showItem = new TvShowItem(tvShow.ImdbId, tvShow.Title)
            {
                Started = int.Parse(showYears[0]),
                Ended = showYears.Length > 1 ? (int?)int.Parse(showYears[1]) : null,
                Link = string.Format(ImdbUrlPath, tvShow.ImdbId)
            };

            showItem.Status = GetTvShowStatus(tvShow.Year, showItem.Ended);

            return showItem;
        }

        public static TvShowItem ConvertToTvShowItem(TvShowImdbItem tvShow)
        {
            if (tvShow == null)
            {
                return null;
            }

            var showYears = tvShow.Year.Split(new string[] { "–" }, StringSplitOptions.RemoveEmptyEntries);

            var showItem = new TvShowItem(tvShow.ImdbId, tvShow.Title)
            {
                Started = int.Parse(showYears[0]),
                Ended = showYears.Length > 1 ? (int?)int.Parse(showYears[1]) : null,
                Link = string.Format(ImdbUrlPath, tvShow.ImdbId),
                Country = tvShow.Country,
                Plot = tvShow.Plot
            };

            showItem.Status = GetTvShowStatus(tvShow.Year, showItem.Ended);

            return showItem;
        }

        public static TvShowQuickInfoItem ConvertToTvShowQuickInfoItem(TvShowImdbItem tvShow)
        {
            if (tvShow == null)
            {
                return null;
            }

            var showYears = tvShow.Year.Split(new string[] { "–" }, StringSplitOptions.RemoveEmptyEntries);
            var endYear = showYears.Length > 1 ? (int?)int.Parse(showYears[1]) : null;

            var showItem = new TvShowQuickInfoItem()
            {
                ShowId = tvShow.ImdbId,
                ShowName = tvShow.Title,
                Started = DateTime.ParseExact(tvShow.Released, "d MMM yyyy", null),
                ShowUrl = string.Format(ImdbUrlPath, tvShow.ImdbId),
                Country = tvShow.Country,
                Plot = tvShow.Plot,
                Runtime = tvShow.RunTime,
                Genres = tvShow.Genre,
                Status = GetTvShowStatus(tvShow.Year, endYear)
            };

            showItem.Status = GetTvShowStatus(tvShow.Year, endYear);

            return showItem;
        }

        #endregion

        #region Private Methods

        private static string GetTvShowStatus(string years, int? endYear)
        {

            if (!years.Contains("–"))
            {
                return "Ended";
            }
            else if (endYear == null)
            {
                return "Running";
            }
            else
            {
                return "Ended";
            }
        }

        #endregion
    }
}
