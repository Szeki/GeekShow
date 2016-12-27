using GeekShow.Core.Service;

namespace GeekShow.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var shows = new TvMazeService().SearchShow("Game of Thrones").ToList();
            var episode = new TvMazeService().GetEpisode("729575");

            //System.Console.WriteLine(TvMazeServiceHelper.GetEpisodeIdFromUrl("http://api.tvmaze.com/episodes/729575"));

            System.Console.ReadKey();
        }
    }
}
