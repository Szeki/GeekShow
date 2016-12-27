using GeekShow.Core.Component;
using GeekShow.Core.Model.TvMaze;
using GeekShow.Core.Service;
using System;
using System.Threading.Tasks;

namespace GeekShow.Shared.Component
{
    public class TvShowUpdateManager
    {
        #region Members

        readonly ITvShowService _tvShowService;
        readonly ITvShowPersistManager _persistManager;

        #endregion

        #region Constructor

        public TvShowUpdateManager(ITvShowService tvShowService, ITvShowPersistManager persistManager)
        {
            _tvShowService = tvShowService;
            _persistManager = persistManager;
        }

        #endregion

        #region Public Methods

        public async Task<bool> UpdateTvShowsAsync()
        {
            var defaultDate = new DateTime(9999, 12, 31);
            var tvShows = await _persistManager.LoadTvShowsAsync();

            var updated = false;

            foreach (var tvShow in tvShows)
            {
                if(tvShow.TvShow.Status == "Ended")
                {
                    continue;
                }

                var currentNextEpisodeDate = tvShow.NextEpisode == null ? defaultDate : DateTimeOffset.Parse(tvShow.NextEpisode.AirStamp).AddHours(20);
                
                if (currentNextEpisodeDate > DateTimeOffset.Now)
                {
                    continue;
                }

                try
                {
                    var updatedTvShow = await _tvShowService.GetTvShowAsync(tvShow.Id);

                    updated = UpdateTvShow(_tvShowService, tvShow, updatedTvShow) || updated;
                }
                catch { }
            }

            if (updated)
            {
                await _persistManager.SaveTvShowsAsync(tvShows);
            }

            return updated;
        }

        #endregion

        #region Private Methods
        
        private bool UpdateTvShow(ITvShowService service, TvMazeTvShow tvShow, TvMazeItem updatedTvShow)
        {
            if(tvShow.Updated == updatedTvShow.Updated)
            {
                return false;
            }

            TvMazeServiceHelper.UpdateTvShow(service, tvShow, updatedTvShow);

            return true;
        }
        
        #endregion
    }
}
