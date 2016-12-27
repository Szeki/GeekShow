using Newtonsoft.Json;

namespace GeekShow.Core.Model.TvMaze
{
    public class TvMazeNetwork
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public TvMazeCountry Country
        {
            get;
            set;
        }

        [JsonIgnore]
        public string Network => $"{Name}, {Country.Name}";
    }
}
