using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Core.Model
{
    public class TvShowEpisode
    {
        #region Members

        readonly static string UnknowDateValue = "N/A";
        DateTime? _releaseDate;

        #endregion

        #region Properties

        public string Title
        {
            get;
            set;
        }

        public int Year
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

        [JsonIgnore]
        public DateTime? ReleaseDate
        {
            get
            {
                return _releaseDate ?? (_releaseDate = ParseReleaseDate());
            }
        }

        public int Season
        {
            get;
            set;
        }

        public int Episode
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

        public string ImdbId
        {
            get;
            set;
        }

        public string SeriesId
        {
            get;
            set;
        }

        #endregion

        #region Private Methods

        private DateTime? ParseReleaseDate()
        {
            if(string.IsNullOrEmpty(Released) || Released == UnknowDateValue)
            {
                return null;
            }

            DateTime value;

            if(DateTime.TryParse(Released, out value))
            {
                return value;
            }

            return null;
        }

        #endregion
    }
}
