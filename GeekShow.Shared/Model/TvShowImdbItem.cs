using GeekShow.Shared.Component.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Model
{
    public class TvShowImdbItem
    {
        #region Properties

        public string Title
        {
            get;
            set;
        }

        public string Year
        {
            get;
            set;
        }

        public string Rated
        {
            get;
            set;
        }

        public string Released
        {
            get;
            set;
        }

        public string RunTime
        {
            get;
            set;
        }

        public string Genre
        {
            get;
            set;
        }

        public string Director
        {
            get;
            set;
        }

        public string Writer
        {
            get;
            set;
        }

        public string Actors
        {
            get;
            set;
        }

        public string Plot
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }

        public string Awards
        {
            get;
            set;
        }

        public string Poster
        {
            get;
            set;
        }

        public string Metascore
        {
            get;
            set;
        }

        public string ImdbRating
        {
            get;
            set;
        }

        public string ImdbVotes
        {
            get;
            set;
        }

        public string ImdbId
        {
            get;
            set;
        }

        #endregion

        #region Operator Overloading

        public static explicit operator TvShowItem(TvShowImdbItem tvShow)
        {
            return ImdbHelper.ConvertToTvShowItem(tvShow);
        }

        public static explicit operator TvShowQuickInfoItem(TvShowImdbItem tvShow)
        {
            return ImdbHelper.ConvertToTvShowQuickInfoItem(tvShow);
        }

        #endregion
    }
}
