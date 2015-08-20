using GeekShow.Shared.Component;
using GeekShow.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Background;

namespace GeekShow.Tasks
{
    public sealed class NotifyShowsTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var persistManager = new TvShowPersistManager();
            var notificationManager = new NotificationManager();

            var tvShows = persistManager.LoadTvShowsAsync();
            var notifiedTvShows = await persistManager.LoadNotifiedTvShowsAsync();
            var newlyNotifiedTvShows = new List<NotifiedTvShowItem>();

            foreach(var tvShow in (await tvShows).Where(item => item.EndDate == null && item.NextEpisodeDate != null))
            {
                var utcNow = DateTime.UtcNow;
                var notificationDeadLine = tvShow.NextEpisodeDate.Value.UtcDateTime.Subtract(new TimeSpan(6, 5, 0));

                if(tvShow.NextEpisodeDate.Value.UtcDateTime < utcNow || notificationDeadLine > utcNow)
                {
                    continue;
                }

                var notifiedShow = notifiedTvShows.FirstOrDefault(item => item.ShowId == tvShow.ShowId && item.EpisodeId == tvShow.NextEpisodeId);

                if(notifiedShow == null)
                {
                    notifiedShow = notifiedTvShows.FirstOrDefault(item => item.ShowId == tvShow.ShowId && item.EpisodeDate == tvShow.NextEpisodeDate.Value);
                }

                if(notifiedShow != null)
                {
                    newlyNotifiedTvShows.Add(notifiedShow);

                    continue;
                }

                notificationManager.SendEpisodeReminderNotification(tvShow);

                newlyNotifiedTvShows.Add(new NotifiedTvShowItem(tvShow.ShowId) { EpisodeId = tvShow.NextEpisodeId, EpisodeDate = tvShow.NextEpisodeDate.Value, NotifiedAt = DateTime.UtcNow });
            }

            await persistManager.SaveNotifiedTvShowsAsync(newlyNotifiedTvShows);

            deferral.Complete();
        }
    }
}
