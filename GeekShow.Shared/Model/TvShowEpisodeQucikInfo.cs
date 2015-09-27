using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Model
{
    public class TvShowEpisodeQucikInfo
    {
        #region Properties

        public int Season
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        [JsonProperty("Number")]
        public int EpisodeNumber
        {
            get;
            set;
        }

        #endregion
    }
}
