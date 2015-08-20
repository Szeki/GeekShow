using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using GeekShow.Shared.Service;
using System;
using Windows.ApplicationModel.Background;

namespace GeekShow.Tasks
{
    public sealed class UpdateShowsTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var tvShowService = new TvShowTvRageService();
            var persistManager = new TvShowPersistManager();
            var tvShows = await persistManager.LoadTvShowsAsync();

            var updated = false;

            foreach(var tvShow in tvShows)
            {
                if(tvShow.EndDate != null || (tvShow.NextEpisodeDate != null && tvShow.NextEpisodeDate.Value.ToLocalTime() > DateTimeOffset.Now))
                {
                    continue;
                }

                try
                {
                    var baseInfo = tvShowService.GetTvShowAsync(tvShow.ShowId);
                    var quickInfo = tvShowService.GetTvShowQuickInfoAsync(tvShow.Name);

                    updated = updated || UpdateTvShow(tvShow, await baseInfo, await quickInfo);
                }
                catch { }
            }

            if(updated)
            {
                await persistManager.SaveTvShowsAsync(tvShows);

                Windows.Storage.ApplicationData.Current.LocalSettings.Values["UpdateSynched"] = false;
            }

            deferral.Complete();
        }

        private bool UpdateTvShow(TvShowSubscribedItem tvShow, TvShowItem tvShowBaseInfo, TvShowQuickInfoItem tvShowQuickInfo)
        {
            var updated = false;

            if(tvShow.LastEpisodeId != tvShowQuickInfo.LastEpisodeId)
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
            if (tvShow.Seasons != tvShowBaseInfo.Seasons)
            {
                tvShow.Seasons = tvShowBaseInfo.Seasons;
                updated = true;
            }

            return updated;
        }
    }
}
