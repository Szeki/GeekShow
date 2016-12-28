using Newtonsoft.Json;

namespace GeekShow.Core.Model.TvMaze
{
    public class TvMazeSeason
    {
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("number")]
        public int SeasonNumber
        {
            get;
            set;
        }

        public int? EpisodeOrder
        {
            get;
            set;
        }
    }
}
