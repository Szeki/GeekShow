using GeekShow.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Shared.Component
{
    public class TvShowNotifier
    {
        #region Members

        readonly ITvShowPersistManager _persistManager;
        readonly INotificationManager _notificationManager;

        readonly static TimeSpan NotificationTimeSpan = new TimeSpan(6, 5, 0);

        #endregion

        #region Constructor

        public TvShowNotifier(ITvShowPersistManager persistManager, INotificationManager notificationManager)
        {
            _persistManager = persistManager;
            _notificationManager = notificationManager;
        }

        #endregion

        #region Public Methods

        public async void CalculateAndSendNotifications()
        {
            var tvShows = _persistManager.LoadTvShowsAsync();
            var notifiedTvShows = await _persistManager.LoadNotifiedTvShowsAsync();
            var newlyNotifiedTvShows = new List<NotifiedTvShowItem>();

            foreach (var tvShow in (await tvShows).Where(item => item.EndDate == null && item.NextEpisodeDate != null))
            {
                var utcNow = DateTimeProvider.UtcNow;
                var notificationDeadLine = tvShow.NextEpisodeDate.Value.UtcDateTime.Subtract(NotificationTimeSpan);

                if (tvShow.NextEpisodeDate.Value.UtcDateTime < utcNow || notificationDeadLine > utcNow)
                {
                    continue;
                }

                var notifiedShow = notifiedTvShows.FirstOrDefault(item => item.ShowId == tvShow.ShowId && item.EpisodeId == tvShow.NextEpisodeId);

                if (notifiedShow == null)
                {
                    notifiedShow = notifiedTvShows.FirstOrDefault(item => item.ShowId == tvShow.ShowId && item.EpisodeDate == tvShow.NextEpisodeDate.Value);
                }

                if (notifiedShow != null)
                {
                    newlyNotifiedTvShows.Add(notifiedShow);

                    continue;
                }

                _notificationManager.SendEpisodeReminderNotification(tvShow);

                newlyNotifiedTvShows.Add(new NotifiedTvShowItem(tvShow.ShowId) { EpisodeId = tvShow.NextEpisodeId, EpisodeDate = tvShow.NextEpisodeDate.Value, NotifiedAt = DateTimeProvider.UtcNow });
            }

            await _persistManager.SaveNotifiedTvShowsAsync(newlyNotifiedTvShows);
        }

        #endregion
    }
}
