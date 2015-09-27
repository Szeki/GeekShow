using GeekShow.Shared.Component;
using GeekShow.Shared.Component.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Model
{
    public class TvShowImdbSearchItem
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

        public string ImdbId
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        #endregion

        #region Operator Overloading

        public static explicit operator TvShowItem(TvShowImdbSearchItem tvShow)
        {
            return ImdbHelper.ConvertToTvShowItem(tvShow);
        }

        #endregion
    }
}
