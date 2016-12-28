using GeekShow.Core.Service;
using System.Linq;

namespace GeekShow.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var shows = new TvMazeService().SearchShow("Game of Thrones").ToList();
            //var episode = new TvMazeService().GetEpisode("729575");
            //var seasons = new TvMazeService().GetTvShowSeasons(82).ToList();
            var episode = new TvMazeService().GetEpisodeByNumberAsync(82, 6, 9).Result;

            //System.Console.WriteLine(TvMazeServiceHelper.GetEpisodeIdFromUrl("http://api.tvmaze.com/episodes/729575"));

            System.Console.ReadKey();
        }
    }
}
