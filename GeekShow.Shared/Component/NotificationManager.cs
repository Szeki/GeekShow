using GeekShow.Core.Model.TvMaze;
using Windows.UI.Notifications;

namespace GeekShow.Shared.Component
{
    public class NotificationManager : INotificationManager
    {
        public void SendEpisodeReminderNotification(TvMazeTvShow tvShow)
        {
            var toastTemplate = ToastTemplateType.ToastText02;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            var textElements = toastXml.GetElementsByTagName("text");
            textElements[0].AppendChild(toastXml.CreateTextNode(string.Format("{0} episode today", tvShow.TvShow.Name)));
            textElements[1].AppendChild(toastXml.CreateTextNode(string.Format("{0}: {1}", tvShow.NextEpisode.EpisodeId, tvShow.NextEpisode.EpisodeName)));

            var toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
