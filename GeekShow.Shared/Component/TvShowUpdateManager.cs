using GeekShow.Shared.Model;
using GeekShow.Shared.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var tvShows = await _persistManager.LoadTvShowsAsync();

            var updated = false;

            foreach (var tvShow in tvShows)
            {
                var nextEpisodeDate = GetNextEpisodeDate(tvShow.NextEpisodeDate);

                if (tvShow.EndDate != null || (nextEpisodeDate != null && nextEpisodeDate.Value.DateTime > DateTimeOffset.Now))
                {
                    continue;
                }

                try
                {
                    var baseInfo = _tvShowService.GetTvShowAsync(tvShow.ShowId);
                    var quickInfo = _tvShowService.GetTvShowQuickInfoAsync(tvShow.Name);

                    updated = updated || UpdateTvShow(tvShow, await baseInfo, await quickInfo);
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


        private bool UpdateTvShow(TvShowSubscribedItem tvShow, TvShowItem tvShowBaseInfo, TvShowQuickInfoItem tvShowQuickInfo)
        {
            var updated = false;

            if (tvShow.LastEpisodeId != tvShowQuickInfo.LastEpisodeId)
            {
                tvShow.LastEpisodeId = tvShowQuickInfo.LastEpisodeId;
                updated = true;
            }
            if (tvShow.LastEpisodeName != tvShowQuickInfo.LastEpisodeName)
            {
                tvShow.LastEpisodeName = tvShowQuickInfo.LastEpisodeName;
                updated = true;
            }
            if (tvShow.LastEpisodeDate != tvShowQuickInfo.LastEpisodeDate)
            {
                tvShow.LastEpisodeDate = tvShowQuickInfo.LastEpisodeDate;
                updated = true;
            }
            if (tvShow.NextEpisodeId != tvShowQuickInfo.NextEpisodeId)
            {
                tvShow.NextEpisodeId = tvShowQuickInfo.NextEpisodeId;
                updated = true;
            }
            if (tvShow.NextEpisodeName != tvShowQuickInfo.NextEpisodeName)
            {
                tvShow.NextEpisodeName = tvShowQuickInfo.NextEpisodeName;
                updated = true;
            }
            if (tvShow.NextEpisodeDate != tvShowQuickInfo.NextEpisodeDate)
            {
                tvShow.NextEpisodeDate = tvShowQuickInfo.NextEpisodeDate;
                updated = true;
            }
            if (tvShow.Status != tvShowQuickInfo.Status)
            {
                tvShow.Status = tvShowQuickInfo.Status;
                updated = true;
            }
            if (tvShow.EndDate != tvShowQuickInfo.Ended)
            {
                tvShow.EndDate = tvShowQuickInfo.Ended;
                tvShow.Ended = tvShowBaseInfo.EndDate == null ? 0 : tvShowBaseInfo.EndDate.Value.Year;

                updated = true;
            }
            if (tvShow.Seasons != tvShowQuickInfo.Seasons)
            {
                tvShow.Seasons = tvShowQuickInfo.Seasons;
                updated = true;
            }

            return updated;
        }

        private DateTimeOffset? GetNextEpisodeDate(DateTimeOffset? currentShowDateTime)
        {
            if (currentShowDateTime == null)
            {
                return null;
            }
            else if (currentShowDateTime.Value.Hour == 0)
            {
                return currentShowDateTime.Value.AddHours(20);
            }
            else
            {
                return currentShowDateTime;
            }
        }

        #endregion
    }
}
