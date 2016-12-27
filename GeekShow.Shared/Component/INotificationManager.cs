using GeekShow.Core.Model.TvMaze;

namespace GeekShow.Shared.Component
{
    public interface INotificationManager
    {
        void SendEpisodeReminderNotification(TvMazeTvShow tvShow);
    }
}
