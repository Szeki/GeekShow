using GeekShow.Core.Component;
using GeekShow.Core.Model;
using GeekShow.Core.Model.TvMaze;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CalculateAndSendNotificationsAsync()
        {
            var now = DateTimeOffset.Now;
            var tvShows = _persistManager.LoadTvShowsAsync();
            var notifiedTvShows = await _persistManager.LoadNotifiedTvShowsAsync();
            var newlyNotifiedTvShows = new List<NotifiedTvShowItem>();

            foreach (var tvShow in (await tvShows).Where(item => item.TvShow.Status != "Ended" && item.NextEpisode != null))
            {
                var nextEpisodeDate = tvShow.NextEpisode.AirDate;
                var airTimeParts = string.IsNullOrEmpty(tvShow.NextEpisode.AirTime) ? new string[0] : tvShow.NextEpisode.AirTime.Split(':');

                var nextEpisodeDateAdjusted = new DateTime(nextEpisodeDate.Year, nextEpisodeDate.Month, nextEpisodeDate.Day, airTimeParts.Length == 0 ? 20 : int.Parse(airTimeParts[0]), airTimeParts.Length == 0 ? 0 : int.Parse(airTimeParts[1]), 0);
                var notificationDeadLine = nextEpisodeDateAdjusted.Subtract(NotificationTimeSpan);

                if (nextEpisodeDateAdjusted < now || notificationDeadLine > now)
                {
                    continue;
                }

                var notifiedShow = notifiedTvShows.FirstOrDefault(item => item.ShowId == tvShow.Id && item.EpisodeId == tvShow.NextEpisode.EpisodeId);

                if (notifiedShow == null)
                {
                    notifiedShow = notifiedTvShows.FirstOrDefault(item => item.ShowId == tvShow.Id && item.EpisodeDate == tvShow.NextEpisode.AirStamp);
                }

                if (notifiedShow != null)
                {
                    newlyNotifiedTvShows.Add(notifiedShow);

                    continue;
                }

                _notificationManager.SendEpisodeReminderNotification(tvShow);

                newlyNotifiedTvShows.Add(new NotifiedTvShowItem(tvShow.Id) { EpisodeId = tvShow.NextEpisode.EpisodeId, EpisodeDate = tvShow.NextEpisode.AirStamp, NotifiedAt = DateTimeProvider.UtcNow });
            }

            await _persistManager.SaveNotifiedTvShowsAsync(newlyNotifiedTvShows);
        }

        #endregion

        #region Private Methods
        
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
